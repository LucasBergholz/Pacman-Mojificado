using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{

    public Camera cam;       // Refer�ncia � c�mera
    public Slider slider;    // Refer�ncia ao Slider

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(UpdateCameraZoom);
    }

    void UpdateCameraZoom(float value)
    {
        cam.orthographicSize = value;
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
