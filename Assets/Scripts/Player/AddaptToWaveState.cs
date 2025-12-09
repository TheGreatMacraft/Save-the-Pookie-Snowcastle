using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddaptToWaveState : MonoBehaviour
{
    public GameObject gun;
    public GunBaseClass shootingScript;

    public BuildingDefenses buildingDefensesScript;

    private void Start()
    {
        // Setup Gun Script
        if(shootingScript == null)
        {
            shootingScript = GetComponentInChildren<GunBaseClass>();
            gun = shootingScript.gameObject;
        }

        // Subscribe to Even OnWaveStateChanged - Called after A Wave and at Beggining of New Wave
        WaveHandling.instance.onWaveStateChanged += WaveStateChanged;
    }

    private void WaveStateChanged(bool isWave)
    {
        // Set Gun Activity
        gun.SetActive(isWave);
        shootingScript.enabled = isWave;

        // Set Building Script
        buildingDefensesScript.enabled = !isWave;
        if(!isWave)
            buildingDefensesScript.StopBuilding();
    }
}
