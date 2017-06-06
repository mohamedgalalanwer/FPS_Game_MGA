using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxUpdate : MonoBehaviour {
    public Material skyBox;
    public float n1, n2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        skyBox.SetFloat("_AtmosphereThickness", Mathf.Repeat(Time.time / n1, n2));
    
	}
}
