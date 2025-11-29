using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OnButtonClickBase<C> : MonoBehaviour where C : OnButtonClickBase<C>
{
    public Button resumeGameButton;

    public virtual void OnClickFunction() { }

    private void Awake()
    {
        resumeGameButton = GetComponent<Button>();
        resumeGameButton.onClick.AddListener(() => OnClickFunction());
    }
}
