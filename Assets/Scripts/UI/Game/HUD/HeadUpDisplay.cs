using System;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : BaseMenu
{
    public Camera cam;
    public UiManager uiManager;

    private void Start()
    {
        this.SetStateValues(1f, 1f, 1f);
        this.tmpCoinCounter.text = "0";
        this.tmpInfectedTime.text = "00:00:00";
    }
    
    private void Update()
    {
        // TODO: Muss noch abgeglichen werden mit INPUT
        if (Input.GetKey(KeyCode.Escape))
        {
            this.uiManager.ButtonBreakMenuShow();
            return;
        }

        // Test
        this.tmpInfectedTime.text = $"{DateTime.Now:hh:mm:ss}";

        var space = Input.GetKey(KeyCode.Space);
        var leftControl = Input.GetKey(KeyCode.LeftControl);

        if (leftControl && space)
        {
            this.uiManager.ButtonScoreResult();
        }
    }
    
    

    private void SetStateValues(float health, float stamina, float infectionState)
    {
        SetValueIfBetweenMinMax(this.sliderHealth, health);
        SetValueIfBetweenMinMax(this.sliderStamina, stamina);
        SetValueIfBetweenMinMax(this.sliderInfectionState, infectionState);

        static void SetValueIfBetweenMinMax(Slider slider, float value)
        {
            if (value >= slider.minValue && value <= slider.maxValue)
            {
                slider.value = value;
            }
        }
    }

    public override void ScaleElements(float aspect)
    {
        this.screenElementsTop.ScaleByAspectAndPixelHeightAndMinWidth(this.cam, 1000);
        this.screenElementsBottom.ScaleByAspectAndPixelHeightAndMinWidth(this.cam, 1000);
    }

    public override void Show()
    {
        this.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }

    #region Game content

    public GameObject screenElementsTop;
    public GameObject screenElementsBottom;

    public Slider sliderHealth;
    public Slider sliderStamina;
    public Slider sliderInfectionState;

    public TMP_Text tmpCoinCounter;
    public TMP_Text tmpInfectedTime;

    #endregion
}