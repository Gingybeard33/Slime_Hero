using System.Collections;
using System.Collections.Generic;
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

        if(MouseX >= -0.5 * MouseY && MouseX < 0.5 * MouseY)
        {
            // Up
            retVal = new Vector2(0, -.07f);
        }
        else if(MouseX >= 0.5 * MouseY && MouseX < 2 * MouseY)
        {
            // Up Right
            retVal = new Vector2(0.12f, -0.1f);

        }
        else if(MouseX >= 2 * MouseY && MouseX > -2 * MouseY)
        {
            // Right
            retVal = new Vector2(0.09f, -0.09f);
        }
        else if(MouseX <= -2 * MouseY && MouseX > -0.5 * MouseY)
        {
            // Down Right
            retVal = new Vector2(0.08f, -0.15f);
        }
        else if(MouseX <= -0.5 * MouseY && MouseX > 0.5 * MouseY)
        {
            // Down
            retVal = new Vector2(0, -0.15f);
        }
        else if(MouseX <= 0.5 * MouseY && MouseX > 2 * MouseY)
        {
            // Down Left
            retVal = new Vector2(-0.08f, -0.15f);
        }
        else if(MouseX <= 2 * MouseY && MouseX < -2 * MouseY)
        {
            // Left
            retVal = new Vector2(-0.09f, -0.09f);
        }
        else if(MouseX >= -2 * MouseY && MouseX < -0.5 * MouseY)
        {
            // Up Left
            retVal = new Vector2(-0.07f, -0.06f);

        }

        return retVal;
    }
}
