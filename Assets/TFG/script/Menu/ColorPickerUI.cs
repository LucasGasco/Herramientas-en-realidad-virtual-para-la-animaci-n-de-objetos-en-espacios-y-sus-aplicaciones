using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorPickerUI : MonoBehaviour
{
	int index,preindex;
	float tiempo = 0;
	public Transform lista;
	public List<Material> color;
	public MenuSeleccionar seleccionar;
	public TextMesh ui;
	int n;
	// Start is called before the first frame updateç
	private void Start()
	{
		n = color.Count;
		index = 0;
		preindex = 0;
	}

	void Update()
    {
		//Iterar entre opciones 
		tiempo += Time.deltaTime;
		if (tiempo >= 0.25)
		{
			tiempo = 0;
			//Selecion horizontalmente
			float input = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x;
			if (input <= -0.5)
			{
				preindex = index;
				//Comprobar posicion en lista
				if (index <= 0)
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
				if (index >= n - 1)
				{
					index = 0;
				}
				else
				{
					index++;
				}
				Actualizar();
			}
			//Seleccion verticalmente
			input = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
			if (input <= -0.5)
			{
				preindex = index;
				//Comprobar posicion en lista
				if (index+5 > n-1)
				{
					index -= 10;
				}
				else
				{
					index+=5;
				}
				
				Actualizar();

			}
			else if (input >= 0.5)
			{
				preindex = index;
				//Comprobar posicion en lista
				if (index-5 < 0)
				{
					index += 10;
				}
				else
				{
					index-=5;
				}
				
				Actualizar();
			}

		}
		//Seleccionar color
		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
		{
			if (seleccionar.seleccionado != null && seleccionar.seleccionado.tag == "Selectable")
			{
				seleccionar.seleccionado.GetComponent<MeshRenderer>().material = color[index];
			}
		}
	}
	//Actualizar ui resaltando el seleccionado
	private void Actualizar()
	{
		lista.GetChild(preindex).gameObject.GetComponent<Outline>().enabled = false;
		lista.GetChild(index).gameObject.GetComponent<Outline>().enabled = true;
	}
}
