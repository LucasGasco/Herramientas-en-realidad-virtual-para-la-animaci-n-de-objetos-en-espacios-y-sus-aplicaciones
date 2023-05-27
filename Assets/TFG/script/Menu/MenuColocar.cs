using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuColocar : MonoBehaviour
{
	public OVRInput.Controller controllerHand = OVRInput.Controller.None;
	public GameObject contenedor;	//objeto padre para almacenar los generados
	public List<GameObject> formas;  //lista de iconos para la ui
	public List<GameObject> prefabs;  // lista de prefabs de objetos a instanciar
	private float tiempo=1;
	int n,index,preindex;
	public GameObject mando;
	public Material basico, rojo;
	public MenuSeleccionar seleccionar;
	// Start is called before the first frame update
	void Start()
    {
		n = formas.Count;
		index = 0;
    }

    // Update is called once per frame
    void Update()
	{
		tiempo += Time.deltaTime;
		//Iterar entre objetos
		if(tiempo>=0.25)
		{
			tiempo = 0;
			float input = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x;
			if (input <= -0.5)
			{
				preindex = index;
				//Comprobar posicion en array
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
				//Comprobar posicion en array
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
		//Seleccionar
		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
		{
			GameObject inst = Instantiate(prefabs[index], transform);
			Transform aux = inst.transform;
			aux.parent = contenedor.transform;
			Grabar grabador = inst.GetComponent<Grabar>();
			grabador.mando = mando;
			grabador.rojo = rojo;
			grabador.basico = basico;
			grabador.seleccionar = seleccionar;
		}
	}
	//Funcion para actualizar la UI
	void ActualizarFigura()
	{
		formas[preindex].SetActive(false);
		formas[index].SetActive(true);
	}
}
