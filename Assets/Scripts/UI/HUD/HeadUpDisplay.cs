using System;
using TMPro;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : MonoBehaviour
{
    public Camera Camera;

    #region Other content

    public GameObject BreakMenu;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        this.SetScaleUiElements();
        this.SetStateValues(1f, 1f, 1f);
        this.CoinCounter.text = "0";
        this.InfectedTime.text = "00:00:00";
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: Muss noch abgeglichen werden mit IMPUT
        if (Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            this.BreakMenu.SetActive(true);
            return;
        }

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

    #region Game content

    public GameObject Top;
    public GameObject Bottom;

    public Slider Health;
    public Slider Stamina;
    public Slider InfectionState;

    public TMP_Text CoinCounter;
    public TMP_Text InfectedTime;

    #endregion
}