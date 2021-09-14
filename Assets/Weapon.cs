using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public float damage = 50f;
    public float range = 100f;
    public Camera fpsCam;
    
    public GameObject impactEffect;
    public float impactForce =30f;
    public float fireRate = 20f;

    private float nextTimeToFire = 0f;


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();

        }



    }

    void Shoot()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }
          GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

    }
}

