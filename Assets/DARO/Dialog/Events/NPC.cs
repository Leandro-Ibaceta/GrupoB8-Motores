using UnityEngine;

public class NPC : MonoBehaviour, IDialogue
{
    public Dialogue Dialogue;
    public Dialogue DialogueData => Dialogue;

    private void OnEnable()
    {
       
    }
    private void OnDisable()
    {
       
    }

    public void EndConversation()
    {
        Debug.Log("Termine: " + gameObject.name);
    }
    
    public void StartConversation()
    {
        GameEvents.TriggerDialogueStarted(this);
    }
}
