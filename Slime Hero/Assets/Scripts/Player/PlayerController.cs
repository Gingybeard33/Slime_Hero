using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    public ContactFilter2D movementFilter;

    [HideInInspector]
    public Vector2 movementInput;
    [HideInInspector]
    public Vector2 lastMovementInput;
    [HideInInspector]
    public Vector2 mousePos;

    Rigidbody2D rb;
    Animator faceAnimator;
    Animator bodyAnimator;
    SpriteRenderer spriteRenderer;

    
    bool canMove;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        faceAnimator = GetComponent<Animator>();
        bodyAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        canMove = true;
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
        Move();
    }

    void Move(){
        if(movementInput != Vector2.zero && canMove) {
            rb.velocity = movementInput * playerData.MoveSpeed;
            lastMovementInput = movementInput;
            bodyAnimator.SetBool("isMoving", true);
            faceAnimator.SetBool("isMoving", true);    
        }
        else {
            rb.velocity = Vector2.zero;
            bodyAnimator.SetBool("isMoving", false);
            faceAnimator.SetBool("isMoving", false);
        }
    }

    void Animate() {
        faceAnimator.SetFloat("MouseX", mousePos.x);
        faceAnimator.SetFloat("MouseY", mousePos.y);

        bodyAnimator.SetFloat("MoveX", lastMovementInput.x);
        bodyAnimator.SetFloat("MouseX", mousePos.x);
        bodyAnimator.SetFloat("MouseY", mousePos.y);
    }

    void OnMove(InputValue movement){
        movementInput = movement.Get<Vector2>();
    }

    void OnEat() {
        print("EaTING");
    }

    void OnFire(){
        AbilityController ability = FindObjectOfType<AbilityController>();
        ability.Attack();
        print("firing");
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

}
