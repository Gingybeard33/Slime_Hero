using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Script for all ability controllers
/// </summary>
public class AbilityController : MonoBehaviour
{
    [Header("Ability Stats")]
    public AbilityScriptableObject abilityData;
    float currentCooldown;

    protected PlayerController pc;

    protected bool canAttack;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        currentCooldown = abilityData.CooldownDuration;
        canAttack = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(currentCooldown > 0f){
            currentCooldown -= Time.deltaTime;
        }
        else{
            canAttack = true;
        }
    }

    public virtual void Attack(){
            currentCooldown = abilityData.CooldownDuration;
            canAttack = false;
    }
}
