using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhost : MonoBehaviour
{
    private Rigidbody2D rb;
    private char[] direction = {'E', 'S', 'W', 'N'};
    private char currentDirection;
    private int indexDirection = 0;
    private Vector2 initialPosition;
    private bool hitWall = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDirection = direction[0];
    }
    public void Movement()
    {
        initialPosition = this.transform.position;
        if (currentDirection == 'N')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, 1));
        }
        else if (currentDirection == 'S')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, -1));
        }
        else if (currentDirection == 'E')
        {
            rb.MovePosition(this.transform.position + new Vector3(1, 0));
        }
        else if (currentDirection == 'W')
        {
            rb.MovePosition(this.transform.position + new Vector3(-1, 0));
        }

        StartCoroutine(WaitForMovement());
    }

    IEnumerator WaitForMovement()
    {
        yield return new WaitForSeconds(0.1f);
        if (hitWall)
        {
            hitWall = false;
            Movement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            this.transform.position = initialPosition;
            if (indexDirection == 3)
            {
                indexDirection = 0;
            } else indexDirection += 1;
            currentDirection = direction[indexDirection];
            hitWall = true;
        }
    }
}
