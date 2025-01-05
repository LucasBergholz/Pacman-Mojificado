using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    //private string map = "##########\n#P$*****B#\n#*######*#\n#*#  R #*#\n#@# *! #*#\n#*#    #*#\r\n#*##**##*#\n#********#\n#        #\n#G       #\n##########";
    private string map;

    public GameObject[] prefabs;        // Array de prefabs que serão instanciados para cada caractere

    private int xCamera, yCamera;

    public GameObject cam;

    public GameObject gameController;

    void Start()
    {
        // Adiciona um listener para o evento de submissão do input
        Mapa mapa = GameObject.FindGameObjectWithTag("Map").GetComponent<Mapa>();
        map = mapa.map;
        GenerateGameObjectsFromInput(map);
        xCamera = 0;
        yCamera = 0;
    }

    // Método para gerar gameobjects baseado no input
    void GenerateGameObjectsFromInput(string inputText)
    {
        int x = 0, y = 0;  // Coordenadas iniciais de posicionamento dos GameObjects

        foreach (char character in inputText)
        {
            // Pula linha se encontrar uma quebra de linha
            if (character == '\n')
            {
                x = 0;
                y--;  // Move para a proxima linha no eixo Y
                continue;
            }

            if (character == 32)
            {
                x++; // Pula um espaco
                continue;
            }

            if (character == '#')
            {
                if (x > xCamera) xCamera = x; // Achar o maior X
                if (y < yCamera) yCamera = y; // Achar o menor Y
            }

            // Verifica se o caractere tem um prefab associado
            int prefabIndex = GetPrefabIndexForCharacter(character);
            if (prefabIndex >= 0 && prefabIndex < prefabs.Length)
            {
                Vector3 position = new Vector3(x, y, 0);
                Instantiate(prefabs[prefabIndex], position, Quaternion.identity);
            }

            x++;  // Move para a próxima posição no eixo X
        }
        cam.transform.position = new Vector3(xCamera/2, yCamera/2, -10);
        gameController.SetActive(true);
    }

    // Função para mapear caracteres para índices de prefabs
    int GetPrefabIndexForCharacter(char character)
    {
        switch (character)
        {
            case '#': return 0;  // Associa o caractere 'A' ao primeiro prefab
            case 'P': return 1;
            case '*': return 2;
            case 'R': return 3;
            case 'G': return 4;
            case 'B': return 5;
            case '!': return 6;
            case '@': return 7;
            case '$': return 8;
            case 'I': return 9;
            case 'O': return 10;
            default: return -1;  // Retorna -1 se o caractere não tiver prefab associado
        }
    }
}
