using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkullEnemy : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    [SerializeField]  private GameObject player_bullet_right;
    [SerializeField] private GameObject player_bullet_left;
    [SerializeField]   private Transform attack_Point;

    public float attack_Timer = 0f;
    private Rigidbody2D m_body2d;
    private Animator m_animator;
    private AI_sensor m_aiSensor;
    private Sensor_Cube m_groundSensor;
    private bool m_grounded = false;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
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
        if (inputX != 0)
            if (inputX > transform.position.x && inputX - transform.position.x < 2.5f)
                inputX = -1.0f;
            else if (inputX < transform.position.x && transform.position.x - inputX < 2.5f)
                inputX = 1.0f;
            else
                inputX = 0f;
            

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // attack
        if (attack_Timer <= 0 && m_aiSensor.State() != 0f)
        {
            if (m_aiSensor.State() > transform.position.x && inputX == 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                attack_Timer = 5f;
                Attack();
            } else if (inputX == 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                attack_Timer = 5f;
                Attack();
            }
        }

        // Move
        if (!(attack_Timer > 4.0f))
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        if (attack_Timer > 0)
            attack_Timer -= Time.deltaTime;
    }

    void Attack()
    {
        m_animator.SetTrigger("IsAttacking");
        if (transform.localScale.x == 1.0f)
            Instantiate(player_bullet_right, attack_Point.position, Quaternion.identity);
        else
            Instantiate(player_bullet_left, attack_Point.position, Quaternion.identity);
    }
}
