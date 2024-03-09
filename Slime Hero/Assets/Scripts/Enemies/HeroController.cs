using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    enum MovementStyle 
    {
        Scan,
        Follow,
        Hide
    }

    [SerializeField] MovementStyle movementStyle;
    public ContactFilter2D movementFilter;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Transform playerTransform;
    Animator animator;
    EnemyStats enemy;
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyStats>();
        canMove = true;
    }

    void FixedUpdate() {
        UseMoveStyle();
    }

    private void UseMoveStyle() {
        bool isMoving = false;
        Vector2 direction = Vector2.zero;
        if(canMove)
        {
            switch(movementStyle) {
                case MovementStyle.Scan:
                    break;
                case MovementStyle.Follow:
                    Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y - .5f);
                    direction = target - new Vector2(transform.position.x, transform.position.y);
                    direction.Normalize();

                    float step = enemy.currentMoveSpeed * Time.fixedDeltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, target, step);
                    isMoving = true;
                    break;
                case MovementStyle.Hide:
                    break;
                
            }
        }
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        if(isMoving) {
            animator.SetBool("isMoving", true);
            if(direction.x > 0) {
                spriteRenderer.flipX = false;
            }
            else {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void LockMovement() {
        print("LOCKING MOVEMENT");
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

}
