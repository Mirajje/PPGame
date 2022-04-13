using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox_Bandit : MonoBehaviour
{
    private bool m_is_Attacking = false;
    private float m_disableTimer = 0f;

    void Update()
    {
        if (m_disableTimer > 0)
            m_disableTimer -= Time.deltaTime;
    }

    public void changeState(bool state)
    {
        if (!(m_disableTimer > 1f))
            if (state)
                m_is_Attacking = true;
            else
                m_is_Attacking = false;
    }

    public float timer()
    {
        if (m_is_Attacking)
            return m_disableTimer;
        else
            return 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_is_Attacking)
            if (other.gameObject.tag == "Enemy")
                Destroy(other.gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (m_is_Attacking)
            if (other.gameObject.tag == "Enemy")
                Destroy(other.gameObject);
    }


    public void Disable(float timer)
    {
        m_disableTimer = timer;
    }
}
