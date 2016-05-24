using UnityEngine;
using System.Collections;

/**
    Player Movement, basically.
**/

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    public Behaviour _InputBehaviour;

    [Range(0f, 1f)]
    public float _MovementThreshold = 0.2f;

    [Range(0f, 100f)]
    public float _MoveSpeed = 5f;

    [Range(0f, 100f)]
    public float _RotateSpeed = 20f;

    public AnimationCurve _SpeedOverHealth = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 0f));

    private Rigidbody _Rigidbody;
    private IInput _Input;
    private Health _Health;

	void Start () {
        _Rigidbody = GetComponent<Rigidbody>();
        _Input = (IInput) _InputBehaviour;
        _Health = GetComponent<Health>();
	}
	
	void FixedUpdate () {
        // this also rotates our movement by 90 degrees, because our camera angle is weird, this feels more natural
        var move = new Vector3(-_Input.Vertical, 0f, _Input.Horizontal);
        if (move.sqrMagnitude < _MovementThreshold) return;

        if (move.sqrMagnitude > 1f) move.Normalize();

        if (_Health != null)
        {
            move *= (_MoveSpeed * _SpeedOverHealth.Evaluate(1f - (float)_Health.Value / _Health._InitialValue));
        } else
        {
            move *= _MoveSpeed;
        }

        if (_Rigidbody.velocity.sqrMagnitude < move.sqrMagnitude)
        {
            _Rigidbody.AddForce(move, ForceMode.Impulse);
        }

        // this is not exactly the input scheme proposed;
        // I think it makes the game easier to control (press the stick/WASD in the direction you want to go)

        var rotation = Quaternion.LookRotation(move, Vector3.up);
        _Rigidbody.MoveRotation(Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.fixedDeltaTime * _RotateSpeed));
	}
}
