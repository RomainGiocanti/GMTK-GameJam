using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControl : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    bool mainMenuBool = true;
    bool controlsMenuBool = false;
    
    public void MainMenuControls()
    {
        if(mainMenuBool == true)
        {
            mainMenu.SetActive(false);
            controlsMenu.SetActive(true);
            controlsMenuBool = true;
            mainMenuBool = false;
        }
        else if(controlsMenuBool == true)
        {
            controlsMenu.SetActive(false);
            mainMenu.SetActive(true);
            mainMenuBool = true;
            controlsMenuBool = false;
        }
    }
}
