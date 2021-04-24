using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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
