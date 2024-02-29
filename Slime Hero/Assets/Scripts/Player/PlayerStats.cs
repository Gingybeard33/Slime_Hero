using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileMovementSpeed;

    // I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        currentHealth = playerData.MaxHealth;
        currentRecovery = playerData.Recovery;
        currentMoveSpeed = playerData.MoveSpeed;
        currentMight = playerData.Might;
        currentProjectileMovementSpeed = playerData.ProjectileMovementSpeed;
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        // Invincibility timer has reached 0
        else if(isInvincible)
        {
            isInvincible = false;
        }
    }

    public void TakeDamage(float dmg)
    {
        if(!isInvincible){
            if(currentHealth - dmg < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= dmg;
            }

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if(currentHealth == 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Debug.Log("PLAYER IS DEAD");
    }

}
