using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public EnemyScriptableObject enemyData;

    //Current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;
    float currentBodyDamage;


    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        currentBodyDamage = enemyData.BodyDamage;
    }

    public void TakeDamage(float dmg){
        if(currentHealth - dmg < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= dmg;
        }

        if(currentHealth <= 0) 
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
