using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   
    private LayerMask _playerLayer;

    private void Start()
    {
        _playerLayer = PlayerManager.instance.PlayerLayer;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _playerLayer) != 0)
        {
            GameManager.instance.SetCheckpoint(transform.position);
        }

    }

}
