using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all melee ability behaviors [To be placed on a prefab of an ability that is a melee type]
/// </summary>
public class MeleeAbilityBehaviour : MonoBehaviour
{
    public AbilityScriptableObject abilityData;
    protected PlayerController pc;


    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;

    void Awake()
    {
        currentDamage = abilityData.Damage;
        currentSpeed = abilityData.Speed;
        currentCooldownDuration = abilityData.CooldownDuration;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
    }

    public virtual void AttackFinished(){
        Destroy(gameObject);
        pc.UnlockMovement();
        print("Attack is Finished");
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}
