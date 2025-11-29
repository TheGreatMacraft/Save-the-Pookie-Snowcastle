using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeGameButton : OnButtonClickBase<ResumeGameButton>
{
    public GameObject pauseMenuCanvas;
    public override void OnClickFunction()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeInHierarchy);
    }
}
