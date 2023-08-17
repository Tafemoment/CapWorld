using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinearDialogue : Dialogue
{
    private void OnGUi()
    {
        //if our dialogue is visible
        if (showDialogue)
        {
            //the dialogue box takes up the whole bottom 3rd of he screen and shows the NPC name and current dialogue line
            GUI.Box(UIHandler.ScreenPlacement(0, 6, 16, 3), speakersName+": "+ dialogueText[currentLineOfText]);

            //if not at the end o the dialogue
            if (currentLineOfText < dialogueText.Length-1)
            {
                //next button allows us to skip forward to the next line of dialogue
                if (GUI.Button(UIHandler.ScreenPlacement(15,8.5f,1,0.5f), "Next"))
                {
                    //incrementing currentLineIndex b 1 so that we go to next line
                    //currentLineOfText = currentLineOfText + 1; deprecated by next line
                    currentLineOfText++;
                }
            }
            else
            {
                //the leave button allows us to end dialogue
                if (GUI.Button(UIHandler.ScreenPlacement(10, 5, 0, 0.5f), "Ta"))
                {
                    CloseDialogue();
                }
            }
        }
    }

}
