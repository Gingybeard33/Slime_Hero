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
    Animator faceAnimator;
    Animator bodyAnimator;

    Vector2 movementInput;
    Vector2 lastMovementInput;
    Vector2 mousePos;
    Rigidbody2D rb;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        faceAnimator = GetComponent<Animator>();
        bodyAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Update() {
        Vector2 temp = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 targetMouse = new Vector3(temp.x, temp.y);
        Vector3 direction = targetMouse - transform.position;
        direction.Normalize();
        mousePos = direction;

        Animate();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(movementInput != Vector2.zero) {
            // bool success = TryMoveNotPhysics(movementInput);

            // if(!success) {
            //     if(movementInput.x != 0) {
            //         success = TryMoveNotPhysics(new Vector2(movementInput.x, 0));
            //     }

            //     if(!success && movementInput.y != 0){
            //         success = TryMoveNotPhysics(new Vector2(0, movementInput.y));
            //     }
            // }

            // if(success) {
            //     bodyAnimator.SetBool("isMoving", true);
            //     faceAnimator.SetBool("isMoving", true);
            // }
            // else {
            //     bodyAnimator.SetBool("isMoving", false);
            //     faceAnimator.SetBool("isMoving", false);
            // }

            // PHYSICS

            rb.velocity = movementInput * moveSpeed;
            bodyAnimator.SetBool("isMoving", true);
            faceAnimator.SetBool("isMoving", true);    
        }
        else {
            rb.velocity = Vector2.zero;
            bodyAnimator.SetBool("isMoving", false);
            faceAnimator.SetBool("isMoving", false);
        }
    }

    private bool TryMoveNotPhysics(Vector2 direction) {
        if(direction != Vector2.zero) {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );


            lastMovementInput = direction;
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
            return true;
        }
        else {
            return false;
        }
    }

    void Animate() {
        faceAnimator.SetFloat("MouseX", mousePos.x);
        faceAnimator.SetFloat("MouseY", mousePos.y);
        bodyAnimator.SetFloat("MoveX", lastMovementInput.x);
    }

    void OnMove(InputValue movement){
        movementInput = movement.Get<Vector2>();
    }

}
