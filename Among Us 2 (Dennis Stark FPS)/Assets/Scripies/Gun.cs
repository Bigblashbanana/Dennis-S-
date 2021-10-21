using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100F;
    public float knockBack = 30f;
    public float fireRate = 15f;
    public Camera fpsCam;

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public ParticleSystem muzzleFlash;
    public ParticleSystem bullet;
    public GameObject impactEffect;
    public AudioSource shooting;

    private float nextTimeToFire = 0f;

    
    void Start()
    {
        currentAmmo = maxAmmo;
    }


    void Update()
    {
        if (isReloading)
            return;


        if (currentAmmo <= 0)
        {
          StartCoroutine(Reload());
            return;
        }



        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            Shoot();

        }
        


    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);


        currentAmmo = maxAmmo;
        isReloading = false;
    }



    void Shoot ()
    {
        muzzleFlash.Play();
        bullet.Play();
        shooting.Play();

        currentAmmo--; 

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if (hit.rigidbody  != null)
            {
                hit.rigidbody.AddForce(-hit.normal * knockBack); 
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
