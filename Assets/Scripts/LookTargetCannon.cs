using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(FindTarget))]
    public class LookTargetCannon : MonoBehaviour
    {

        //[SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 2f;

        [SerializeField] private float angle;
        [SerializeField] private float distance;
        private FindTarget findTarget;
        private void Start()
        {
            findTarget = GetComponent<FindTarget>();
        }
        void Update()
        {
            float gravity = -9.81f;
            float velocity = 20000;


            if (!findTarget.HasTarget) return;
            var direction = Vector3.ProjectOnPlane(findTarget.Target.position - transform.position,transform.up);

            angle = (Mathf.Asin((direction.magnitude * 9.81f) / (20*20))*Mathf.Rad2Deg)/2;
            
            distance = direction.magnitude;
            var step = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.fixedDeltaTime, 0f);
            var qat = Quaternion.AngleAxis(angle, Vector3.right);
            transform.rotation = transform.rotation * qat;
        }
    }
    //public class BulletPhysics : MonoBehaviour
    //{
    //    public float gravity = -9.81f;
    //    public float velocity = 10000;
    //    public Vector3 origin;

    //    float increment = 0;

    //    private void Start()
    //    {
    //        origin = transform.position;
    //    }
    //    private void FixedUpdate()
    //    {

    //        //displacement = v0*t + 0.5at^2
    //        Debug.DrawLine(disp(increment), disp(increment + 0.02f) , Color.red, 60);
    //        RaycastHit.hit;

    //        if (Physics.Linecast(disp(increment), disp(increment + 0.02f), out hit))
    //        {
    //            Destroy(gameObject);
    //        }

    //        transform.position = disp(increment + 0.02f);
    //        increment += 0.02f; // fixUpdTime (sorta)
             
    //    }
    //    Vector3 disp (float t)
    //    {
    //        Vector3 vel = new Vector3(velocity * Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.x), velocity * Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.x), 0);
    //        Vector3 disp = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z) * new Vector3(0, vel.y * t + 0.5f * Mathf.Pow(t, 2) * gravity, vel.x * t) + origin;
    //        return disp;
    //    }
    //}
}