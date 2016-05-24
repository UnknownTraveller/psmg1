using UnityEngine;
using System.Collections;

public class PowerConsole : MonoBehaviour {

    public Door _Door;
    public Behaviour _InputBehaviour;
    public GameObject _InteractHelp;

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
        if (_InteractHelp != null) _InteractHelp.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        _Triggered = false;
        if (_InteractHelp != null) _InteractHelp.SetActive(false);
    }

    void Update () {
	    if(_Triggered && !_Door.Moving && _Input.Interact)
        {
            if (_Beep != null) _Beep.Play();
            _Door.ToggleDoor();  
        }
	}
}
