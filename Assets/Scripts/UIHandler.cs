using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler 
{
    /*
     Method or function?
     */
    /*
     How would you reference or use the below function in side MainMenu.cs?
     */
    public static Rect ScreenPlacement(float startPositionX, float startPositionY, float sizeX, float sizeY)
    {
        Rect placement = new Rect(startPositionX * Screen.width / 16, startPositionY * Screen.height / 9, sizeX * Screen.width / 16, sizeY * Screen.height / 9);
        return placement;
    }
}
