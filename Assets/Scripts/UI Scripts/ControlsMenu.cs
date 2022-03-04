using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    public Sprite pcControls;
    public Sprite xboxControls;

    bool isPcControls = true;

    public Image controlsImage;

    public void ChangeControlScheme()
    {
        isPcControls = !isPcControls;
        ChangeImage();
    }

    void ChangeImage() 
    {
        if (isPcControls)
        {
            controlsImage.sprite = pcControls;
        }
        else
        {
            controlsImage.sprite = xboxControls;
        }
    }

}
