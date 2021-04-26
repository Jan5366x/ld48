using UnityEngine;

/// <summary>
/// Basis aller eingesetzter UI Elemente für eine Einheitliche Darstellung der Inhalte.
/// Wird für die obere Instance zu einem Menü abschnitt eingesetzt, die hier entsprechend in Ordner aufgeteilt wurde.
/// </summary>
public abstract class BaseMenu : MonoBehaviour
{
    /// <summary>
    /// Übergibt die aktuelle Bildschirm verhältnis für die skalierung der UI Elemente. 
    /// </summary>
    /// <param name="aspect">Bildschirmverhältnis</param>
    public abstract void ScaleElements(float aspect);
    
    /// <summary>
    /// Schaltet den Menubereich ein. 
    /// Weitere Untergeordnete Elemente aus dem Bereich sollten in die Überschriebene Methode mit eingetragen werden.
    /// </summary>
    public abstract void Show();

    /// <summary>
    /// Schaltet den Menuebereich aus. 
    /// Untergeordnete Inhalte aus dem Menuebereich koennten hier ebenfalls bearbeitet werden.
    /// </summary>
    public abstract void Hide();
}