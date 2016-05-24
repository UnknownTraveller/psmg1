using UnityEngine;

// shamelessly copied from our main project (all these are still by me).

/**
    Allows to add simple idle or movement animations to an object.
    Can stack with other animations, or multiple instances of itself.
    Will directly manipulate the transform, so use with caution on Rigidbodies.
**/
public class MovementBobbing : MonoBehaviour
{

    /**
        If this is set, the movements of this Rigidbody will be used as an input to the animation.
        If this is not set, this will become an idle animation.
    **/
    public Rigidbody _Rigidbody;

    public AnimationCurve _Curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.25f, 1f), new Keyframe(0.5f, 0f),
                                                                new Keyframe(0.75f, -1.0f), new Keyframe(1.0f, 0.0f));

    public bool _EnableMovement = true;
    public Vector3 _MovementScaling = new Vector3(0.1f, 0.2f, 0f);
    public Vector3 _MovementTimeScaling = new Vector3(1.0f, 1.0f, 1.0f);

    public bool _EnableRotation = false;
    public Vector3 _RotationScaling = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 _RotatationTimeScaling = new Vector3(1.0f, 1.0f, 1.0f);

    private float _CurveLength;

    private Vector3 _MovementDistance = new Vector3();
    private Vector3 _RotationDistance = new Vector3();
    private Vector3 _LastPositionValues = new Vector3();
    private Quaternion _LastRotationValues = new Quaternion();

    /// helper function to not care about divisions by 0. this can be optimized heavily, but it's not a hot path.
    private float NaNTo0(float x)
    {
        if (float.IsNaN(x) || float.IsInfinity(x))
        {
            return 0f;
        } else
        {
            return x;
        }
    }

    void Start()
    {
        _CurveLength = _Curve[_Curve.length - 1].time;
    }

    // TODO: possible bug right there if Update and FixedUpdate don't match??

    void Update()
    {
        if (_EnableMovement)
        {
            Vector3 positionValues = new Vector3(
                _MovementScaling.x * _Curve.Evaluate(_MovementDistance.x),
                _MovementScaling.y * _Curve.Evaluate(_MovementDistance.y),
                _MovementScaling.z * _Curve.Evaluate(_MovementDistance.z));

            Vector3 positionDelta = positionValues - _LastPositionValues;

            gameObject.transform.localPosition += positionDelta;

            _LastPositionValues = positionValues;
        }
        if (_EnableRotation)
        {
            Quaternion rotationValues = Quaternion.Euler(
                _RotationScaling.x * _Curve.Evaluate(_RotationDistance.x),
                _RotationScaling.y * _Curve.Evaluate(_RotationDistance.y),
                _RotationScaling.z * _Curve.Evaluate(_RotationDistance.z));

            Quaternion rotationDelta = rotationValues * Quaternion.Inverse(_LastRotationValues);

            gameObject.transform.localRotation *= rotationDelta;
            _LastRotationValues = rotationValues;
        }        
    }

    void FixedUpdate()
    {
        float dP;
        if (_Rigidbody != null)
        {
            dP = _Rigidbody.velocity.magnitude * Time.fixedDeltaTime * _CurveLength;
        } else
        {
            dP = Time.fixedDeltaTime * _CurveLength;
        }

        _MovementDistance += new Vector3(NaNTo0(dP / _MovementTimeScaling.x), NaNTo0(dP / _MovementTimeScaling.y), NaNTo0(dP / _MovementTimeScaling.z));
        _RotationDistance += new Vector3(NaNTo0(dP / _RotatationTimeScaling.x), NaNTo0(dP / _RotatationTimeScaling.y), NaNTo0(dP / _RotatationTimeScaling.z));

        if (_MovementDistance.x > _CurveLength) _MovementDistance.x -= _CurveLength;
        if (_MovementDistance.y > _CurveLength) _MovementDistance.y -= _CurveLength;
        if (_MovementDistance.z > _CurveLength) _MovementDistance.z -= _CurveLength;

        if (_RotationDistance.x > _CurveLength) _RotationDistance.x -= _CurveLength;
        if (_RotationDistance.y > _CurveLength) _RotationDistance.y -= _CurveLength;
        if (_RotationDistance.z > _CurveLength) _RotationDistance.z -= _CurveLength;
    }

}
