using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EnemyAbstr : MonoBehaviour
{
    //Common Fields
    [Header("Fields common for every enemy")]
    public float maxHealth;
    public float currentHealth;
    public float movementSpeed;
    public float dealtDamage;

    //Enemy Animation
    [Header("Required animations for enemy")]
    public Animation idleAnim;
    public Animation movementAnim;
    public Animation attackAnim;
    public Animation deathAnimExplosion, deathAnimIgnition; //etc.

    [Header("Required sounds for enemy")]
    public AudioSource idleSound;
    public AudioSource attackSound;
    public AudioSource deathSound;

    // I assume that every enemy use Rigidbody for movement
    protected Rigidbody rb;

    // Initialization
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }


    protected virtual void Update()
    {
        Movement(movementSpeed);
    }

    protected virtual void Movement(float speed)
    {

        //move

    }

    protected virtual void Attack(float dealtDamage)
    {
        //attack!
    }

    public virtual void TakeDamage(float damage)
    {
        //take damage
    }

    protected virtual void Death(DeathTypes deathType)
    {
        //If currentHealth <= 0
        //switch (deathType)
        //{
        //    case DeathTypes.Explosion:
        //        DeathFromExplosion();
        //        break;
        //    case DeathTypes.OtherType:
        //        DeathFromOtherType();
        //        break;
        //}
    }

    protected virtual void DeathFromExplosion(Animation deathAnimation)
    {
        //BUM!
        //Delete(gameObject);
    }

    protected virtual void DeathFromOtherType(Animation deathAnimation)
    {
        //Die
        //Delete(gameObject);
    }

}
