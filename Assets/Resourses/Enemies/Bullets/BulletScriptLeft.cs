using UnityEngine;
using System;

public class BulletScriptLeft : MonoBehaviour
{

    [SerializeField] public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //Check if bullet flew off the map
        if (Math.Abs(transform.position.x) > 30f)
            Destroy(gameObject);
    }

    void Move()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
