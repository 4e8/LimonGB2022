using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

namespace Game
{
    public class Aim : MonoBehaviour
    {
        [SerializeField] Stats _myStats;
        //[SerializeField] private float _surfaceOffset = 1f;
        [SerializeField] CinemachineVirtualCamera _vcam;
        [SerializeField] Image _targetHpBar;
        Camera _camera;

        Transform _defaultTarget;
        [SerializeField] Transform _raceTarget;
        CinemachineComposer _composer;

        DepthOfField _depthOfField;
        private void Start()
        {
            _defaultTarget = _vcam.LookAt;
            _camera = Camera.main;
            _composer = _vcam.GetCinemachineComponent<CinemachineComposer>();

            PostProcessVolume postProcessVolume = _camera.GetComponent<PostProcessVolume>();
            _depthOfField = postProcessVolume.profile.GetSetting<DepthOfField>();
        }
        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            _depthOfField.focusDistance.value = (transform.position - _camera.transform.position).magnitude;


            if (!Physics.Raycast(ray, out _hit))
                return;

            transform.position = _hit.point; //- (hit.normal * _surfaceOffset);

            if (Input.GetKey(KeyCode.Mouse1)) 
                LockTarget(_hit);
            
            if (Input.GetKey(KeyCode.R)) 
                RaceCamMode();
            
            if (Input.GetKey(KeyCode.Mouse2) || !_vcam.LookAt.gameObject.active || _vcam.LookAt == null) 
                ResetTarget();
        }

        private void LockTarget(RaycastHit hit)
        {
            _vcam.LookAt = hit.transform;
            if (hit.collider.gameObject.TryGetComponent<CollisionChild>(out CollisionChild target) && target.Parent != _myStats)
            {
                target.SetTargetHpBar(_targetHpBar);
            }
            _composer.m_DeadZoneWidth = 0.1f;
            _composer.m_DeadZoneHeight = 0.1f;
        }
        private void ResetTarget()
        {
            _vcam.LookAt = _defaultTarget;
            _composer.m_DeadZoneWidth = 0.5f;
            _composer.m_DeadZoneHeight = 0.5f;
        }
        private void RaceCamMode()
        {
            _vcam.LookAt = _raceTarget;
            _composer.m_DeadZoneWidth = 0.1f;
            _composer.m_DeadZoneHeight = 0.1f;
        }
    }
}