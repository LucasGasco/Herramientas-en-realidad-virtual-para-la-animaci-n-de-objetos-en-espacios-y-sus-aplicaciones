using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class elementoLista : MonoBehaviour
{
	public GameObject icono;
	public Image color;
	public Text n;
	private GameObject objeto;

	public void Actualizar(int i,int index, GameObject referencia){
		n.text = "ID: "+i.ToString();
		icono.transform.GetChild(index).gameObject.SetActive(true);
		objeto = referencia;
	}

	public void ActualizarColor(Color col)
	{
		color.color = col;
	}

	public void ActualizarN(int i)
	{
		n.text = i.ToString();
	}

	private void Update()
	{
		color.color = objeto.GetComponent<Renderer>().material.color;
	}

	public Transform Referencia()
	{
		return objeto.transform;
	}
}
