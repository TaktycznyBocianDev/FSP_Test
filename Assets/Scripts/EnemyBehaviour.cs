using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(NavMeshAgent))]
public abstract class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy States")]
    public bool idleMode, attackMode, chaaseMode;

    [Header("Common variables for all enemies")]
    public GameObject Player;
    public float health = 50f;
    public float movementSpeed = 10f;

    [Header("Common variables for all enemies")]
    public float idleRadarRadius = 10f;
    public float chaaseRadarRadius = 20f;
    public LayerMask whatIsPlayer;

    [Header("Common variables for all enemies")]
    public AudioClip chaseModeActivatedClip;

    [SerializeField] bool playerInRadius;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Collider coll;
    private AudioSource audioSource;
    private int tmp = 0;

    private void Start()
    {
        idleMode = true;
        attackMode = false;
        chaaseMode = false;

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        playerInRadius = Physics.CheckSphere(transform.position, idleRadarRadius, whatIsPlayer);
        if (!playerInRadius)
        {
            IdleModeBehav();
            return;
        }
        else
        {
            ChaaseModeBehav();
        }

        //if (agent != null)
        //{
        //    agent.speed = movementSpeed;
        //    agent.SetDestination(Player.transform.position);
        //}
    }

    public void IdleModeBehav()
    {
        tmp = 0;
        agent.isStopped = true;
    }

    public void ChaaseModeBehav()
    {
        Debug.Log("Start chaase player");
        if (!audioSource.isPlaying && tmp == 0)
        {
            audioSource.PlayOneShot(chaseModeActivatedClip);
            tmp++;
        }
        

        agent.isStopped = false;       
        if (Player != null)
        {
            agent.SetDestination(Player.transform.position);
        }
        playerInRadius = Physics.CheckSphere(transform.position, chaaseRadarRadius, whatIsPlayer);

    }

    public void AttackModeBehav()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


}
