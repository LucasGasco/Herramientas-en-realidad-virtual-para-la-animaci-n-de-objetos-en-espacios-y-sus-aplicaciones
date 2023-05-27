using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSliders: MonoBehaviour
{
	public Slider sliders;
	public Text marcadores;
	
	// Start is called before the first frame update
	void Start()
    {
		sliders.GetComponentInChildren<Outline>(true).enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
		//Mover slider
		float input2 = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x;
		sliders.value += input2 * Time.deltaTime * 3;
		marcadores.text = sliders.value.ToString();
	}

}
