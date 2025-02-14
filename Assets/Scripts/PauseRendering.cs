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
            cam.enabled = false; // Desativa a c�mera (tela para de atualizar)
        }

        yield return new WaitForSeconds(duration);

        if (cam != null)
        {
            cam.enabled = true; // Reativa a c�mera ap�s o tempo definido
        }
    }
}
