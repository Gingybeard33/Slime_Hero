using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName ="Scriptable Objects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
   // Base Stats for Enemies
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value;}

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value;}
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value;}
    [SerializeField]
    float attackSpeed;
    public float AttackSpeed { get => attackSpeed; private set => attackSpeed = value;}
    [SerializeField]
    public float collisionOffset = .02f;
    public float CollisionOffset { get => collisionOffset; private set => collisionOffset = value;}
    [SerializeField]
    public float bodyDamage;
    public float BodyDamage { get => bodyDamage; private set => bodyDamage = value;}
   
}
