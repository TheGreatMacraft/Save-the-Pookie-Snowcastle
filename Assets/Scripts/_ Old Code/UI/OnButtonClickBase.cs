using UnityEngine;
using UnityEngine.UI;

public abstract class OnButtonClickBase<C> : MonoBehaviour where C : OnButtonClickBase<C>
{
    public Button resumeGameButton;

    private void Awake()
    {
        resumeGameButton = GetComponent<Button>();
        resumeGameButton.onClick.AddListener(() => OnClickFunction());
    }

    public virtual void OnClickFunction()
    {
    }
}