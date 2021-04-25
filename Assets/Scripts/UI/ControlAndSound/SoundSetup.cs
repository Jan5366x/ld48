using UnityEngine;
using UnityEngine.UI;

public class SoundSetup : MonoBehaviour
{
    public Slider SliderVolume;

    public void SetVolume()
    {
        Debug.Log($"Volume: {AudioListener.volume}, Slider: {this.SliderVolume.value}");
        AudioListener.volume = this.SliderVolume.value;
    }
}