using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform target;
    public float speedX = 1.0f;
    public float speedY = 1.0f;
    public float Distance = 5.0f;
    private Transform mCamera;

    [SerializeField]
    private bool CanRotate = true;

    [SerializeField]
    private float rotateMax = 90f;
    [SerializeField]
    private float rotateMin = -10f;
    [SerializeField]
    private float distanceMax = 10.0f;
    [SerializeField]
    private float distanceMin = 2.0f;


    private float x = 0.0f;   //representing mouseX axis
    private float y = 0.0f;   //representing mouseY axis

    float RotationLerp;
    // Use this for initialization
    void Awake()
    {
        mCamera = transform;
        Vector3 angles = mCamera.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    public void SetTarget(Transform a_target)
    {
        target = a_target;
        ResetCamera();
        InitCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ControlCamera();
    }

    public void ControlCamera()
    {
        if (target)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");


            if (CanRotate) x += mouseX * speedX * 5.0f / Distance * (distanceMax / distanceMin);
            if (CanRotate) y -= mouseY * speedY;

            y = ClampAngle(y, rotateMin, rotateMax);

            //scroll to zoom
            //Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * 20.0f, distanceMin, distanceMax);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -Distance);

            mCamera.rotation = rotation /** Quaternion.Euler(0, RotationLerp, 0)*/;
            mCamera.position = target.position + rotation * negDistance + new Vector3 (0,4,0);
        }
    }

    void InitCamera()
    {
        x += Input.GetAxis("Mouse X") * speedX * Distance;
        y -= Input.GetAxis("Mouse Y") * speedY;

        y = ClampAngle(y, rotateMin, rotateMax);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        //scroll to zoom
        //Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -Distance);
        Vector3 position = target.position + rotation * negDistance;

        mCamera.rotation = rotation;
        mCamera.position = position;
        //mCamera.position = Vector3.Lerp(mCamera.position, position, Time.fixedDeltaTime * 5.0f);
    }

    void ResetCamera()
    {
        x = target.eulerAngles.y;
        y = 30;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        else if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);

    }
}
