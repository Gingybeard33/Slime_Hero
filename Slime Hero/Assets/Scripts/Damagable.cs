using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    public float maxHealth = 10;
    public float health = 10;

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
            if(health <= 0) {
                health = 0;

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
        Health -= damage;
    }

    public void Defeated() {
    }

}