using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public float width = 50;
    public float height = 5;
    public Color colorRemaining = Color.green;
    public Color colorBorder = Color.black;

    private void OnGUI()
    {
        if (true) //!GameOverToggler.triggered)
        {
            Enemy enemy = transform.parent.GetComponent<Enemy>();
            if (enemy)
            {
                if (!Mathf.Approximately(enemy.entity.health, enemy.entity.maxHealth))
                {
                    var pos = Camera.main.WorldToScreenPoint(transform.position);
                    Rect rect = new Rect(pos.x - width / 2, Camera.main.pixelHeight - pos.y, width, height);
                    IMUIHelper.DrawFilledBorderRect(rect, 1, enemy.entity.health / enemy.entity.maxHealth, colorBorder,
                        colorRemaining);
                }
            }
        }
    }
}