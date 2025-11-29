
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : OnButtonClickBase<QuitGameButton>
{
    public override void OnClickFunction()
    {
        Application.Quit();
    }
}
