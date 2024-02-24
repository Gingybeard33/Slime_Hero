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
        // rb = GetComponent<Rigidbody2D>();
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
        Vector2 direction = new Vector2();
        switch(movementStyle) {
            case MovementStyle.Scan:
                break;
            case MovementStyle.Follow:
                Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y - .5f);
                direction = target - new Vector2(transform.position.x, transform.position.y);
                direction.Normalize();

                float step = moveSpeed * Time.fixedDeltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target, step);
                isMoving = true;
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


    void OnCollisionEnter2D(Collision2D other){
        other.gameObject.GetComponent<Player>().Damage(bodyDamage);
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

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        canAttack = 0f;
    }

}
