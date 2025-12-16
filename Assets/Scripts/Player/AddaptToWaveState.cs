using UnityEngine;

public class AddaptToWaveState : MonoBehaviour
{
    public GameObject weapon;
    public WeaponBase playerWeaponScript;

    public BuildingDefenses buildingDefensesScript;

    private void Start()
    {
        // Setup Weapon Script
        if (playerWeaponScript == null)
        {
            playerWeaponScript = GetComponentInChildren<WeaponBase>();
            weapon = playerWeaponScript.gameObject;
        }

        // Subscribe to Even OnWaveStateChanged - Called after A Wave and at Beggining of New Wave
        WaveHandling.instance.onWaveStateChanged += WaveStateChanged;
    }

    private void WaveStateChanged(bool isWave)
    {
        // Set Weapon Activity
        weapon.SetActive(isWave);
        playerWeaponScript.enabled = isWave;

        // Set Building Script
        buildingDefensesScript.enabled = !isWave;
        if (!isWave)
            buildingDefensesScript.StopBuilding();
    }
}