using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 50f;
    public float impactForce = 60f;


    public Camera cam;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip shoot, reload;

    private float nextTimeToFire = 0f;
    private bool isShooting = false;

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            StartCoroutine(Shoot());
            Debug.Log("Shoot!");
            return;

        }

        //if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        //{
        //    nextTimeToFire = Time.time + 1f / fireRate;
        //    StartCoroutine(Shoot());
        //    Debug.Log("Shoot!");
        //    return;

        //}

    }

    IEnumerator Shoot()
    {
        isShooting = true;
        audioSource.clip = shoot;
        audioSource.Play();

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        animator.SetBool("ShootShotgun", true);

        yield return new WaitForSeconds(0.45f);

        audioSource.clip = reload;
        audioSource.Play();

        yield return new WaitForSeconds(0.45f);
        animator.SetBool("ShootShotgun", false);
        isShooting = false;

    }


}
