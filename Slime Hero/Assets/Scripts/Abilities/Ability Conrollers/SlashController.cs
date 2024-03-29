using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlashController : AbilityController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        if(canAttack){
            base.Attack();
            pc.LockMovement();
            GameObject spawnedSlash = Instantiate(abilityData.Prefab);
            spawnedSlash.transform.position = transform.position;
            spawnedSlash.GetComponent<Animator>().SetFloat("MouseX", pc.mousePos.x);
            spawnedSlash.GetComponent<Animator>().SetFloat("MouseY", pc.mousePos.y);
            CapsuleCollider2D collider = spawnedSlash.GetComponent<CapsuleCollider2D>();
            collider.offset = GetColliderPosition(pc.mousePos.x, pc.mousePos.y);
        }
        
    }

    Vector2 GetColliderPosition(float MouseX, float MouseY)
    {
        Vector2 retVal = Vector2.zero;

        switch(HelperFunctions.GetLocationOctant(MouseX, MouseY))
        {
            case "UP":
                retVal = new Vector2(0, -.07f);
                break;
            case "UP RIGHT":
                retVal = new Vector2(0.12f, -0.1f);
                break;
            case "RIGHT":
                retVal = new Vector2(0.09f, -0.09f);
                break;
            case "DOWN RIGHT":
                retVal = new Vector2(0.08f, -0.15f);
                break;
            case "DOWN":
                retVal = new Vector2(0, -0.15f);
                break;
            case "DOWN LEFT":
                retVal = new Vector2(-0.08f, -0.15f);
                break;
            case "LEFT":
                retVal = new Vector2(-0.09f, -0.09f);
                break;
            case "UP LEFT":
                retVal = new Vector2(-0.07f, -0.06f);
                break;
            
        }

        return retVal;
    }
}
