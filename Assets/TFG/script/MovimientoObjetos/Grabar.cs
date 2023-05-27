using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grabar : MonoBehaviour
{
	OVRGrabbable grabbable;
	Vector3 posicion;
	public List<Vector3> ruta = new List<Vector3>();
	//private float tiempo = 0;
	//private float lanzamiento=0;
	//public Slider gravedad;
	public GameObject mando;	//Referencia al mando para cambiarlo de color
	public Material basico,rojo; // Materiales para el mando
	public MenuSeleccionar seleccionar; //Script de seleccion

	public enum Estado
	{
		Idle,
		Grabando,
		Volviendo//Estado obsoleto usado cuando se podia cambiar la gravedad
	}

	Estado estado;
    // Start is called before the first frame update
    void Start()
    {
		estado = Estado.Idle;
		grabbable = this.GetComponent<OVRGrabbable>();
		posicion = this.GetComponent<Transform>().position;
	}

    // Update is called once per frame
    void Update()
    {
		switch(estado)
		{
			case Estado.Idle:
				//Si es agarrado
				if (grabbable.isGrabbed)
				{
					estado = Estado.Grabando;
					mando.GetComponent<Renderer>().material = rojo;				
				}
				break;
			case Estado.Grabando:
				//Recolectar coordenadas por los que se pasa
				ruta.Add(transform.position);
				if (!grabbable.isGrabbed)
				{
					listo();
					////////////OBSOLETO/////////////
					//Si la gravedad del objeto esta cambiada
					/*if(gravedad.value == 0)
					{
						listo();
					}
					else
					{
						tiempo = 0;
						estado = Estado.Volviendo;
						this.GetComponent<Rigidbody>().isKinematic = false;
						if (gravedad.value < 0)
						{
							this.GetComponent<Rigidbody>().useGravity = false;
							lanzamiento = -gravedad.value;
						}
						else if (gravedad.value > 0)
						{
							lanzamiento = gravedad.value;
							this.GetComponent<Rigidbody>().useGravity = true;
						}
					}*/
				}
				break;
				/*
			//Estado auxiliar mientras no hay gravedad
			case Estado.Volviendo:
				ruta.Add(transform.position);
				tiempo += Time.deltaTime;
				if(tiempo > lanzamiento)
				{
					listo();
					this.GetComponent<Rigidbody>().isKinematic = true;
				}
				break;*/
		}
    }
	//Grabacion completada
	private void listo()
	{
		seleccionar.Destruir();
		mando.GetComponent<Renderer>().material = basico;
		estado = Estado.Idle;
		this.GetComponent<Reproducir>().activar(ruta);
		ruta.Clear();
	}
}
