using UnityEngine.UI;

public sealed class UISlider : Media
{
    private Slider slider;
    

    public UISlider(Slider slider)
    {
        this.slider = slider;
    }

    
    public void Update(float current, float max)
    {
        slider.value = current / max;
    }
}