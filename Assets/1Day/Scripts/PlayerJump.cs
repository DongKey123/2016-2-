using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

    private Rigidbody myRigid;
    public float JumpPower;
    public bool IsJumping = false;
    public bool grounded= true;

	// Use this for initialization
	void Start () {
        myRigid = GetComponent<Rigidbody>();	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            grounded = false;
            Debug.Log("Jump");
            IsJumping = true;
        }
	}

    void FixedUpdate()
    {
        Jump();
    }

    void Jump()
    {
        if (IsJumping)
        {
            myRigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            grounded = false;
            IsJumping = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
