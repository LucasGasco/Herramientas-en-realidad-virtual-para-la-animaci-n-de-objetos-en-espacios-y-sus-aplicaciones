using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBotones : MonoBehaviour
{
	private int index, preindex,n;
	private float tiempo = 0;    //temporizador entre cambio de seleccion
	public List<Button> botones; //Lista de botones
	private Color colorOriginal; 
	public OVRInput.RawAxis2D controller;
	public OVRInput.RawButton gatillo;
	// Start is called before the first frame update
	void Start()
    {
		n = botones.Count;
		index = 0;
		colorOriginal = botones[0].GetComponent<Image>().color;
		botones[0].GetComponent<Image>().color = Color.gray;
	}

    // Update is called once per frame
    void Update()
    {
		//Iterar entre opciones 
		tiempo += Time.deltaTime;
		if (tiempo >= 0.25)
		{
			tiempo = 0;
			float input = OVRInput.Get(controller).x;
			if (input <= -0.5)
			{
				preindex = index;
				//Comprobar posicion en lista
				if (index == 0)
				{
					index = n - 1;
				}
				else
				{
					index--;
				}
				Actualizar();

			}
			else if (input >= 0.5)
			{
				preindex = index;
				//Comprobar posicion en lista
				if (index == n - 1)
				{
					index = 0;
				}
				else
				{
					index++;
				}
				Actualizar();
			}

		}
		//Seleccionar opcion
		if (OVRInput.GetDown(gatillo))
		{
			botones[index].onClick.Invoke();
		}
	}

	//Funcion para actualizar la UI
	private void Actualizar()
	{
		botones[preindex].GetComponent<Image>().color = colorOriginal;
		botones[index].GetComponent<Image>().color =  Color.gray;
	}
}
