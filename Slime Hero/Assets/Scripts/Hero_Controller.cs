using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Hero_Controller : MonoBehaviour
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
    Player playerObj;
    Animator animator;

    public float bodyDamage = 2f;
    public float attackSpeed = 1f;
    public float moveSpeed = 2f;
    public float collisionOffset = .02f;
    private float canAttack;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        animator = GetComponent<Animator>();
        playerObj = player.GetComponent<Player>(); // Might not need
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        UseMoveStyle();
    }

    private void UseMoveStyle() {
        bool isMoving = false;
        Vector3 direction = new Vector3();
        switch(movementStyle) {
            case MovementStyle.Scan:
                break;
            case MovementStyle.Follow:
                Vector3 target = new Vector3(playerTransform.position.x, playerTransform.position.y - .5f);
                direction = target - transform.position;
                float buffer = .1f;
                if(direction.x > buffer || direction.x < -buffer || direction.y > buffer || direction.y < -buffer) 
                {
                    // Debug.DrawRay(transform.position, direction, Color.green);
                    direction.Normalize();
                    isMoving = TryMove(direction);


                    if(!isMoving) {
                        if(direction.x != 0) {
                            isMoving = TryMove(new Vector2(direction.x, 0));
                        }

                        if(!isMoving && direction.y != 0){
                            isMoving = TryMove(new Vector2(0, direction.y));
                        }
                    }
                }
                break;
            case MovementStyle.Hide:
                break;
        }

        if(isMoving) {
            // animator.SetBool("isMoving", true);
            if(direction.x > 0) {
                spriteRenderer.flipX = false;
            }
            else {
                spriteRenderer.flipX = true;
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

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        print("HERE1");
        if(other.gameObject.tag == "Player") {
            if(attackSpeed <= canAttack) {
                other.gameObject.GetComponent<Player>().Damage(bodyDamage);
                canAttack = 0f;
            }
            else {
                canAttack += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") {
            if(attackSpeed <= canAttack) {
                other.gameObject.GetComponent<Player>().Damage(bodyDamage);
                canAttack = 0f;
            }
            else {
                canAttack += Time.deltaTime;
            }
        }
    }

}
