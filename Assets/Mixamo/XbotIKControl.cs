using UnityEngine;

[RequireComponent(typeof(Animator))]
public class XbotIKControl : MonoBehaviour
{
    [SerializeField] Transform _raySource;
    [SerializeField] float _surfaceOffset = 0.1f;
    Animator _animator;

    [SerializeField] Transform _rightHandPoint;
    [SerializeField] Transform _leftHandPoint;
    [SerializeField] Transform _lookPoint;

    float _dist;

    RaycastHit hit;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 vec = _lookPoint.position - transform.position;
        if (Vector3.Distance(transform.position, _lookPoint.position) < 5 && Vector3.Angle(transform.forward, vec) < 100)
        {
            _animator.SetLookAtWeight(1);
            _animator.SetLookAtPosition(_lookPoint.position);
        }
        else _animator.SetLookAtWeight(0);

        Ray ray = new Ray(_raySource.position, Vector3.RotateTowards(transform.forward, transform.right, Mathf.Deg2Rad * 45, 0));
        Physics.Raycast(ray, out hit);
        _dist = hit.distance;
        if (_dist > 2)
        {
            WeightRightHand(0);
        }
        else if (_dist > 0)
        {
            _rightHandPoint.position = hit.point + (hit.normal * _surfaceOffset);

            WeightRightHand(1.3f - _dist);
            
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandPoint.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandPoint.rotation);
        }

        ray = new Ray(_raySource.position, Vector3.RotateTowards(transform.forward, transform.right, Mathf.Deg2Rad * -45, 0));
        Physics.Raycast(ray, out hit);
        _dist = hit.distance;
        if (_dist > 2)
        {
            WeightLeftHand(0);
        }
        else if (_dist > 0)
        {
            _leftHandPoint.position = hit.point + (hit.normal * _surfaceOffset);

            WeightLeftHand(1.3f - _dist);

            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandPoint.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandPoint.rotation);
        }

    }
    private void WeightRightHand(float val)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, val);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, val);
    }
    private void WeightLeftHand(float val)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, val);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, val);
    }
}
