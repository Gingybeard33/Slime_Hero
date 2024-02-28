using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBehavior : MeleeWeaponBehavior
{
    PlayerController pc;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pc = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void AttackFinished(){
        Destroy(gameObject);
        pc.UnlockMovement();
    }
}
