using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractableObject nearbyObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TryInteract();
    }

    public void SetNearbyObject(InteractableObject obj)
    {
        nearbyObject = obj;
    }
    public void ClearNearbyObject(InteractableObject obj)
    {
        if (nearbyObject == obj)
            nearbyObject = null;
    }

    private void TryInteract()
    {
        if (nearbyObject != null)
        {
            nearbyObject.Interact();
        }
    }
}
