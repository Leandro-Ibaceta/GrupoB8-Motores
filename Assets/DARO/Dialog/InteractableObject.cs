using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent interact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            player.SetNearbyObject(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            player.ClearNearbyObject(this);
        }
    }

    public void Interact()
    {
        interact?.Invoke();
    }
}
