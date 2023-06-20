using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public abstract class GunScript : MonoBehaviour
{
    //Common Fields
    [Header("Fields common for every weapon")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 50f;
    public float impactForce = 60f;
    public float maxAmmo = 1f;
    public float reloadTime = 0.9f;
    public bool isAutomatic = true;
    public string animationVariable;

    [Header("Needed componenets")]
    public Camera playerCam;
    public AudioClip shootSound, reloadSound;


    protected Animator weaponAnimator;
    protected AudioSource weaponAudioSource;
    protected float nextTimeToFire = 0f;
    protected float currentAmmo = 0f;
    protected bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        weaponAnimator = GetComponent<Animator>();
        weaponAudioSource = GetComponent<AudioSource>();
        weaponAudioSource.Stop();
    }

    protected virtual void Update()
    {

        if (isReloading) return; //stop everything else if PLayer is reloading

        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }

        //If is automatic, then get button down OR get only button but then take time into account
        if ((Input.GetButtonDown("Fire1") && !isAutomatic) ||
            (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && isAutomatic))
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            Debug.Log("Shoot!");

        }

    }

    protected virtual IEnumerator Reload()
    {
        isReloading = true;

        weaponAnimator.SetBool(animationVariable, true);



        weaponAudioSource.clip = reloadSound;
        weaponAudioSource.Play();


        yield return new WaitForSeconds(reloadTime);

        weaponAnimator.SetBool(animationVariable, false);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    protected virtual void Shoot()
    {

        //weaponAudioSource.clip = shootSound;
        weaponAudioSource.PlayOneShot(shootSound);

        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
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

        currentAmmo--;

    }
}
