using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    /* Pacman Mojificado � uma adapta��o do cl�ssico jogo Pac-Man, desenvolvido por Toru Iwatani para a empresa Namco.

Esta vers�o foi desenvolvida para a disciplina de Fundamentos L�gicos de Intelig�ncia Artificial (FLIA) da Universidade de Bras�lia (UnB).
    */


    [SerializeField]
    private TMP_Text title, textField, page;
    private string[] tutorialTexts = new string[10];
    private Button next, back, exit;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        exit = GameObject.Find("Exit").GetComponent<Button>();
        exit.onClick.AddListener(Exit);

        next = GameObject.Find("Next").GetComponent<Button>();
        next.onClick.AddListener(ChangeText);

        back = GameObject.Find("Back").GetComponent<Button>();
        back.onClick.AddListener(ChangeText);

        tutorialTexts[0] = "Pacman Mojificado � uma adapta��o do cl�ssico jogo Pac-Man, desenvolvido por Toru Iwatani para a empresa Namco.\n Esta vers�o foi desenvolvida para a disciplina de Fundamentos L�gicos de Intelig�ncia Artificial (FLIA) da Universidade de Bras�lia (UnB).";
        tutorialTexts[1] = "Em FLIA, os alunos da disciplina precisam modelar, utilizando a linguagem PDDL, o jogo Pacman Mojificado, de forma que seus c�digos retornem a solu��o de cada um dos mapas.";
        tutorialTexts[2] = "Dessa forma, este jogo serve para os estudantes jogarem o jogo, mas tamb�m testar os planos gerados por seus c�digos em PDDL";
        tutorialTexts[3] = "Como jogar?\n Para jogar Pacman Mojificado, primeiro voc� deve inserir (ou selecionar) um mapa de jogo, seguindo as regras de entrada para o jogo. Depois, com as teclas WASD ou com as setinhas, voc� pode movimentar o Pacman.\n Para vencer o jogo, o jogador deve matar todos os fantasmas!";
        tutorialTexts[4] = "O jogo possui uma pontua��o, e, um objetivo secund�rio � atingir a menor pontua��o poss�vel!\n Cada fantasma possui um movimento �nico e padronizado: o Vermelho anda em sentido hor�rio, o Verde faz o mesmo movimento do Pacman e o Azul faz o movimento contr�rio.";
        tutorialTexts[5] = "Existem tr�s tipos diferentes de ch�o, os quais s� afetam o Pacman:\n Ch�o de Gelo: o Pacman desliza por ele at� chegar em outro tipo de ch�o. Caso tenha uma parede no caminho, ele rebater� nela.\n Ch�o de Portal: o Pacman � teletransportado para o outro portal do mapa.\n Ch�o normal: � o ch�o comum do jogo.";
        tutorialTexts[6] = "Cada movimento do Pacman tem um custo, e eles s�o:\n 1- Movimento padr�o = 2;\n 2- Comer pastilha = 1;\n 3- Andar com alguma fruta ativa = 4;\n 4- Cada c�lula que andar no gelo ter� seu custo adicionado;\n 5- Andar no portal tem o mesmo custo de andar para uma c�lula normal;";
        tutorialTexts[7] = "O �ltimo caso de movimento � chamado de \"Dummy Move\". O Dummy Move consiste no Pacman andar na dire��o de uma parede.\n\nCaso isso ocorra, os fantasmas andar�o normalmente, conforme a dire��o do Dummy Move, e o custo do movimento ser� o dobro do custo que seria andar para uma c�lula normal.";
        tutorialTexts[8] = "As regras de entrada de um mapa s�o:\n # : Paredes do mapa;\r\n* : Pastilhas;\r\n (Espa�o Vazio) : C�lula vazia;\r\nI : Ch�o de gelo;\r\nO : Ch�o de portal;\r\nP : Pac-Man;";
        tutorialTexts[9] = "R : Fantasma Vermelho;\r\nG : Fantasma Verde;\r\nB : Fantasma Azul;\r\n! : Fruta Vermelha;\r\n@ : Fruta Verde;\r\n$ : Fruta Azul;";
        textField.text = tutorialTexts[0];
        title.text = "Sobre O Jogo";
        page.text = $"{index+1} de {tutorialTexts.Length}";
    }

    public void ChangeText()
    {
        //Check button clicked so decide to go back or next
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        if (clickedButton.name == "Next" && index != (tutorialTexts.Length - 1)) index++;
        else if (clickedButton.name == "Back" && index != 0) index--;
        else return;

        //Check slide to change title
        if (index >= 3) title.text = "Como Jogar";
        else title.text = "Sobre O Jogo";

        //Update main text and page
        textField.text = tutorialTexts[index];
        page.text = $"{index + 1} de {tutorialTexts.Length}";
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
