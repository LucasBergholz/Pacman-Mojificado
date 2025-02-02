using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacmanScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 initialPosition;
    private char activeFruit = 'N';
    private bool isWall = false;
    private bool isCoin = false;
    private bool hasFruit = false;
    private GameController gameController;
    private bool hasTeleported = false;
    private bool inIce = false;
    private char directionForIce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void Movement(char direction)
    {
        initialPosition = this.transform.position;
        directionForIce = direction;
        inIce = false;
        if (direction == 'N')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, 1));
        } else if (direction == 'S')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, -1));
        } else if (direction == 'E')
        {
            rb.MovePosition(this.transform.position + new Vector3(1, 0));
        } else if (direction == 'W')
        {
            rb.MovePosition(this.transform.position + new Vector3(-1, 0));
        }

        StartCoroutine(AddScore());
        if (activeFruit != 'N') hasFruit = true;
    }

    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(0.1f);
        if (isCoin) gameController.AddScore(1);
        else if (isWall && activeFruit == 'N') gameController.AddScore(4);
        else if (isWall) gameController.AddScore(8);
        else if (hasFruit) gameController.AddScore(4);
        else gameController.AddScore(2);

        if (inIce && isWall && !hasFruit) gameController.AddScore(-4);
        else if (inIce && isWall && hasFruit) gameController.AddScore(-8);

        isWall = false;
        isCoin = false;
    }

    public IEnumerator MovingInIce(char direction)
    {
        while (inIce) {
            if (gameController.gameOver) yield break;
            if (isWall)
            {
                if (direction == 'N') direction = 'S';
                else if (direction == 'S') direction = 'N';
                else if (direction == 'E') direction = 'W';
                else if (direction == 'W') direction = 'E';
                Debug.Log("Parede");
                if (activeFruit == 'N') gameController.AddScore(-2);
                else gameController.AddScore(-4);
                isWall = false;
            }
            if (direction == 'N')
            {
                rb.MovePosition(this.transform.position + new Vector3(0, 1));
            }
            else if (direction == 'S')
            {
                rb.MovePosition(this.transform.position + new Vector3(0, -1));
            }
            else if (direction == 'E')
            {
                rb.MovePosition(this.transform.position + new Vector3(1, 0));
            }
            else if (direction == 'W')
            {
                rb.MovePosition(this.transform.position + new Vector3(-1, 0));
            }

            yield return new WaitForSeconds(0.1f);
            initialPosition = this.transform.position;
            if (hasFruit) gameController.AddScore(4);
            else gameController.AddScore(2);
            GameObject target = GameObject.FindGameObjectWithTag("BlockOfIce");
            Collider2D myCollider = GetComponent<Collider2D>();
            if (target != null && !myCollider.IsTouching(target.GetComponent<Collider2D>()))
            {
                gameController.semaforo = false;
                break;
            }
        }
    }

    public IEnumerator CollidingWithFruit(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        if (collision.gameObject.name == "RedFruit(Clone)")
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            activeFruit = 'R';
        }
        else if (collision.gameObject.name == "GreenFruit(Clone)")
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
            activeFruit = 'G';
        }
        else if (collision.gameObject.name == "BlueFruit(Clone)")
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
            activeFruit = 'B';
        }
        Destroy(collision.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            this.transform.position = initialPosition;
            isWall = true;
        }
        else if (collision.gameObject.CompareTag("Fruit"))
        {
            StartCoroutine(CollidingWithFruit(collision));
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            isCoin = true;
        }
        else if (collision.gameObject.CompareTag("RedGhost"))
        {
            if (activeFruit == 'R')
            {
                Destroy(collision.gameObject);
                activeFruit = 'N';
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("GreenGhost"))
        {
            if (activeFruit == 'G')
            {
                Destroy(collision.gameObject);
                activeFruit = 'N';
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("BlueGhost"))
        {
            if (activeFruit == 'B')
            {
                Destroy(collision.gameObject);
                activeFruit = 'N';
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Portal") && !hasTeleported && !isWall)
        {
            if (this.transform.position == gameController.portal[0].transform.position) rb.MovePosition(gameController.portal[1].transform.position);
            else if (this.transform.position == gameController.portal[1].transform.position) rb.MovePosition(gameController.portal[0].transform.position);
            hasTeleported = true;
        }
        else if (collision.gameObject.CompareTag("Portal") && hasTeleported) hasTeleported = false;
        else if (collision.gameObject.CompareTag("BlockOfIce") && !isWall)
        {
            Debug.Log("BLOCO");
            inIce = true;
            gameController.semaforo = true;
            StartCoroutine(MovingInIce(directionForIce));
        }
    }
}
