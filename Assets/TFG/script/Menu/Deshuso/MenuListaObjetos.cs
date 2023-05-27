using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuListaObjetos : MonoBehaviour
{
	public List<Image> png;
	public GameObject elemento;
	public GameObject lista;
	public MenuSeleccionar seleccionar;
	int index, preindex;
	float tiempo = 0;
	int n;
	ScrollRect scroll;
	RectTransform contentPanel;
	VerticalLayoutGroup layoutGroupComponent;
	// Start is called before the first frame update
	void Start()
	{
		n = 0;
		index = 0;
		preindex = 0;
		scroll = lista.GetComponentInParent<ScrollRect>();
		contentPanel = lista.GetComponentInParent<RectTransform>();
		layoutGroupComponent = lista.GetComponentInParent<VerticalLayoutGroup>();
	}

	// Update is called once per frame
	void Update()
	{
		tiempo += Time.deltaTime;
		if (n > 0 && tiempo >= 0.25)
		{
			tiempo = 0;
			float input = -OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y;
			//ui.text = input.ToString();
			if (input <= -0.5)
			{
				preindex = index;
				if (index == 0)
				{
					index = n - 1;
				}
				else
				{
					index--;
				}
				ActualizarFigura();
			}
			else if (input >= 0.5)
			{
				preindex = index;
				if (index == n - 1)
				{
					index = 0;
				}
				else
				{
					index++;

				}
				ActualizarFigura();
			}
		}
		if (OVRInput.GetDown(OVRInput.RawButton.Y))
		{
			if (n > 0)
			{
				//seleccionar.Seleccionar(lista.transform.GetChild(preindex).GetComponent<elementoLista>().Referencia());
			}
		}
	}

	private void ActualizarFigura()
	{
		lista.transform.GetChild(preindex).GetComponentInChildren<Outline>().enabled = false;
		lista.transform.GetChild(index).GetComponentInChildren<Outline>().enabled = true;

		//layoutGroupComponent.enabled = false;

		Vector3 target = lista.transform.GetChild(index).GetComponent<RectTransform>().position;
		contentPanel.anchoredPosition =
			(Vector2)scroll.transform.InverseTransformPoint(contentPanel.position)
			- (Vector2)scroll.transform.InverseTransformPoint(target);

		//layoutGroupComponent.enabled = true;

	}

	public void Anadir(int index)
	{
		GameObject obj = Instantiate(elemento);
		obj.transform.SetParent(lista.transform, false);
		obj.GetComponent<elementoLista>().Actualizar(n, index, seleccionar.seleccionado.gameObject);
		if (n == 0)
		{
			lista.transform.GetChild(0).GetComponentInChildren<Outline>().enabled = true;
		}
		n++;
		//ActualizarFigura();
	}
}
