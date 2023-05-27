using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuSeleccionar : MonoBehaviour
{
	//public LineRenderer puntero;
	//public Slider speed;

	[SerializeField, FormerlySerializedAs("trackedDevice_")]
	private Transform _trackedDevice;

	public Transform seleccionado=null;
	//public TextMesh ui;
	private int layer;
	protected Dictionary<GameObject, int> m_grabCandidates = new Dictionary<GameObject, int>();
	[SerializeField]
	protected Transform m_gripTransform = null;

	// Start is called before the first frame update
	void Start()
    {
		layer = LayerMask.GetMask("Selectable");
	}

	// Update is called once per frame
	void Update()
    {
		//ui.text = m_grabCandidates.Count.ToString();
		if (this.GetComponent<OVRGrabber>().grabbedObject != null)
		{
			if (seleccionado != null)
			{
				seleccionado.gameObject.layer = 8;
			}
			seleccionado = this.GetComponent<OVRGrabber>().grabbedObject.gameObject.transform;
			seleccionado.gameObject.layer = 9;
		}
		else 
		{
			float closestMagSq = float.MaxValue;
			OVRGrabbable closestGrabbable = null;
			Collider closestGrabbableCollider = null;
			//ui.text = m_grabCandidates.Count.ToString();
			foreach (GameObject obj in m_grabCandidates.Keys)
			{
				OVRGrabbable grabbable = obj.GetComponent<OVRGrabbable>();
				bool canGrab = !(grabbable.isGrabbed && !grabbable.allowOffhandGrab);
				if (!canGrab)
				{
					continue;
				}

				for (int j = 0; j < grabbable.grabPoints.Length; ++j)
				{
					Collider grabbableCollider = grabbable.grabPoints[j];
					// Store the closest grabbable
					Vector3 closestPointOnBounds = grabbableCollider.ClosestPointOnBounds(m_gripTransform.position);
					float grabbableMagSq = (m_gripTransform.position - closestPointOnBounds).sqrMagnitude;
					if (grabbableMagSq < closestMagSq)
					{
						closestMagSq = grabbableMagSq;
						closestGrabbable = grabbable;
						closestGrabbableCollider = grabbableCollider;
					}
				}
			}

			if (closestGrabbable != null)
			{
				if (seleccionado != null)
				{
					seleccionado.gameObject.layer = 8;
				}
				seleccionado = closestGrabbableCollider.gameObject.transform;
				seleccionado.gameObject.layer = 9;
			}
			else
			{
				Destruir();
			}

			if (m_grabCandidates.Count == 0 && seleccionado!=null)
			{
				//ui.text = seleccionado.name;
				deseleccionar();
			}
		}
	}

	public void deseleccionar()
	{
		if (seleccionado != null)
		{
			seleccionado.gameObject.layer = 8;
		}
		seleccionado = null;
		
	}

	public void Destruir()
	{
		m_grabCandidates.Clear();
		deseleccionar();
	}

	void OnTriggerEnter(Collider otherCollider)
	{
		if (!otherCollider.tag.Equals("Selectable")) return;

		// Add the grabbable
		int refCount = 0;
		m_grabCandidates.TryGetValue(otherCollider.gameObject, out refCount);
		m_grabCandidates[otherCollider.gameObject] = refCount + 1;
	}

	void OnTriggerExit(Collider otherCollider)
	{
		OVRGrabbable grabbable = otherCollider.GetComponent<OVRGrabbable>() ?? otherCollider.GetComponentInParent<OVRGrabbable>();
		if (grabbable == null) return;

		// Remove the grabbable
		int refCount = 0;
		bool found = m_grabCandidates.TryGetValue(otherCollider.gameObject, out refCount);
		if (!found)
		{
			return;
		}

		if (refCount > 1)
		{
			m_grabCandidates[otherCollider.gameObject] = refCount - 1;
		}
		else
		{
			m_grabCandidates.Remove(otherCollider.gameObject);
		}
	}
}
