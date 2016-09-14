using UnityEngine;
using System.Collections;


public class PlayerMoveMent : MonoBehaviour
{

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

    private Animator anim;

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

        //이동
        Move(h, v);      
        //회전
        Rotation(h, v);

        //점프
        if (Input.GetButtonDown("Jump") && m_grounded == true)
        {
            anim.SetTrigger("t_Jump");
            anim.SetBool("b_Ground", false);
            m_grounded = false;
            Debug.Log("Jump");
            m_IsJumping = true;
        }


        //if( h != 0)
        //{
        //    anim.SetBool("b_Move", true);
        //}
        //else
        //{
        //    anim.SetBool("b_Move", false);
        //}
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
        else if (col.gameObject.tag == "minHand")
        {
            if (anim.GetBool("b_Move") == true)
            {
                anim.SetBool("b_Push", true);
            }
        }
    }


    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "minHand")
        {
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "minHand")
        {
            anim.SetBool("b_Push", false);
            //clock.ReSetMoveSpeed();
        }
    }

    void Move(float h, float v)
    {
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
