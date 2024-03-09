using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public EnemyScriptableObject enemyData;
    protected HeroController hc;

    //Current stats
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentDamage;
    [HideInInspector] public float currentBodyDamage;
    [HideInInspector] public float currentAttackSpeed;

    public float despawnDistance = 20f;
    Transform player;


    float attackCooldown;
    Animator animator;


    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        currentBodyDamage = enemyData.BodyDamage;
        currentAttackSpeed = enemyData.AttackSpeed;
        attackCooldown = currentAttackSpeed;
        animator = GetComponent<Animator>();
        hc = GetComponent<HeroController>();
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            MoveEnemy();
        }
    }

    // For ATTACK SPEED BASED ATTACKING

    // void Update()
    // {
    //     if(attackCooldown > 0)
    //     {
    //         attackCooldown -= Time.deltaTime;
    //     }
    // }

    public void TakeDamage(float dmg){
        if(currentHealth - dmg < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= dmg;
        }

        if(currentHealth == 0) 
        {
            hc.LockMovement();
            animator.SetBool("isDefeated", true);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") {
            // ATTACK SPEED TYPE OF DAMAGING

            // if(attackCooldown < 0) {
            //     PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            //     player.TakeDamage(currentBodyDamage);
            //     attackCooldown = currentAttackSpeed;
            // }
            // else {
            //     attackCooldown -= Time.deltaTime;
            // }

            // I-FRAMES TYPE OF DAMAGING

            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentBodyDamage);
        }
    }


    // void OnCollisionEnter2D(Collision2D other){
    //     if(other.gameObject.CompareTag("Player"))
    //     {
    //         PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
    //         player.TakeDamage(currentBodyDamage);
    //     }
    // }  


    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    // void OnCollisionExit2D(Collision2D other)
    // {
    //     attackCooldown = 0f;
    // }


    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    void MoveEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
