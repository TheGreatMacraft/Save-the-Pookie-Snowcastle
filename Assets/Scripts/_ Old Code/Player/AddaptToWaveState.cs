using UnityEngine;

public class AddaptToWaveState : MonoBehaviour
{
    public GameObject weapon;
    public AttackActions playerAttackScript;

    public BuildingDefenses buildingDefensesScript;

    private void Start()
    {
        // Setup Weapon Script
        if (playerAttackScript == null)
        {
            playerAttackScript = GetComponentInChildren<AttackActions>();
            weapon = playerAttackScript.gameObject;
        }

        // Subscribe to Even OnWaveStateChanged - Called after A Wave and at Beggining of New Wave
        WaveHandling.instance.onWaveStateChanged += WaveStateChanged;
    }

    private void WaveStateChanged(bool isWave)
    {
        // Set Weapon Activity
        weapon.SetActive(isWave);
        playerAttackScript.enabled = isWave;

        // Set Building Script
        buildingDefensesScript.enabled = !isWave;
        if (!isWave)
            buildingDefensesScript.StopBuilding();
    }
}