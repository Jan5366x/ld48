using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : MonoBehaviour
{
    public Camera Camera;

    public GameObject Top;
    public GameObject Bottom;
    
    
    public Slider Health;
    public Slider Stamina;
    public Slider InfectionState;

    public TMP_Text CoinCounter;
    public TMP_Text InfectedTime;
    
    // Start is called before the first frame update
    private void Start()
    {
        this.SetScaleUiElements();
        this.SetStateValues(1f, 1f, 1f);
        this.CoinCounter.text = "0";
        this.InfectedTime.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        // Test
        this.InfectedTime.text = $"{DateTime.Now:hh:mm:ss}";
        this.SetScaleUiElements();
    }

    private void SetScaleUiElements()
    {
        this.Top.ScaleByAspectAndPixelHeight(this.Camera, 1000);
        this.Bottom.ScaleByAspectAndPixelHeight(this.Camera, 1000);
    }

    private void SetStateValues(float health, float stamina, float infectionState)
    {
        SetValueIfBetweenMinMax(this.Health, health);
        SetValueIfBetweenMinMax(this.Stamina, stamina);
        SetValueIfBetweenMinMax(this.InfectionState, infectionState);

        static void SetValueIfBetweenMinMax(Slider slider, float value)
        {
            if (value >= slider.minValue && value <= slider.maxValue)
            {
                slider.value = value;   
            }
        }
    }
}
