using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlashBehaviour : ProjectileAbilityBehaviour
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * abilityData.Speed * Time.deltaTime;
    }
}
