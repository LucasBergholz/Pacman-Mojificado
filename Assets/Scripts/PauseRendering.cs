using System.Collections;
using UnityEngine;

public class PauseRendering : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void PauseForSeconds(float duration)
    {
        StartCoroutine(PauseRenderingCoroutine(duration));
    }

    private IEnumerator PauseRenderingCoroutine(float duration)
    {
        if (cam != null)
        {
            cam.enabled = false; // Desativa a câmera (tela para de atualizar)
        }

        yield return new WaitForSeconds(duration);

        if (cam != null)
        {
            cam.enabled = true; // Reativa a câmera após o tempo definido
        }
    }
}
