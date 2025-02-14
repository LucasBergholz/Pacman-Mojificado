/*using System.Collections;
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
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhost : MonoBehaviour
{
    private Rigidbody2D rb;
    private char[] direction = { 'E', 'S', 'W', 'N' };
    private char currentDirection;
    private int indexDirection = 0;
    private float moveDistance = 1f;
    private float checkRadius = 0.2f; // Tamanho da detecção da colisão

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDirection = direction[0];
    }

    public void Movement()
    {
        Vector3 moveVector = GetMoveVector(currentDirection);
        Vector3 targetPosition = transform.position + moveVector * moveDistance;

        // Verifica se a posição destino colidiria com uma parede
        if (IsWallAtPosition(targetPosition))
        {
            ChangeDirection();
            Movement(); // Tenta mover na nova direção
        }
        else
        {
            rb.MovePosition(targetPosition);
        }
    }

    private Vector3 GetMoveVector(char dir)
    {
        switch (dir)
        {
            case 'N': return new Vector3(0, 1);
            case 'S': return new Vector3(0, -1);
            case 'E': return new Vector3(1, 0);
            case 'W': return new Vector3(-1, 0);
            default: return Vector3.zero;
        }
    }

    private void ChangeDirection()
    {
        indexDirection = (indexDirection + 1) % direction.Length;
        currentDirection = direction[indexDirection];
    }

    private bool IsWallAtPosition(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, checkRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Wall")) return true;
        }
        return false;
    }
}



