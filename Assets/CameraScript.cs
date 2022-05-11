using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(player.transform.position.y - transform.position.y) > 5f)
            transform.position = new Vector3(player.transform.position.x, transform.position.y + Math.Sign(player.transform.position.y - transform.position.y) * 5f, -5f);
        else
            if (player.GetComponent<Bandit>().m_grounded)
                if (Math.Abs(player.transform.position.y - transform.position.y) > 0.001f)
                    transform.position = new Vector3(player.transform.position.x, transform.position.y + Math.Sign(player.transform.position.y - transform.position.y) * 0.01f, -5f);
                else
                    transform.position = new Vector3(player.transform.position.x, transform.position.y, -5f);
            else
                transform.position = new Vector3(player.transform.position.x, transform.position.y, -5f);

        if (transform.position.y < 2.7f)
            transform.position = new Vector3(transform.position.x, 2.7f, -5f);
    }
}
