using UnityEngine;

public class QuitGameButton : OnButtonClickBase<QuitGameButton>
{
    public override void OnClickFunction()
    {
        Application.Quit();
    }
}