using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    public Slider slider;

    private void OnGUI()
    {
        slider.value = Player.stamina / Player.maxStamina;
    }
}