using UnityEngine;

//shamelessly copied from our main project (all these are still by me).

/**
    Uses Unity's built-in InputManager to provide an IInput implementation for our game.
**/
public class UnityInput : MonoBehaviour, IInput
{
    public string _HorizontalAxis = "Horizontal";
    public string _VerticalAxis = "Vertical";
    public string _InteractButton = "Interact";

    public float Horizontal { get { return enabled ? Input.GetAxis(_HorizontalAxis) : 0; } }
    public float Vertical { get { return enabled ? Input.GetAxis(_VerticalAxis) : 0; } }
    public bool Interact { get { return Input.GetButtonDown(_InteractButton); } }
}