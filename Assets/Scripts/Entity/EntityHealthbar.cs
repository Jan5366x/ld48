using UnityEngine;

public class EntityHealthbar : MonoBehaviour
{
    public float width = 50;
    public float height = 5;
    public Color colorRemaining = Color.green;
    public Color colorBorder = Color.black;

    private void OnGUI()
    {
        if (true) //!GameOverToggler.triggered)
        {
            Enemy enemy = transform.parent.GetComponentInChildren<Enemy>();
            if (enemy)
            {
                DrawHealthBar(enemy.entity);
                return;
            }

            PlayerBuilding playerBuilding = transform.parent.GetComponentInChildren<PlayerBuilding>();
            if (playerBuilding)
            {
                DrawHealthBar(playerBuilding.entity);
            }
        }
    }

    private void DrawHealthBar(EntityData entityData)
    {
        if (!Mathf.Approximately(entityData.health, entityData.maxHealth))
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position);
            Rect rect = new Rect(pos.x - width / 2, Camera.main.pixelHeight - pos.y, width, height);
            IMUIHelper.DrawFilledBorderRect(rect, 1, entityData.health / entityData.maxHealth, colorBorder,
                colorRemaining);
        }
    }
}