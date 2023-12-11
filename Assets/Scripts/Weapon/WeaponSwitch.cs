using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] string buttonName;
     [SerializeField] GameObject[] weapons;
    [SerializeField] Transform handPos;
    int currentWeaponIndex = 0;
    GameObject currentWeapon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(buttonName))
        {
            SwitchWeapon(currentWeaponIndex + 1);
        }
    }

    void SwitchWeapon(int i)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeaponIndex = i;

        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        Debug.Log("Switch to weapon: " + currentWeaponIndex);
        currentWeapon = Instantiate(weapons[currentWeaponIndex], handPos);
    }
}
