using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTodas : MonoBehaviour
{
	//Script para controlar la reproduccion con los botones 

	public MenuSeleccionar seleccionar;		//Script de seleccion
	public List<GameObject> orden = new List<GameObject>();  //Lista ordenada para reproducir secuencialmente
	private int index = 0;
	private bool secuencial = false;	//Se esta reproduciendo secuencial
	private bool adelante = true;		//Se reproducen hacia adelante
	private bool ralentizado = false;	//Esta activado la ralentización
	public Slider speed;				//Slider con escala de ralentización

	//Pausar todos los objetos
	public void ButtonPause()
	{
		secuencial = false;
		int n = this.transform.childCount;
		for(int i=0; i < n; i++)
		{
			Transform hijo = this.transform.GetChild(i);
			hijo.GetComponent<Reproducir>().ButtonPause();
		}
	}

	//Reproducir todos los objetos desde el principio
	public void ButtonPlay()
	{
		secuencial = false;
		int n = this.transform.childCount;
		for (int i = 0; i < n; i++)
		{
			Transform hijo = this.transform.GetChild(i);
			hijo.GetComponent<Reproducir>().ButtonPlay(adelante);
		}
	}

	//Añadir/actualizar objeto en el orden de reproduccion secuencial
	public void AñadirSecuencia(GameObject obj)
	{
		if (orden.Contains(obj))
		{
			orden.Remove(obj);
		}
		orden.Add(obj);
	}

	//Reproducir de forma secuencial
	public void PlaySecuencial()
	{
		if (orden.Count > 0)
		{
			int n = this.transform.childCount;
			for (int i = 0; i < n; i++)
			{
				Transform hijo = this.transform.GetChild(i);
				hijo.GetComponent<Reproducir>().Reiniciar();
			}
			index = 0;
			ButtonPause();
			orden[0].GetComponent<Reproducir>().ButtonPlay(adelante);
			secuencial = true;
			index++;
		}
	}

	//Reproducir siguinte objeto de la lista en secuencial
	public void ContinuarSecuencial()
	{
		if (secuencial)
		{
			if (index < orden.Count)
			{
				orden[index].GetComponent<Reproducir>().ButtonPlay(adelante);
				orden[index-1].GetComponent<Reproducir>().ButtonPause();
				index++;
			}
			else
			{
				secuencial = false;
				orden[index - 1].GetComponent<Reproducir>().ButtonPause();
			}
		}
	}

	//Activar/desactivar ralentizacion en base a escala del slider
	public void ButtonTime()
	{
		int n = this.transform.childCount;
		float timeStep;
		if (ralentizado)
		{
			timeStep = 1;
		}
		else
		{
			timeStep = speed.value;
		}

		for (int i = 0; i < n; i++)
		{
			Reproducir hijo = this.transform.GetChild(i).GetComponent<Reproducir>();
			hijo.ButtonTime(timeStep);
		}
		ralentizado = !ralentizado;
	}

	//Cambiar el sentido de reproduccion
	public void ButtonDireccion()
	{
		adelante = !adelante;
		int n = this.transform.childCount;
		for (int i = 0; i < n; i++)
		{
			Reproducir hijo = this.transform.GetChild(i).GetComponent<Reproducir>();
			hijo.ButtonDireccion(adelante);
		}
	}

	//Borrar todos los objetos
	public void ButtonBorrar()
	{
		seleccionar.Destruir();
		int n = this.transform.childCount;
		orden.Clear();
		for (int i = 0; i < n; i++)
		{
			this.transform.GetChild(i).GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero,Vector3.zero);
			Destroy(this.transform.GetChild(i).gameObject);
		}
	}

}
