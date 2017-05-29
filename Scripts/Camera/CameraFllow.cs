using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour {
    public Transform target;
    public float soomthing = 5f;
    Vector3 offest;

	// Use this for initialization
	void Start () {
        offest = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraPos = target.position + offest;
        transform.position = Vector3.Lerp(transform.position, cameraPos, soomthing * Time.deltaTime);
	}
}
