using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;

    private void OnGUI()
    {
        slider.value = Player.entity.health / Player.entity.maxHealth;
    }
}