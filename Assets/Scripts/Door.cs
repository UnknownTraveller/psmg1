using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public AnimationCurve _OpenAnimation = new AnimationCurve(new Keyframe(0f, 3f), new Keyframe(1f, 1.5f));
    public AnimationCurve _CloseAnimation = new AnimationCurve(new Keyframe(0f, 1.5f), new Keyframe(1f, 3f));

    private bool _IsOpen;
    private bool _Moving;

    private float _OpenAnimationLength;
    private float _CloseAnimationLength;
    private float _AnimationTime;

    public bool Moving { get { return _Moving; } }

    public void ToggleDoor()
    {
        if (!_Moving)
        {
            _Moving = true;
            _AnimationTime = 0f;
        }
    }

    public void Start()
    {
        _OpenAnimationLength = _OpenAnimation[_OpenAnimation.length - 1].time;
        _CloseAnimationLength = _CloseAnimation[_CloseAnimation.length - 1].time;
    }

    public void FixedUpdate()
    {
        if (_Moving)
        {
            _AnimationTime += Time.fixedDeltaTime;

            float animationLength = _IsOpen ? _CloseAnimationLength : _OpenAnimationLength;

            if (_AnimationTime > animationLength) _AnimationTime = animationLength;

            float animationValue = _IsOpen ? _CloseAnimation.Evaluate(_AnimationTime) : _OpenAnimation.Evaluate(_AnimationTime);

            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, animationValue, gameObject.transform.localPosition.z);

            if (_AnimationTime == 1)
            {
                _Moving = false;
                _IsOpen = !_IsOpen;
            }
        }
    }
}
