using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName ="Scriptable Objects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value; }

    [SerializeField]
    Sprite icon;

    public Sprite Icon { get => icon; private set => icon = value; }
}
