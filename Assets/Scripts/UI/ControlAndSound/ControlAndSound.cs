using System.Collections;
using System.Collections.Generic;
using UI.MainMenu;
using UnityEngine;

public class ControlAndSound : MonoBehaviour
{
    public Camera Camera;
    
    public GameObject SoundOption;
    public GameObject ControlDescriptions;
    
    // Start is called before the first frame update
    void Start()
    {
        this.ScaleMenuItems();
    }

    // Update is called once per frame
    void Update()
    {
        
        // TODO: nur beim testen nötig
        this.ScaleMenuItems();
    }
    
    private void ScaleMenuItems()
    {
        var aspect = this.Camera.aspect;
        this.SoundOption.ScaleByAspect(aspect);
        this.ControlDescriptions.ScaleByAspect(aspect);
    }

    public void ButtonShowControlAndSound()
    {
        this.gameObject.SetActive(true);
    }
    
    public void ButtonHideControlAndSound()
    {
        this.gameObject.SetActive(false);
    }
}
