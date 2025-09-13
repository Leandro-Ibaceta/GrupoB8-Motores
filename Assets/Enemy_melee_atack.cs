using UnityEngine;

public class Enemy_melee_atack : MonoBehaviour
{
    [SerializeField] private Collider hitCollider;


    public void ActivateHitCOllider()
    {
        hitCollider.enabled = true;
    }
    public void DeactivateHitCOllider()
    {
        hitCollider.enabled = false;
    }

}
