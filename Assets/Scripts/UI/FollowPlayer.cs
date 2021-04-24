using Unity.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public Rect nothingEdge;
    public Rect softEdge;
    public Rect hardEdge;

    public float speedSoft = 5;
    public float speedHard = 12;

    [ReadOnly] public float speed;
    [ReadOnly] public Vector2 dir;

    private static Vector2 centerNormalized = new Vector2(0.5f, 0.5f);

    void FixedUpdate()
    {
        if (player)
        {
            speed = 0;
            Camera camera = GetComponentInChildren<Camera>();
            var screenRatio = camera.WorldToScreenPoint(player.transform.position) /
                              new Vector2(Screen.width, Screen.height);
            if (nothingEdge.Contains(screenRatio))
            {
                speed = 0;
            }
            else if (softEdge.Contains(screenRatio))
            {
                speed = Mathf.Lerp(0, speedSoft, getMaxRectRatio(nothingEdge, softEdge, screenRatio));
            }
            else if (hardEdge.Contains(screenRatio))
            {
                speed = Mathf.Lerp(speedSoft, speedHard, getMaxRectRatio(softEdge, hardEdge, screenRatio));
            }
            else
            {
                speed = speedHard;
            }

            Vector3 delta = player.transform.position - transform.position;
            dir = delta.normalized * (speed * Time.fixedDeltaTime);
            transform.Translate(dir.x, dir.y, 0);
        }
    }

    private float getMaxRectRatio(Rect innerRect, Rect outerRect, Vector2 point)
    {
        Vector2 deltaPoint = (point - centerNormalized);
        float deltaX = Mathf.Abs(deltaPoint.x);
        float deltaY = Mathf.Abs(deltaPoint.y);

        float innerWidth = innerRect.width / 2;
        float innerHeight = innerRect.height / 2;
        float outerWidth = outerRect.width / 2;
        float outerHeight = outerRect.height / 2;

        float tX = Mathf.InverseLerp(innerWidth, outerWidth, deltaX);
        float tY = Mathf.InverseLerp(innerHeight, outerHeight, deltaY);

        return Mathf.Max(tX, tY);
    }
}