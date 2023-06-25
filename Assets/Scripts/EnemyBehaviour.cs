using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(NavMeshAgent))]
public abstract class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy States")]
    public bool idleMode, attackMode, chaaseMode, damagedMode;

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
        damagedMode = false;

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (damagedMode) agent.enabled = false;
        else agent.enabled = true;

        if (!playerInRadius)
        {
            playerInRadius = Physics.CheckSphere(transform.position, idleRadarRadius, whatIsPlayer);
            IdleModeBehav();
            return;
        }
        else
        {
            ChaaseModeBehav();
        }
    }

    public void IdleModeBehav()
    {
        tmp = 0;
        if (agent.enabled)
        {
            agent.isStopped = true;
        }
    }

    public void ChaaseModeBehav()
    {
        //Debug.Log("Start chaase player");
        if (!audioSource.isPlaying && tmp == 0)
        {
            audioSource.PlayOneShot(chaseModeActivatedClip);
            tmp++;
        }

        if (!damagedMode)
        {
            agent.isStopped = false;
            if (Player != null && agent.enabled)
            {
                agent.SetDestination(Player.transform.position);
            }
            playerInRadius = Physics.CheckSphere(transform.position, chaaseRadarRadius, whatIsPlayer);
        }
       
    }

    public void AttackModeBehav()
    {

    }

    public void TakeDamage(float damage, float impactForce, RaycastHit hit)
    {
        //damagedMode = true;
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        //if (damagedMode)
        Debug.Log("Before Courotine");
        if(!damagedMode) StartCoroutine(DamagedMode(impactForce, hit));


    }

    IEnumerator DamagedMode(float impactForce, RaycastHit hit)
    {
        damagedMode = true;
        rb.AddForce(-hit.normal * impactForce);

        yield return new WaitForSeconds(1f);
        Debug.Log("After 1s in courotine");

        damagedMode = false;
        

    }

    void Die()
    {
        Destroy(gameObject);
    }


}
