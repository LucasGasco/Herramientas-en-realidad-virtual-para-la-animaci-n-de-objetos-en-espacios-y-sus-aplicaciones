using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reproducir : MonoBehaviour
{
	public OVRInput.Controller controllerHand = OVRInput.Controller.None;
	
	bool play;			//Animacion activa
	bool adelante;		//Reproduccion marcha adelante
	public List<Vector3> ruta = new List<Vector3>(); // lista de posiciones a recorrer
	Vector3 origen;
	int index = 0;
	int size;
	float tiempo=0;
	float step;			//Escala de tiempo
	private OVRGrabbable agarrar; //Componenete OVRGrabbable
	Transform padre;			//Transform de objeto padre para almacenarlos
	// Start is called before the first frame update
	void Start()
    {
		padre = this.transform.parent;
		play = false;
		adelante = true;
		step = 1;
		//delta = Time.deltaTime;
		agarrar = this.GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {	
		//Solo se reproduce si no esta agarrado
		if (play && !agarrar.isGrabbed)
		{
			//Movimiento segun escala de tiempo
			tiempo += step;
			transform.position = Vector3.Lerp(origen, ruta[index], tiempo);
			//Si llego a la posicion pasar a la siguiente
			if (transform.position == ruta[index])
			{
				tiempo = 0;
				origen = transform.position;
				//Reproduccion marcha adelante
				if (adelante)
				{
					if (index >= size - 1)
					{
						index = 1;
						transform.position = ruta[0];
						//Llamada para el siguiente objeto en reproduccion scuencial
						this.GetComponentInParent<ControlTodas>().ContinuarSecuencial();
					}
					else
					{
						index++;
					}
				}
				//Reproduccion marcha atras
				else
				{
					if (index <= 0)
					{
						index = size - 2;
						transform.position = ruta[size - 1];
						//Llamada para el siguiente objeto en reproduccion scuencial
						this.GetComponentInParent<ControlTodas>().ContinuarSecuencial();
					}
					else
					{
						index--;
					}
				}
				
			}

			/*	
			if (OVRInput.GetUp(OVRInput.Button.Two, controllerHand))
			{
				play = false;
			}*/
		}
		/*
		if (OVRInput.GetUp(OVRInput.Button.Two, controllerHand) && size>2)
		{
			play = true;
		}*/
	}

	//Se inicia la reproduccion cuando acaba de grabar
	public void activar(List<Vector3> lista)
	{
		ruta.Clear();
		index = 1;
		play = true;
		size = lista.Count;
		for(int i = 0; i < size; i++)
		{
			ruta.Add(lista[i]);
		}
		transform.position = ruta[0];
		origen = ruta[0];
		tiempo = 1;
		if (padre != null)
		{
			transform.SetParent(padre);
			//Colocar objeto en orden para la reproduccion secuencial
			padre.GetComponent<ControlTodas>().AñadirSecuencia(gameObject);
		}
	}

	//Recibir la escala de tiempo
	public void ButtonTime(float time)
	{
		step = time;
	}

	//Boton play
	public void ButtonPlay(bool direccion)
	{
		adelante = direccion;
		if (ruta.Count != 0)
		{
			index = 0;
			play = true;
		}
	}
	//Boton pausa
	public void ButtonPause()
	{
		play = false;
	}
	//Boton de cambiar direccion
	public void ButtonDireccion(bool direccion)
	{

		adelante = direccion;
	}
	
	//Accion auxiliar para la reproduccion secuencial.
	public void Reiniciar()
	{
		index = 0;
		transform.position = ruta[0];
	}
}
