using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_cube : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
            Destroy(other.gameObject);
    }
}
