using System.Collections;
using UnityEngine;

[AddComponentMenu("Game Systems/Player/Mouse Look")]


public class MouseLook : MonoBehaviour
{
    #region RotationalAxis
    private enum RotationalAxis
    {
        MouseX,
        MouseY
    }
    #endregion
    #region Variables

    //private link to the rotational axis called _axis and set a default axis
    [SerializeField] private RotationalAxis _axis;
    //float for sensitivity
    public static float sensitivity = 10;
    //invert mouse positionto calculate mouse look correctly later
    private static bool invertMouse;
    //max and min Y rotation
    [SerializeField] private Vector2 _clamp = new Vector2(-60, 60);
    //private float for rotation of mouse Y
    private float _rotationMouseY;
    #endregion
    #region Start
    private void Start()
    {
        //if our game object has rigidbody
        if (GetComponent<Rigidbody>())
        {
            //set rigidbody freezeRotation to true
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        //check if script s on player object vs camera object to correctly setup movement
        if(gameObject.tag == "Player")
        {
            _axis = RotationalAxis.MouseX;
        }
        else //otherwise we am become camera, viewer of shit
        {
            _axis = RotationalAxis.MouseY;
        }
    }
    #endregion
}
