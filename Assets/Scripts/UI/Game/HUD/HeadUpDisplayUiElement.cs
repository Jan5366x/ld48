using System;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplayUiElement : BaseUiElement
{
    public Camera cam;
    public UiManager uiManager;

    public GameObject staminaMinLine;
    
    private void Start()
    {
        this.SetStateValues(1f, 1f, 1f);
        this.tmpCoinCounter.text = "0";
        this.tmpInfectedTime.text = "00:00:00";
    }
    
    private void Update()
    {
        var rectTransform = (RectTransform)this.sliderStamina.transform;
        var rect = rectTransform.rect;
        var middle = rect.width / 2f;
        var staminaStep = rect.width / Player.maxStamina;
        var shift = staminaStep * Player.minSprintStartStamina;
        var shiftToLeft = shift - middle;

        this.staminaMinLine.transform.localPosition = new Vector3(shiftToLeft, 0, 0);
        
        // TODO: Muss noch abgeglichen werden mit INPUT
        if (Input.GetKey(KeyCode.Escape))
        {
            this.uiManager.ButtonBreakMenuShow();
            return;
        }

        // TODO Only Test

        var space = Input.GetKey(KeyCode.Space);
        var leftControl = Input.GetKey(KeyCode.LeftControl);
        var leftAlt = Input.GetKey(KeyCode.LeftAlt);

        if (leftControl && space)
        {
            this.uiManager.ButtonScoreResult();
        }

        if (leftControl && leftAlt)
        {
            this.uiManager.ButtonGameOverShow();
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
    
    #region ueberschriebene methode aus BaseUiElement

    /// <inheritdoc cref="BaseUiElement"/>
    public override void ScaleElements(float aspect)
    {
        this.screenElementsTop.ScaleByAspectAndPixelHeightAndMinWidth(this.cam, 1000);
        this.screenElementsBottom.ScaleByAspectAndPixelHeightAndMinWidth(this.cam, 1000);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Show()
    {
        this.gameObject.SetActive(true);
    }

    /// <inheritdoc cref="BaseUiElement"/>
    public override void Hide()
    {
        this.gameObject.SetActive(false);
    }
    
    #endregion

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