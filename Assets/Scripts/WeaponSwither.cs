using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwither : MonoBehaviour
{
    public GameObject[] weapons;
    public int currentWeaponIndex = 0;


    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousWeaponSelectedIndex = currentWeaponIndex;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeaponIndex >= weapons.Length - 1)
            {
                currentWeaponIndex = 0;
            }
            else
                currentWeaponIndex++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeaponIndex <= 0)
            {
                currentWeaponIndex = weapons.Length - 1;
            }
            else
                currentWeaponIndex--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
        {
            currentWeaponIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
        {
            currentWeaponIndex = 2;
        }

        if (previousWeaponSelectedIndex != currentWeaponIndex)
        {
            SelectWeapon();
        }

    }


    void SelectWeapon()
    {
        int i = 0;
        foreach (var weapon in weapons)
        {
            if (i == currentWeaponIndex)
            {
                weapon.SetActive(true);
            }
            else
                weapon.SetActive(false);

            i++;
        }
    }

}
