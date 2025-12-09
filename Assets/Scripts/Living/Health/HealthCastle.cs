using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCastle : BaseHealth
{
    public static HealthCastle Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public override void Die()
    {
        //Game Over
    }
}
