using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);

                if(!success) {
                    if(movementInput.x != 0) {
                        success = TryMove(new Vector2(movementInput.x, 0));
                    }

                    if(!success && movementInput.y != 0){
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
        }   
    }

     private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if(count == 0) {
                rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    void OnMove(InputValue movement){
        movementInput = movement.Get<Vector2>();
    }
}
