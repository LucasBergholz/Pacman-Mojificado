using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do Pacman
    private GameObject pacman, ghostRed, ghostGreen, ghostBlue, fruitRed, fruitGreen, fruitBlue;
    public GameObject[] portal;
    private int score = 0;
    public TMP_Text scoreText;
    public Stack<int> movementStack = new Stack<int>();

    void Start()
    {
        // Encontra o GameObject com a tag "pacman"
        pacman = GameObject.FindGameObjectWithTag("Pacman");
        ghostRed = GameObject.Find("RedGhost(Clone)");
        ghostGreen = GameObject.Find("GreenGhost(Clone)");
        ghostBlue = GameObject.Find("BlueGhost(Clone)");
        fruitRed = GameObject.Find("RedFruit(Clone)");
        fruitGreen = GameObject.Find("GreenFruit(Clone)");
        fruitBlue = GameObject.Find("BlueFruit(Clone)");

        portal = GameObject.FindGameObjectsWithTag("Portal");

        GameObject ices = GameObject.Find("Ices");
        GameObject[] iceObjects = GameObject.FindGameObjectsWithTag("Ice"); // Ou use o nome "Ice(Clone)" diretamente

        // Para cada objeto de gelo encontrado, defina seu transform como filho de "Ices"
        foreach (GameObject ice in iceObjects)
        {
            ice.transform.SetParent(ices.transform);
        }

        if (pacman == null)
        {
            Debug.LogError("Nenhum GameObject com a tag 'pacman' foi encontrado na cena.");
        }
    }

    void Update()
    {
        if (pacman != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                HandleMovement('W');
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                HandleMovement('E');
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                HandleMovement('N');
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                HandleMovement('S');
            }
        }
    }

    IEnumerator GhostsMovement(char direction)
    {
        yield return new WaitForSeconds(0.3f);
        while (movementStack.Count > 0) yield return new WaitForSeconds(0.3f);
        // Fantasma Vermelho
        if (ghostRed != null)
        {
            RedGhost scriptRG = ghostRed.GetComponent<RedGhost>();
            scriptRG.Movement();
        }

        // Fantasma Verde
        if (ghostGreen != null)
        {
            GreenGhost scriptGG = ghostGreen.GetComponent<GreenGhost>();
            scriptGG.Movement(direction);
        }

        // Fantasma Azul
        if (ghostBlue != null)
        {
            BlueGhost scriptBG = ghostBlue.GetComponent<BlueGhost>();
            scriptBG.Movement(direction);
        }
    }

    void HandleMovement(char direction)
    {
        // Primeiro Pacman
        if (pacman != null)
        {
            PacmanScript scriptP = pacman.GetComponent<PacmanScript>();
            scriptP.Movement(direction);
        }

        StartCoroutine(GhostsMovement(direction));
    }

    public void AddScore(int increment)
    {
        score += increment;
        scoreText.text = $"SCORE = {score}";
    }

    public int GetScore()
    {
        return score;
    }
}
