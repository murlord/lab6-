using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public float damage = 1f;
    public float range = 100f;
    public Camera fpsCam;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }



    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

    }
}

