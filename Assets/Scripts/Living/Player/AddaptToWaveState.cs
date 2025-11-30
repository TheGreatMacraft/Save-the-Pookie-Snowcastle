using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddaptToWaveState : MonoBehaviour
{
    public GameObject gun;
    public ShootingSnowballsPlayer shootingScript;

    public BuildingDefenses buildingDefensesScript;

    private void WaveStateChanged(bool isWave)
    {
        gun.SetActive(isWave);
        shootingScript.enabled = isWave;

        buildingDefensesScript.enabled = !isWave;
        if(!isWave)
            buildingDefensesScript.StopBuilding();
    }

    private void Start()
    {
        WaveHandling.instance.onWaveStateChanged += WaveStateChanged;
    }
}
