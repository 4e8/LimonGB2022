using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DayCycle : MonoBehaviour
{
    Animator _animator;

    float _animatorStateTime;

    [SerializeField] Gradient _skyGradient;
    [SerializeField] Gradient _equatorGradient;
    [SerializeField] Gradient _groundGradient;
    
    [SerializeField] float _dayLenght = 20;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 1/_dayLenght;
    }
    private void Update()
    {
        _animatorStateTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime - Mathf.Round(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 0.5f);
        RenderSettings.ambientSkyColor = _skyGradient.Evaluate(_animatorStateTime);
        RenderSettings.ambientEquatorColor = _equatorGradient.Evaluate(_animatorStateTime);
        RenderSettings.ambientGroundColor = _groundGradient.Evaluate(_animatorStateTime);
    }
}
