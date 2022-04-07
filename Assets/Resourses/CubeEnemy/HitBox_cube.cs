using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_cube : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            Destroy(other.gameObject);
    }
}
