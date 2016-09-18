using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
    //카메라 연출체크
    public bool IsDramatic = false;
    //카메라 바라보기,pos
    public GameObject player;
    private Vector3 offset;
    //카메라 회전
    public Vector2 rotationRange = new Vector3(70, 70);
    public float rotationSpeed = 10;
    public float dampingTime = 0.2f;

    private Vector3 m_TargetAngles;
    private Vector3 m_FollowAngles;
    private Vector3 m_FollowVelocity;
    private Quaternion m_OriginalRotation;
    //마우스 휠
    [SerializeField]
    float zoomMax;
    [SerializeField]
    float zoomMin;
    [SerializeField]
    float wheelSpeed = 3f;

    void Start()
    {
        offset = transform.position - player.transform.position;
        m_OriginalRotation = transform.localRotation;
    }

    void Update()
    {
        transform.localRotation = m_OriginalRotation;

        float inputH;
        float inputV;

        if(!IsDramatic)
        {
            //마우스 좌우 이동
            inputH = Input.GetAxis("Mouse X");
            inputV = Input.GetAxis("Mouse Y");

            m_TargetAngles.y += inputH * rotationSpeed;
            m_TargetAngles.x += inputV * rotationSpeed;

            //m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
            m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);

            m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, dampingTime);

            // update the actual gameobject's rotation
            transform.localRotation = m_OriginalRotation * Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);

            //마우스 휠
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            Camera.main.fieldOfView -= wheel * wheelSpeed;

            if (Camera.main.fieldOfView > zoomMax)
            {
                Camera.main.fieldOfView = zoomMax;
            }
            if (Camera.main.fieldOfView < zoomMin)
            {
                Camera.main.fieldOfView = zoomMin;
            }

        }



    }

    void LateUpdate()
    {
        
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
    }
}


