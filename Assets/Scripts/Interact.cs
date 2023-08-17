using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interactRay;

            interactRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            RaycastHit hitInfo;

            if (Physics.Raycast(interactRay, out hitInfo, 10))
            {
                #region NPC tag
                if (hitInfo.collider.tag == "NPC")
                {
                    Debug.Log("Hello There: " + hitInfo.transform.name + " is talking to you");
                    //trigger the dialogue script
                    if (hitInfo.collider.GetComponent<Dialogue>())
                    {
                        hitInfo.collider.GetComponent<Dialogue>().OpenDialogue();
                    }
                }
                #endregion
                #region Item
                if (hitInfo.collider.tag == "Item")
                {
                    Debug.Log(hitInfo.transform.name);
                }
                #endregion
                #region Chest
                if (hitInfo.collider.tag == "Chest")
                {
                    Debug.Log(hitInfo.transform.name);
                }
                #endregion
            }
        }
    }
}
