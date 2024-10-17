using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private int numJump;
    private Vector2 startPos;
    private int score;
    private SpriteRenderer sr;

    [SerializeField] Animator animator;
    [SerializeField] int speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        numJump = 0;
        startPos = transform.position;
        score = 0;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        rb.AddForce(new Vector2(0, 100));
        if (collision.gameObject.CompareTag("Ground")) {
            numJump = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = movementVector;
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
    }

    void OnRestart() {
        Debug.Log("In restart");
        transform.position = startPos;
    }

    void OnTeleport() {
        Debug.Log("In teleport");
        transform.position = transform.position + new Vector3(5, 0, 0);
    }

    void OnMove(InputValue value) {
        movementVector = value.Get<Vector2>();
        animator.SetBool("Walk_Right", !Mathf.Approximately(movementVector.x, 0));
        sr.flipX = movementVector.x < 0;
    }

    void OnJump(InputValue value) {
        if (numJump < 2) {
            rb.AddForce(new Vector2(0, 200));
        }
        numJump++;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Collectable")) {
            other.gameObject.SetActive(false);
            score++;
            Debug.Log("My score is: " + score);
        }
    }

    
}
