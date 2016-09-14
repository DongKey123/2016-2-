using UnityEngine;
using System.Collections;

public class ClockMove : MonoBehaviour {

    [SerializeField]
    Transform m_hourhand;
    [SerializeField]
    Transform m_minhand;
    
    public float m_minhandSpeed;
    public float m_initialminhandSpeed;

	// Use this for initialization
	void Start () {
        m_initialminhandSpeed = m_minhandSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        m_minhand.Rotate(new Vector3(0, 1, 0) * m_minhandSpeed * Time.deltaTime);
        m_hourhand.Rotate(new Vector3(0, 0, 1) * m_minhandSpeed * Time.deltaTime / 12);
    }

    public void ReSetMoveSpeed()
    {
        m_minhandSpeed = m_initialminhandSpeed;
    }
}
