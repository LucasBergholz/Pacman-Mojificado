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
    private bool killedGhost = false;
    private char directionForIce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void MakeTheMove(char direction)
    {
        killedGhost = false;
        if (direction == 'N' || direction == 'n')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, 1));
        }
        else if (direction == 'S' || direction == 's')
        {
            rb.MovePosition(this.transform.position + new Vector3(0, -1));
        }
        else if (direction == 'E' || direction == 'e')
        {
            rb.MovePosition(this.transform.position + new Vector3(1, 0));
        }
        else if (direction == 'W' || direction == 'w')
        {
            rb.MovePosition(this.transform.position + new Vector3(-1, 0));
        }
        if (hasFruit) gameController.AddScore(4);
        else gameController.AddScore(2);
    }
    public void Movement(char direction)
    {
        //Debug.Log($"Parede: {isWall}");
        //Debug.Log($"Gelo: {inIce}");
        initialPosition = this.transform.position;
        directionForIce = direction;
        inIce = false;
        isWall = false;
        MakeTheMove(direction);
        hasTeleported = false;
    }

    public void AddScore()
    {
        if (killedGhost) return;
        if (hasFruit) gameController.AddScore(-4);
        else gameController.AddScore(-2);
        if (isWall && activeFruit == 'N') gameController.AddScore(4);
        else if (isWall) gameController.AddScore(8);
        else if (hasFruit) gameController.AddScore(4);
        else if (isCoin) gameController.AddScore(1);
        else gameController.AddScore(2);

        if (inIce && isWall && !hasFruit) gameController.AddScore(-4);
        else if (inIce && isWall && hasFruit) gameController.AddScore(-8);

        if(!inIce) isWall = false;
        isCoin = false;
    }

    public IEnumerator MovingInIce(char direction)
    {
        //gameController.AddScore(2);
        //if (hasFruit) gameController.AddScore(2);
        while (inIce) {
            Debug.Log("Interacao");
            if (gameController.gameOver) yield break;
            isWall = false;
            MakeTheMove(direction);

            yield return new WaitForSeconds(0.1f);
            if (isWall)
            {
                this.transform.position = initialPosition;
                if (direction == 'N') direction = 'S';
                else if (direction == 'S') direction = 'N';
                else if (direction == 'E') direction = 'W';
                else if (direction == 'W') direction = 'E';
                Debug.Log("Parede");
                gameController.AddScore(2);
                if (hasFruit) gameController.AddScore(2);
            }
            yield return new WaitForSeconds(0.1f);
            initialPosition = this.transform.position;
            GameObject target = GameObject.FindGameObjectWithTag("BlockOfIce");
            Collider2D myCollider = GetComponent<Collider2D>();
            if (target != null && !myCollider.IsTouching(target.GetComponent<Collider2D>()))
            {
                Debug.Log($"Parei isWall: {isWall}");
                gameController.semaforo = false;
                inIce = false;
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
        if (collision.gameObject.CompareTag("BlockOfIce") && !isWall)
        {
            //Debug.Log("GELADO");
            inIce = true;
            gameController.semaforo = true;
            StartCoroutine(MovingInIce(directionForIce));
            return;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (inIce) FindObjectOfType<PauseRendering>().PauseForSeconds(0.1f);
            hasTeleported = true;
            isWall = true;
            Debug.Log($"Wall e {isWall}");
            if (!inIce) this.transform.position = initialPosition;
        }
        else if (collision.gameObject.CompareTag("Fruit"))
        {
            hasFruit = true;
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
                killingGhost(collision);
            }
            else Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("GreenGhost"))
        {
            if (activeFruit == 'G')
            {
                killingGhost(collision);
            }
            else Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("BlueGhost"))
        {
            if (activeFruit == 'B')
            {
                killingGhost(collision);
            }
            else Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Portal") && !hasTeleported && !isWall)
        {
            if (this.transform.position == gameController.portal[0].transform.position) rb.MovePosition(gameController.portal[1].transform.position);
            else if (this.transform.position == gameController.portal[1].transform.position) rb.MovePosition(gameController.portal[0].transform.position);
            hasTeleported = true;
        }
        //else if (collision.gameObject.CompareTag("Portal") && hasTeleported) hasTeleported = false;
        //Debug.Log("Cheguei");
        AddScore();
    }

    public void killingGhost(Collider2D collision)
    {
        Destroy(collision.gameObject);
        activeFruit = 'N';
        hasFruit = false;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        killedGhost = true;
    }
}
