using UnityEngine;
using System.Collections;


public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField]
    Transform PlayerCenter;//밀기위해....존재
    [SerializeField]
    ClockMove m_clock;

    public GameObject mainCam;


    [SerializeField]
    float m_Speed;

    [SerializeField]
    float m_PlayerPushSpeed;
    private Rigidbody myRigid;
    [SerializeField]
    float m_JumpPower;
    [SerializeField]
    bool m_IsJumping = false;
    [SerializeField]
    bool m_grounded = true;


    private Vector3 fowardVec;
    private Vector3 rightVec;

    public Animator anim;

    public bool ClockWise;
    

// Use this for initialization
void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //좌우 확인
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0 || v != 0)
        {
            anim.SetBool("b_Move", true);
        }
        else
        {
            anim.SetBool("b_Move", false);
        }

        ////이동
        //Move(h, v);      
        ////회전
        //Rotation(h, v);

        //점프
        if (Input.GetButtonDown("Jump") && m_grounded == true)
        {
            anim.SetTrigger("t_Jump");
            anim.SetBool("b_Ground", false);
            m_grounded = false;
            Debug.Log("Jump");
            m_IsJumping = true;
        }
        
        //밀때 속도
        //if(anim.GetBool("b_Push") == true)
        //{
        //    m_PlayerCenter.Rotate(new Vector3(0, 1f, 0) * (m_PlayerPushSpeed+ clock.m_initialminhandSpeed) * h * Time.deltaTime);
        //    clock.m_minhandSpeed = (clock.m_initialminhandSpeed + m_PlayerPushSpeed)*h;
        //}
        //else
        //{
        //    m_PlayerCenter.Rotate(new Vector3(0, 1f, 0) * m_playerHorizontalSpeed * h * Time.deltaTime);
        //}


    }

    void FixedUpdate()
    {
        //좌우 확인
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        Rotation(h, v);
        Jump();
    }

    void Jump()
    {
        if (m_IsJumping)
        {
            myRigid.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
            m_IsJumping = false;
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            anim.SetBool("b_Ground", true);
            m_grounded = true;
        }
    }


    void OnCollisionStay(Collision col)
    {
    }

    void OnCollisionExit(Collision col)
    {
    }

    void Move(float h, float v)
    {
        if (anim.GetBool("b_Push") == true) //무엇을 밀고 있을때.
        {
            if (Input.GetButton("Interactive"))
            {
                if(!ClockWise)
                {
                    //시계방향으로 밀때
                    PlayerCenter.Rotate(new Vector3(0, 1, 0) * (m_PlayerPushSpeed + m_clock.m_initialminhandSpeed) * Time.deltaTime);
                    m_clock.m_minhandSpeed = (m_clock.m_initialminhandSpeed + m_PlayerPushSpeed);


                    //anim.SetBool("b_Push", false);
                    mainCam.GetComponent<CameraMove>().IsDramatic = false;
                    //m_clock.ReSetMoveSpeed();
                }
                else
                {
                    //반시계방향으로 밀때
                    PlayerCenter.Rotate(new Vector3(0, 1, 0) * -(m_PlayerPushSpeed + m_clock.m_initialminhandSpeed) * Time.deltaTime);
                    m_clock.m_minhandSpeed = -(m_clock.m_initialminhandSpeed + m_PlayerPushSpeed);


                    //anim.SetBool("b_Push", false);
                    mainCam.GetComponent<CameraMove>().IsDramatic = false;
                    //m_clock.ReSetMoveSpeed();
                }
                
                return;
            }
            else
            {
                m_clock.ReSetMoveSpeed();
            }
        }

        //foward
        fowardVec = this.transform.position - Camera.main.transform.position;
        fowardVec.y = 0;
        fowardVec.Normalize();

        //right
        rightVec = new Vector3();
        float angle = 90 * Mathf.Deg2Rad;
        rightVec.x = fowardVec.x * Mathf.Cos(angle) + fowardVec.z * Mathf.Sin(angle);
        rightVec.z = fowardVec.z * Mathf.Cos(angle) - fowardVec.x * Mathf.Sin(angle);

        //상 하 이동
        if (v>0)
        {
            this.transform.position += fowardVec * m_Speed * Time.deltaTime;
        }
        else if (v < 0)
        {
            this.transform.position += -fowardVec * m_Speed * Time.deltaTime;
        }

        //좌 우 이동
        if (h > 0)
        {
            this.transform.position += rightVec * m_Speed * Time.deltaTime;
        }
        else if (h < 0)
        {
            this.transform.position += -rightVec * m_Speed * Time.deltaTime;
        }
    }

    void Rotation(float h, float v)
    {
        if (anim.GetBool("b_Push") == true) //무엇을 밀고 있을때.
        {
            return;
        }
        Vector3 nowPos = this.transform.position;
        Vector3 lookVec = new Vector3();
        lookVec += nowPos;

        if (v > 0)
        {
            lookVec += fowardVec;
        }
        else if (v < 0)
        {
            lookVec -= fowardVec;
        }

        //좌 우 이동
        if (h > 0)
        {
            lookVec += rightVec;
        }
        else if (h < 0)
        {
            lookVec -= rightVec;
        }

        this.transform.LookAt(lookVec);
    }

}
