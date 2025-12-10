using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddaptToWaveState : MonoBehaviour
{
    public GameObject weapon;
    public WeaponBaseClass weaponScript;

    public BuildingDefenses buildingDefensesScript;

    private void Start()
    {
        // Setup Weapon Script
        if(weaponScript == null)
        {
            weaponScript = GetComponentInChildren<WeaponBaseClass>();
            weapon = weaponScript.gameObject;
        }

        // Subscribe to Even OnWaveStateChanged - Called after A Wave and at Beggining of New Wave
        WaveHandling.instance.onWaveStateChanged += WaveStateChanged;
    }

    private void WaveStateChanged(bool isWave)
    {
        // Set Weapon Activity
        weapon.SetActive(isWave);
        weaponScript.enabled = isWave;

        // Set Building Script
        buildingDefensesScript.enabled = !isWave;
        if(!isWave)
            buildingDefensesScript.StopBuilding();
    }
}
