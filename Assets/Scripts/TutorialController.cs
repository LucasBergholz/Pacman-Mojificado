using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    /* Pacman Mojificado é uma adaptação do clássico jogo Pac-Man, desenvolvido por Toru Iwatani para a empresa Namco.

Esta versão foi desenvolvida para a disciplina de Fundamentos Lógicos de Inteligência Artificial (FLIA) da Universidade de Brasília (UnB).
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

        tutorialTexts[0] = "Pacman Mojificado é uma adaptação do clássico jogo Pac-Man, desenvolvido por Toru Iwatani para a empresa Namco.\n Esta versão foi desenvolvida para a disciplina de Fundamentos Lógicos de Inteligência Artificial (FLIA) da Universidade de Brasília (UnB).";
        tutorialTexts[1] = "Em FLIA, os alunos da disciplina precisam modelar, utilizando a linguagem PDDL, o jogo Pacman Mojificado, de forma que seus códigos retornem a solução de cada um dos mapas.";
        tutorialTexts[2] = "Dessa forma, este jogo serve para os estudantes jogarem o jogo, mas também testar os planos gerados por seus códigos em PDDL";
        tutorialTexts[3] = "Como jogar?\n Para jogar Pacman Mojificado, primeiro você deve inserir (ou selecionar) um mapa de jogo, seguindo as regras de entrada para o jogo. Depois, com as teclas WASD ou com as setinhas, você pode movimentar o Pacman.\n Para vencer o jogo, o jogador deve matar todos os fantasmas!";
        tutorialTexts[4] = "O jogo possui uma pontuação, e, um objetivo secundário é atingir a menor pontuação possível!\n Cada fantasma possui um movimento único e padronizado: o Vermelho anda em sentido horário, o Verde faz o mesmo movimento do Pacman e o Azul faz o movimento contrário.";
        tutorialTexts[5] = "Existem três tipos diferentes de chão, os quais só afetam o Pacman:\n Chão de Gelo: o Pacman desliza por ele até chegar em outro tipo de chão. Caso tenha uma parede no caminho, ele rebaterá nela.\n Chão de Portal: o Pacman é teletransportado para o outro portal do mapa.\n Chão normal: é o chão comum do jogo.";
        tutorialTexts[6] = "Cada movimento do Pacman tem um custo, e eles são:\n 1- Movimento padrão = 2;\n 2- Comer pastilha = 1;\n 3- Andar com alguma fruta ativa = 4;\n 4- Cada célula que andar no gelo terá seu custo adicionado;\n 5- Andar no portal tem o mesmo custo de andar para uma célula normal;";
        tutorialTexts[7] = "O último caso de movimento é chamado de \"Dummy Move\". O Dummy Move consiste no Pacman andar na direção de uma parede.\n\nCaso isso ocorra, os fantasmas andarão normalmente, conforme a direção do Dummy Move, e o custo do movimento será o dobro do custo que seria andar para uma célula normal.";
        tutorialTexts[8] = "As regras de entrada de um mapa são:\n # : Paredes do mapa;\r\n* : Pastilhas;\r\n (Espaço Vazio) : Célula vazia;\r\nI : Chão de gelo;\r\nO : Chão de portal;\r\nP : Pac-Man;";
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
