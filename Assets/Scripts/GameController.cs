using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do Pacman
    public GameObject pacman, ghostRed, ghostGreen, ghostBlue, fruitRed, fruitGreen, fruitBlue;
    public GameObject[] portal;
    private int score = 0;
    public TMP_Text scoreText;
    public bool semaforo = false;
    public bool gameOver = false;
    public bool validating = false;
    public GameObject gameOverUI;
    public TMP_Text gameOverText;
    public Button validateButton;
    public TMP_InputField validateText;

    void Start()
    {
        validateButton.onClick.AddListener(Wrapper);

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
        if (pacman == null)
        {
            gameOver = true;
            gameOverUI.SetActive(true);
            gameOverText.text = $"Gameover!";
        }
        if (ghostRed == null && ghostGreen == null && ghostBlue == null)
        {
            gameOver = true;
            gameOverUI.SetActive(true);
            gameOverText.text = $"Vitoria!\nScore = {score}";
        }
        if (pacman != null && !semaforo && !gameOver && !validateText.isFocused && !validating)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(HandleMovement('W'));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(HandleMovement('E'));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(HandleMovement('N'));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(HandleMovement('S'));
            }
        }
    }

    public void GhostsMovement(char direction)
    {
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

    IEnumerator HandleMovement(char direction)
    {
        // Primeiro Pacman
        if (pacman != null)
        {
            yield return new WaitForSeconds(0.1f);
            PacmanScript scriptP = pacman.GetComponent<PacmanScript>();
            scriptP.Movement(direction);
            yield return new WaitForSeconds(0.1f);
            while (semaforo) yield return new WaitForSeconds(0.1f);
            GhostsMovement(direction);
            yield return new WaitForSeconds(0.1f);
        }

    }

    void Wrapper()
    {
        if (validating) return;
        StartCoroutine(ValidateButton());
    }
    IEnumerator ValidateButton()
    {
        String movements = validateText.text;
        string[] parts = movements.Split(';');
        string result = "";
        validating = true;

        foreach (string part in parts)
        {

            bool isLetterOnly = true;
            foreach (char c in part)
            {
                if (!char.IsLetter(c))
                {
                    isLetterOnly = false;
                    break;
                }
            }

            if (isLetterOnly) result += part;
        }

        result.ToUpper();
        foreach(char c in result)
        {
            if (!gameOver)
            {
                yield return new WaitForSeconds(0.3f);
                while (semaforo) yield return new WaitForSeconds(0.3f);
                StartCoroutine(HandleMovement(c));
            }
        }
        validating = false;
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
