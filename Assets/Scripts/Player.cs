using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 6.25f;
    [SerializeField] private bool isOnTheFloor = true;
    [SerializeField] private Vector3 defaultPosition = new Vector3(3.92f, 3.3f, 113.834f);

    private Rigidbody2D rb;
    private SpriteRenderer sp;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the contact points
        ContactPoint2D contact = collision.contacts[0];

        // Get the normal of the contact point
        Vector2 normal = contact.normal;

        // The normal of the contact point indicates the side of the collision
        if (collision.gameObject.tag == "Ground" && normal.y > 0)
        {
            isOnTheFloor = true;
        }
    }


    private void Jump()
    {
        if (isOnTheFloor)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isOnTheFloor = false;
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + direction,
            speed * Time.deltaTime
        );

        sp.flipX = direction.x < 0f;
    }

    public void Respawn()
    {
        transform.position = defaultPosition;
    }
}
