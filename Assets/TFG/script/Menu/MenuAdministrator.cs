using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuAdministrator : MonoBehaviour
{
	public GameObject menu1, menu2, menu3;
	public OVRInput.Controller controllerHand = OVRInput.Controller.None;

	public TextMesh ui;
	float angulo;

    // Update is called once per frame
    void Update()
    {
		angulo = this.transform.rotation.eulerAngles.z;
		//ui.text = OVRInput.GetLocalControllerRotation(controllerHand).eulerAngles.ToString();
		if (angulo <= 320 && angulo>200)
		{
			menu1.SetActive(true);
			menu2.SetActive(false);
			menu3.SetActive(false);
		}
		else if(angulo >= 40 && angulo<=200)
		{
			menu1.SetActive(false);
			menu2.SetActive(false);
			menu3.SetActive(true);
		}
		else 
		{
			menu1.SetActive(false);
			menu2.SetActive(true);
			menu3.SetActive(false);
		}

	}
}
