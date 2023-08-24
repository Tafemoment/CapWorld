using UnityEngine;
public static class GlobalGameState
{

    //current state of the game
    public static GameStates currentGameState = GameStates.UIViewLock;
    public static void ChangeGameState(GameStates state)
    {
        switch (state)
        {
            case GameStates.UIViewLock:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameStates.InPlay:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameStates.RemovedFromPlay:
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