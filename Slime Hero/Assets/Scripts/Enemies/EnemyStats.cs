using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public EnemyScriptableObject enemyData;
    protected HeroController hc;

    //Current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;
    float currentBodyDamage;
    float currentAttackSpeed;

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
}
