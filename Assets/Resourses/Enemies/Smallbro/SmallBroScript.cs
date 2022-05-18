using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class SmallBroScript : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Rigidbody2D m_body2d;
    private AI_sensor m_aiSensor;
    private Sensor_Cube m_groundSensor;
    private bool m_grounded = false;
    private float dir = 0f;
    private float timer = 0f;

    // Use this for initialization
    void Start()
    {
        m_aiSensor = transform.Find("AISensor").GetComponent<AI_sensor>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Cube>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
            m_grounded = true;

        //Check if character fell off the map
        if (transform.position.y < -6.0f)
            transform.position = new Vector3(0.0f, 1.0f, 0.0f);

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
            m_grounded = false;

        // -- Handle input and movement --
        float inputX = m_aiSensor.State();
        if (Math.Abs(inputX - transform.position.x) < 0.05f)
            inputX = 0;
        if (inputX != 0)
            if (inputX > transform.position.x)
                inputX = 1.0f;
            else if (inputX < transform.position.x)
                inputX = -1.0f;

        if (m_aiSensor.State() == 0)
            if (timer > 0f)
            {
                m_body2d.velocity = new Vector2(dir * m_speed / 2, m_body2d.velocity.y);
                timer -= Time.deltaTime;
            }
            else
            {
                dir = UnityEngine.Random.Range(-1.0f, 1.0f);

                if (dir > 0)
                    dir = 1.0f;
                else if (dir < 0)
                    dir = -1.0f;
                else
                    dir = 0;

                if (dir > 0)
                    transform.localScale = new Vector3(3f, 3f, 3f);
                else if (dir < 0)
                    transform.localScale = new Vector3(-3.0f, 3.0f, 3.0f);

                timer = 2f;
            }

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(3f, 3f, 1f);
        else if (inputX < 0)
            transform.localScale = new Vector3(-3.0f, 3.0f, 1.0f);

        // Move
        if (m_aiSensor.State() != 0)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
    }
}
