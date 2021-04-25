using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetup : MonoBehaviour
{
    public Slider sliderVolume;

    public TMP_Text volumeText;
    
    public void SetVolume()
    {
        var d = -10f - -96f;
        var vol = 100 - this.sliderVolume.value / d * 100f;
        
        this.volumeText.text = $"{vol:000}";
    }
}