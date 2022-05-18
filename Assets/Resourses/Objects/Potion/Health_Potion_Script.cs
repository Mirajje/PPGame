using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Potion_Script : MonoBehaviour
{
    private Bandit player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Bandit>().m_health < 3)
            {
                other.gameObject.GetComponent<Bandit>().m_health = other.gameObject.GetComponent<Bandit>().m_health + 1;
                Destroy(transform.parent.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Bandit> ().m_health < 3)
            {
                other.gameObject.GetComponent<Bandit> ().m_health += 1;
                Destroy(transform.parent.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
