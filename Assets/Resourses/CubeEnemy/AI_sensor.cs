using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_sensor : MonoBehaviour
{
    private float m_coor;
    private bool m_inRange;

    void Start()
    {
        m_coor = 0f;
        m_inRange = false;
    }

    public float State()
    {
        if (m_inRange)
            return m_coor;
        else
            return 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_inRange = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            m_coor = other.gameObject.transform.position.x;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_inRange = false;
        }
    }
}
