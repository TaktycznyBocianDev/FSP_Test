using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootgunScript : GunScript
{
    public float soundPlayOffset = 0.10f;

    override protected IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponAudioSource.clip.length - soundPlayOffset);

        weaponAnimator.SetBool(animationVariable, true);

        weaponAudioSource.clip = reloadSound;
        weaponAudioSource.Play();


        yield return new WaitForSeconds(reloadTime);

        weaponAnimator.SetBool(animationVariable, false);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

}
