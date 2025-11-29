using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : OnButtonClickBase<PlayGameButton>
{
    public string sceneName;

    public override void OnClickFunction()
    {
        SceneManager.LoadScene(sceneName);
    }

}
