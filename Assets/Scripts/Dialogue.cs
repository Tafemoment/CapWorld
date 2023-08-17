using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public bool showDialogue = false;
    public string speakersName = "";
    public string[] dialogueText;
    public int currentLineOfText = 0;

    //Method that we can access when called from another script
    public void OpenDialogue()
    {
        //trigger bool
        showDialogue = true;
        //reset int
        currentLineOfText = 0;
        //any other thingo required
        GlobalGameState.ChangeGameState(GameStates.UIViewLock);

    }

    //repeat for close
    public void CloseDialogue()
    {
        showDialogue= false;
        currentLineOfText = 0;
        GlobalGameState.ChangeGameState(GameStates.InPlay);
    }
}
