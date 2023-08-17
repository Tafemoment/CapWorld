using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameState
{

    //current state of the game
    public GameStates currentGameState = GameStates.UIViewLock;
    public static void ChangeGameState()
    {
        switch (currentGameState)
        {
            case GameStates.UIViewLock:
                break;
                case GameStates.UIViewLock;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
                case GameStates.InPlay;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameStates.RemovedFromPlay;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            default:
                break;
        }
        currentGameState = state;
    }
}
    //public enum that is global coz its not inside a class
    public enum GameStates
    {
        UIViewLock,
        InPlay,
        RemovedFromPlay
    }