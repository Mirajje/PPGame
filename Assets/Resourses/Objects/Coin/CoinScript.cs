using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Bandit player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Bandit>().coinAmount += 1;
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Bandit>().coinAmount += 1;
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
