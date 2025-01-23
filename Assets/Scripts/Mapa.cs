using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mapa : MonoBehaviour
{
    public string map;
    private TMP_InputField inputField;
    public static Mapa instance;
    private Button button, tutorial;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Tutorial") return;
        button = GameObject.FindGameObjectWithTag("Button").GetComponent<Button>();
        tutorial = GameObject.Find("Tutorial").GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ChangeScene);
        }
        if (tutorial != null)
        {
            tutorial.onClick.AddListener(GoToTutorial);
        }
    }

    public void ChangeScene()
    {
        inputField = GameObject.FindAnyObjectByType<TMP_InputField>();
        map = inputField.text;
        Debug.Log(map);
        SceneManager.LoadScene("Template");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
