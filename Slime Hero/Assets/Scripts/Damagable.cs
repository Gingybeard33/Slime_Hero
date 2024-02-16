using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    public float maxHealth = 10;
    public float health = 10;
    Animator animator;
    public float MaxHealth {
        set {
            maxHealth = value;
        }

        get {
            return maxHealth;
        }
    }

    public float Health {
        set { 
            print("here is value " + value);
            if(health <= 0) {
                health = 0;
            }
            else if(value > maxHealth){
                health = maxHealth;
            }
            else {
                health = value;
            }
        }

        get {
            return health;
        }
    }

    private void Start() {
        health = maxHealth;
    }

    public void Damage(float damage) {
        print("Damage " + damage);
        Health -= damage;
    }

    public void Heal(float heals) {
        Health += heals;
    }

    public void Defeated() {
    }

}