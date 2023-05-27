using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTamaño : MonoBehaviour
{
	public MenuSeleccionar seleccion;
	// Update is called once per frame
	void Update()
    {
		//obtener input de la palanca derecha
		float input = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
		//Si hay un objeto seleccionado
		if (seleccion.seleccionado != null && seleccion.seleccionado.tag=="Selectable")
		{
			//Ajustar tamaño
			if (input != 0)
			{
				input = input * Time.deltaTime;
				Vector3 add = new Vector3(input, input, input);
				seleccion.seleccionado.localScale += add;
			}
		}
    }
}
