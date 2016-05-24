using UnityEngine;
using System.Collections;

public class PowerConsole : MonoBehaviour {

    public Door _Door;
    public Behaviour _InputBehaviour;

    private bool _Triggered;

    private IInput _Input;
    private AudioSource _Beep;

    void Start()
    {
        _Input = (IInput)_InputBehaviour;
        _Beep = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        _Triggered = true;
    }

    void OnTriggerExit(Collider other)
    {
        _Triggered = false;
    }

	void Update () {
	    if(_Triggered && _Input.Interact && !_Door.Moving)
        {
            if (_Beep != null)
            {
                _Beep.Play();
            }

            _Door.ToggleDoor();
        }
	}
}
