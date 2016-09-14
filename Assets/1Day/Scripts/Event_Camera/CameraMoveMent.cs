using UnityEngine;
using System.Collections;

public class CameraMoveMent : MonoBehaviour {

    public GameObject Camera;
    public Transform Pivot;
    public float wakeupTime;
    public float wakeupAngle;

    private Transform rePivotTR;
    private Transform reCameraTR;

    public float backwakeupTime;
    public float backwakeupAngle;

    public float ShakeHeadTime;
    public float ShakeHeadAngle;

    public Animator anim;

    // Use this for initialization
    void Start () {
        //rePivotTR = Pivot.transform;
        //reCameraTR = Camera.transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        //Pivot.eulerAngles = new Vector3(0, 0, 0);
        anim.Play("Empty");
    }

    public void CameraCircleMove(int moveNum)
    {
        Camera.transform.SetParent(Pivot);
        switch(moveNum)
        {
            case 1:
                {
                    anim.Play("Move1");
                    //StartCoroutine("MoveCamera1", wakeupTime);
                }
                break;
            case 2:
                {
                    anim.Play("Move2");
                    //StartCoroutine("MoveCamera2", backwakeupTime);
                }
                break;
            case 3:
                {
                    anim.Play("Move3");
                    //StartCoroutine("MoveCamera3", ShakeHeadTime);
                }
                break;
        }
    }

    

    IEnumerator MoveCamera1(float _time)
    {
        //일어나기!
        for (float f = _time; f >= 0; f -= Time.deltaTime)
        {
            Vector3 moveAngle = new Vector3(wakeupAngle, 0, 0) * Time.deltaTime / _time;
            Pivot.transform.Rotate(moveAngle);
            yield return null;
            //yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator MoveCamera2(float _time)
    {
        //뒤로가기
        for (float f = _time; f >= 0; f -= Time.deltaTime)
        {
            Vector3 moveAngle = new Vector3(backwakeupAngle, 0, 0) * Time.deltaTime / _time;
            Pivot.transform.Rotate(moveAngle);
            yield return null;
            //yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator MoveCamera3(float _time)
    {
        //뒤로가기
        for (float f = _time; f >= 0; f -= Time.deltaTime)
        {
            Vector3 moveAngle = new Vector3(0, ShakeHeadAngle, 0) * Time.deltaTime / _time;
            Pivot.transform.Rotate(moveAngle);
            yield return null;
            //yield return new WaitForSeconds(.1f);
        }
    }
}
