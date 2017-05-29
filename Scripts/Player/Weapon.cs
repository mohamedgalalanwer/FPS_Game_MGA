using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour {
    //bulletes per each Magazine 
    public int bulletePreMag = 30;
    // total bulletes we have 
    public int bulletesLefet;
    //the current bullets in our Magazine 
    public int  currentBulletes;
    RaycastHit hit;
    //
    public float fireRate = 0.1f;
    //
    float fireTimer;

    public float range = 100;
    public Transform shootPoint;

    Animator anim;


    public ParticleSystem buzzleFlash;


    private AudioSource audio;
  public  AudioClip audioClip;



    //
    public GameObject hitParticaler;
    public GameObject bulletsImpact;

    /// <summary>
    /// //  cotroll the input and number of bullets
    /// </summary>
    public enum ShootMode{Auto,Semi }

    public ShootMode shootingMode;
    private bool shootInput;

    //

    public float damage = 20;

    //


    private Vector3 originPostion;
    public Vector3 aimPostion;
    public float loadSpeed;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
       currentBulletes=  bulletePreMag ;
       audio = GetComponent<AudioSource>();
        originPostion = transform.localPosition;
    }

  
	
	// Update is called once per frame
    void Update()
    {

        switch (shootingMode)
        {
            case ShootMode.Auto:
                shootInput = Input.GetButton("Fire1");
                break;

            case ShootMode.Semi:
                shootInput = Input.GetButtonDown("Fire1");
                break;




        }



        if (shootInput)
        {
            if (currentBulletes > 0)
            { Fire(); }
            else if(bulletesLefet>0)
            {

                Reload();
            }
        }
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;

        }

        AimSight();
    }

    private void AimSight()
    {
        if (Input.GetButton("Fire2"))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPostion, Time.deltaTime * loadSpeed);


        }
        else

        {

            transform.localPosition = Vector3.Lerp(transform.localPosition, originPostion, Time.deltaTime * loadSpeed);
        }
    }

    void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
       

        //if(info.IsName("Fire"))
        //    anim.SetBool("Fire", false);


    }
    private void Fire()
    {
        if (fireTimer < fireRate||currentBulletes<=0) return;

       
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name + "fire");
            // 


            MakeAHitEffect();
           
            if (hit.transform.GetComponent<HealthController>())
            {

                hit.transform.GetComponent<HealthController>().ApplyDamage(damage);


            }






        }

        currentBulletes--;
        fireTimer = 0.0f;// rest fire timer
        anim.CrossFadeInFixedTime("Fire", 0.01f);// play a fire animation
       // anim.SetBool("Fire", true);

        buzzleFlash.Play();
        PlayShootSound();

    }

    private void MakeAHitEffect()
    {
        GameObject hitParticalerEffect = Instantiate(hitParticaler, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        // hitParticalerEffect.transform.SetParent(hit.transform);
        GameObject BulletsImpact = Instantiate(bulletsImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
        Destroy(hitParticalerEffect, 2f);
        Destroy(BulletsImpact, 3f);

    }

    private void PlayShootSound()
    {// no cut the audio one by one 
        audio.PlayOneShot(audioClip);
        //audio.clip = audioClip;
        //audio.Play();



    }

   

    private void Reload() {
        if (bulletesLefet <= 0) return;
        int bulletesToLoad = bulletePreMag - currentBulletes;
        int bulletesToDecuted = (bulletesLefet >= bulletesToLoad) ? bulletesToLoad : bulletesLefet;
        bulletesLefet -= bulletesToDecuted;
        currentBulletes += bulletesToDecuted;
    
    
    
    }
}
