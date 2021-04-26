using UnityEngine;
using UnityEngine.UI;

public class PlayerInfectionBar : MonoBehaviour
{
    public Slider slider;

    private void OnGUI()
    {
        slider.value = Mathf.Clamp01(WorldController.infectionStatus);
    }
}