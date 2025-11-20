using UnityEngine;

public class PlayerDialogueLock : MonoBehaviour
{
    public MonoBehaviour cameraController;
    public PlayerMovement player;

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

        player._canMove = false;
        player.enabled = false;

        var anim = player.GetComponent<PlayerAnimation>();
        if (anim != null)
        {
            anim.SetLocked(true);        
            anim.ChangePlayerSpeed(0);   
            anim.ChangeStanceValue(0);
            anim.ChangeAnimationSpeed(0);
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

        player._canMove = true;
        player.enabled = true;

        var anim = player.GetComponent<PlayerAnimation>();
        if (anim != null)
        {
            anim.SetLocked(false);       
            anim.ChangeAnimationSpeed(1);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
