using UnityEngine;

public class PlayerDialogueLock : MonoBehaviour
{
    public MonoBehaviour cameraController; // el script que mueve la cámara

    private void OnEnable()
    {
        GameEvents.OnDialogueStarted += FreezePlayer;
        GameEvents.OnDialogueEnded += UnfreezePlayer;
    }

    private void OnDisable()
    {
        GameEvents.OnDialogueStarted -= FreezePlayer;
        GameEvents.OnDialogueEnded -= UnfreezePlayer;
    }

    private void FreezePlayer(IDialogue d)
    {
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UnfreezePlayer()
    {
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
