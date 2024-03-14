using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlashController : AbilityController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        if(canAttack)
        {
            base.Attack();
            GameObject spawnedPowerSlash = Instantiate(abilityData.Prefab);
            spawnedPowerSlash.transform.position = transform.position;
            spawnedPowerSlash.GetComponent<PowerSlashBehaviour>().DirectionChecker(new Vector3(pc.mousePos.x,pc.mousePos.y));
        }
    }
}
