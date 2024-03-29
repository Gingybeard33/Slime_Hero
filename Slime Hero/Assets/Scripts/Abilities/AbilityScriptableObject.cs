using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Ability", menuName ="Scriptable Objects/Ability")]
public class AbilityScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value;}
    
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value;}

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value;}

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value;}

    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value;}

    [SerializeField]
    float abilityDuration;
    public float AbilityDuration { get => abilityDuration; private set => abilityDuration = value;}

    [SerializeField]
    Sprite icon;

    public Sprite Icon { get => icon; private set => icon = value; }
}
