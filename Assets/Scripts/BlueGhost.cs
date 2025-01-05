using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGhost : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 initialPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Movement(char direction)
    {
        initialPosition = this.transform.position;
        if (direction == 'N')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, -1));
        }
        else if (direction == 'S')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, 1));
        }
        else if (direction == 'E')
        {
            rb.MovePosition(this.transform.position + new Vector3(-1, 0));
        }
        else if (direction == 'W')
        {
            rb.MovePosition(this.transform.position + new Vector3(1, 0));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            this.transform.position = initialPosition;
        }
        else if (collision.gameObject.CompareTag("Pacman")) Debug.Log("Pacman");
    }
}
