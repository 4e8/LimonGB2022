using UnityEngine;

[RequireComponent(typeof(Animator))]
public class xbotController : MonoBehaviour
{
    Animator _animator;
    string _idleStr = "idle";
    string _walkStr = "walk";
    string _jumpStr = "jump";

    string _rotationParamStr = "Rotation";
    string _speedParamStr = "Speed";

    string _horizontal = "Horizontal";
    string _vertical = "Vertical";

    [SerializeField] bool _tree = false;
    [SerializeField] float _moveSpeed = 1.7f;
    [SerializeField] float _runSpeed = 3.5f;
    bool _run = false;
    Quaternion _rotation = Quaternion.identity;
    Rigidbody _rigidbody;
    Vector3 _movement; 

    AudioSource _audioSource;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (_tree) TreeUpdate();
        else StateUpdate(); 
    }
    private void StateUpdate()
    {
        _movement.Set(Input.GetAxis(_horizontal), 0f, Input.GetAxis(_vertical));
        _movement.Normalize();
        if (_movement.magnitude > 0) _animator.Play(_walkStr);
        else _animator.Play(_idleStr);

        _rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _movement, 20f * Time.deltaTime, 0f));

        _rigidbody.MovePosition(_rigidbody.position + _movement * Time.deltaTime * _moveSpeed);
        _rigidbody.MoveRotation(_rotation);
    }
    private void TreeUpdate()
    {
        _movement.Set(Input.GetAxis(_horizontal), 0f, Input.GetAxis(_vertical));
        if (_movement.magnitude > _movement.normalized.magnitude) _movement.Normalize();
        _run = Input.GetKey(KeyCode.LeftShift);
        _animator.SetFloat(_speedParamStr, _run ? _movement.magnitude * 2 : _movement.magnitude);
        _animator.SetFloat(_rotationParamStr, Vector3.SignedAngle(transform.forward, _movement, transform.up));

        _rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _movement, 20f * Time.deltaTime, 0f));

        _rigidbody.MovePosition(_rigidbody.position + _movement * Time.deltaTime * (_run ? _runSpeed : _moveSpeed));
        _rigidbody.MoveRotation(_rotation);
    }
    public void StepEvent()
    {
        _audioSource.Play();
    }
}
