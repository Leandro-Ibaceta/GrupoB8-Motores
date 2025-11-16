using UnityEngine;
using TMPro;

public class UIDialogue : MonoBehaviour
{
    public GameObject container;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Dialogue dialogue;
    private int currentIndex;
    private IDialogue owner;

    private void Start()
    {
        GameEvents.OnDialogueStarted += StartDialogue;
    }
     private void OnDestroy()
    {
         GameEvents.OnDialogueStarted -= StartDialogue;
    }

    private void StartDialogue(IDialogue dialogue)
    {
        owner = dialogue;
        container.SetActive(true);
        this.dialogue = dialogue.DialogueData;

        if (dialogue == null || this.dialogue.Lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentIndex = 0;

        ShowLine();
    }
    

    public void NextLines()
    {
        currentIndex++;
        if (currentIndex >= dialogue.Lines.Count)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    private void ShowLine()
    {
        nameText.text = dialogue.Lines[currentIndex].Speaker;
        dialogueText.text = dialogue.Lines[currentIndex].Text;
    }

    private void EndDialogue()
    {
        GameEvents.TriggerDialogueEnded();
        owner.EndConversation();
        container.SetActive(false);
    }
    

}