using UnityEngine;

//shamelessly copied from our main project (all these are still by me).
public class UnityInput : MonoBehaviour, IInput
{
    public string _HorizontalAxis = "Horizontal";
    public string _VerticalAxis = "Vertical";
    public string _InteractButton = "Interact";

    public float Horizontal { get { return Input.GetAxis(_HorizontalAxis); } }
    public float Vertical { get { return Input.GetAxis(_VerticalAxis); } }
    public bool Interact { get { return Input.GetButtonDown(_InteractButton); } }
}