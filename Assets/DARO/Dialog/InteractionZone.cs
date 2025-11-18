using System;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public GameObject promptUI;
    private bool playerInside = false;

    private NPC npc;  

    private void Awake()
    {
        npc = GetComponent<NPC>();   
    }

    private void OnEnable()
    {
        GameEvents.OnDialogueStarted += HandleAnyDialogueStarted;
        GameEvents.OnDialogueEnded += HandleDialogueEnded;
    }

    private void OnDisable()
    {
        GameEvents.OnDialogueStarted -= HandleAnyDialogueStarted;
        GameEvents.OnDialogueEnded -= HandleDialogueEnded;
    }

    private void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (promptUI != null)
            {
                promptUI.SetActive(true);

            }
            Debug.Log("ENTRE AL TRIGGER con: " + other.name);
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (promptUI != null)
            {
                promptUI.SetActive(false);

            }
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (promptUI != null)
            {
                promptUI.SetActive(false);

            }

            GameEvents.TriggerDialogueStarted(npc);
        }
    }

    private void HandleAnyDialogueStarted(IDialogue startedDialogue)
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    private void HandleDialogueEnded()
    {
        if (playerInside && promptUI != null)
            promptUI.SetActive(true);
    }
}
