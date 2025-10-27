using UnityEngine;

public class WinPoint : MonoBehaviour
{
   private PlayerManager _playerManager;
    public EndLevelTrigger cinematicTrigger;

    private void Start()
    {
        _playerManager = GameManager.instance.PlayerManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_playerManager.CompareLayer(other.gameObject.layer))
        {
            cinematicTrigger.StartCinematic();
        }
    }

}
