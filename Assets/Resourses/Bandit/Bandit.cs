using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private GameObject m_cube;
    public int m_health;
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private AttackBox_Bandit m_attackBox;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;

    // Use this for initialization
    void Start()
    {
        m_health = 3;
        m_cube = GameObject.Find("HealthCube_Bandit");
        m_attackBox = transform.Find("AttackBox").GetComponent<AttackBox_Bandit>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && !m_grounded)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_cube.GetComponent<SpriteRenderer>().color = new Color((m_health == 2 || m_health == 1) ? 1.0f : 0f, (m_health == 3 || m_health == 2 ? 1.0f : 0f), 0f);

        if (m_isDead)
            return;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character fell off the map
        if (transform.position.y < -6.0f)
            transform.position = new Vector3(0.0f, 1.0f, 0.0f);

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", false);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        if (m_attackBox.timer() < 1.1f)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        if (Input.GetMouseButtonDown(0) && m_attackBox.timer() == 0f)
        {
            m_animator.SetTrigger("Attack");
            m_attackBox.changeState(true);
            m_attackBox.Disable(2f);
        }
        else
        {
            m_attackBox.changeState(false);
        }

        //Change between idle and combat idle
        if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && m_attackBox.timer() < 1.1f)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_isDead)
            return;
        if (other.gameObject.tag == "SuicideBox" || other.gameObject.tag == "Bullet" || other.gameObject.tag == "HitBox")
        {
            m_health -= other.gameObject.GetComponent<DamageScript>().AttackDamage;

            if (other.gameObject.tag == "SuicideBox")
                Destroy(other.transform.parent.gameObject);
            else
                Destroy(other.gameObject);
        }


        if (m_health <= 0)
        {
            m_animator.SetTrigger("Death");
            m_isDead = true;
        }
    }
}
