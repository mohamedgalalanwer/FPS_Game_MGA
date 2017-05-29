using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {


    // put the script on  eny enemy 


    [SerializeField] private float health = 100;	// Use this for initialization
	public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject,0.1f);
        }
        //you can put the animation death of the eniems here 



    }
}
