using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    public abstract void ScaleElements(float aspect);
    public abstract void Show();

    public abstract void Hide();
}