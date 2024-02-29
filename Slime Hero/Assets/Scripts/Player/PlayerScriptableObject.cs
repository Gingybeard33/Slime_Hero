using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Player", menuName ="Scriptable Objects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float collisionOffset;
    public float CollisionOffset { get => collisionOffset; private set => collisionOffset = value; }

    [SerializeField]
    float might;
    public float Might { get => might; private set => might = value; }

    [SerializeField]
    float projectileMovementSpeed;
    public float ProjectileMovementSpeed { get => projectileMovementSpeed; private set => projectileMovementSpeed = value; }

}
