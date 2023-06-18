using System.Collections;
using UnityEngine;

public abstract class GunScript : MonoBehaviour
{
    //Common Fields
    [Header("Fields common for every weapon")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 50f;
    public float impactForce = 60f;
    public float beforeReloadSoundTime = 0.45f;
    public float afterReloadSoundTime = 0.45f;
    public bool isAutomatic = true;

    //Common Fields
    [Header("Needed componenets")]
    public Camera playerCam;
    public Animator weaponAnimator;
    public AudioSource weaponAudioSource;
    public AudioClip shootSound, reloadSound;

    private float nextTimeToFire = 0f;
    private bool isShooting = false;

    protected virtual void Update()
    {

        if (Input.GetButtonDown("Fire1") && !isShooting && !isAutomatic)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            StartCoroutine(Shoot());
            Debug.Log("Shoot!");
            return;

        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && isAutomatic)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            StartCoroutine(Shoot());
            Debug.Log("Shoot!");
            return;

        }

    }

    IEnumerator Shoot()
    {
        isShooting = true;
        weaponAudioSource.clip = shootSound;
        weaponAudioSource.Play();

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
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

        weaponAnimator.SetBool("ShootShotgun", true);

        yield return new WaitForSeconds(beforeReloadSoundTime); 

        weaponAudioSource.clip = reloadSound;
        weaponAudioSource.Play();

        yield return new WaitForSeconds(afterReloadSoundTime);
        weaponAnimator.SetBool("ShootShotgun", false);
        isShooting = false;

    }


}
