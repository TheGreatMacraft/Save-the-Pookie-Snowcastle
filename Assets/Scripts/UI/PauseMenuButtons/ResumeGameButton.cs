using UnityEngine;

public class ResumeGameButton : OnButtonClickBase<ResumeGameButton>
{
    public GameObject pauseMenuCanvas;

    public override void OnClickFunction()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeInHierarchy);
    }
}