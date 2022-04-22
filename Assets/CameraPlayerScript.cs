using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -5f);
    }
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);  
    }
}
