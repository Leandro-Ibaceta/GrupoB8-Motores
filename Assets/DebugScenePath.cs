using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScenePath : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("ESCENA ACTIVA: " + SceneManager.GetActiveScene().path);
    }
}