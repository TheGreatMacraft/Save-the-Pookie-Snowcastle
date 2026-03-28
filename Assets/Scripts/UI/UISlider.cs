using UnityEngine.UI;

public class UISlider : Media
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

public class NullUISlider : Media
{
    public void Update(float current, float max) { }
}