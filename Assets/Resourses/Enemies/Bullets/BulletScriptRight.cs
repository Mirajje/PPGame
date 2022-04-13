using UnityEngine;
using System;

public class BulletScriptRight : MonoBehaviour
{

    [SerializeField] public float speed = 5f;
    public float disable_Timer = 3f;
    public bool right;

    // Start is called before the first frame update
    void Start()
    {
        /*if (gameObject.parent.transform.scale.x == 1.0f)
            right = true;
        else
            right = false;*/
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
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
