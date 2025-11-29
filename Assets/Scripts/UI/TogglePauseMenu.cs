using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;

    public KeyCode pauseMenuButton;

    private void Update()
    {
        if(Input.GetKeyDown(pauseMenuButton))
            pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeInHierarchy);
    }
}
