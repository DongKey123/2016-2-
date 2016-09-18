using UnityEngine;
using System.Collections;

public class PushObject : MonoBehaviour {

    [SerializeField]
    PlayerMoveMent Player;

    [SerializeField]
    ClockMove m_clock;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "minHand")
        {

            if (Input.GetButton("Interactive"))
            {
                Player.anim.SetBool("b_Push", true);
                Player.mainCam.GetComponent<CameraMove>().IsDramatic = true;
            }
            else
            {
                Player.anim.SetBool("b_Push", false);
                Player.mainCam.GetComponent<CameraMove>().IsDramatic = false;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "clockWise")
        {
            Player.ClockWise = true;
            Debug.Log("clock");
        }
        else if (col.gameObject.tag == "nonclockWise")
        {
            Player.ClockWise = false;
            Debug.Log("nonclock");
        }
        if (col.gameObject.tag == "minHand")
        {

            if (Input.GetButton("Interactive"))
            {
                
                Player.anim.SetBool("b_Push", true);
                Player.mainCam.GetComponent<CameraMove>().IsDramatic = true;
            }
            else
            {
                Player.anim.SetBool("b_Push", false);
                Player.mainCam.GetComponent<CameraMove>().IsDramatic = false;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "minHand")
        {
            Player.anim.SetBool("b_Push", false);
            Debug.Log("Out");
            Player.mainCam.GetComponent<CameraMove>().IsDramatic = false;
            m_clock.ReSetMoveSpeed();
        }
    }
}
