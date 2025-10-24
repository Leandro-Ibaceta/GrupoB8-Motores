using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLevelTrigger : MonoBehaviour
{
    [Header("Cinematic UI")]
    public GameObject cinematicUI;       // Canvas negro
    public TextMeshProUGUI textDisplay;  // Texto que se escribe
    public float typingSpeed = 0.04f;    // Velocidad del typer

    [Header("Story")]
    [TextArea(3, 6)]
    public string[] storyLines;               // Líneas que se van mostrando
    public float timeBetweenLines = 2f;       // Tiempo entre una línea y otra

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PlayCinematic());
        }
    }

    private IEnumerator PlayCinematic()
    {
        cinematicUI.SetActive(true);
        yield return new WaitForSeconds(0.5f); // pequeño delay opcional

        foreach (string line in storyLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(timeBetweenLines);
        }

        // Al terminar todas las líneas
        if (!string.IsNullOrEmpty("WinScene"))
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            // O simplemente cerrás la UI
            cinematicUI.SetActive(false);
        }
    }

    private IEnumerator TypeLine(string line)
    {
        textDisplay.text = "";
        foreach (char letter in line.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
