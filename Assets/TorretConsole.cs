using UnityEngine;

public class TorretConsole : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private PlayerInputs _inputs;
    private PlayerManager _playerManager;


    private void Start()
    {
        _playerManager = PlayerManager.instance;
        _inputs = GameManager.instance.Inputs;
    }

    private void OnTriggerStay(Collider other)
    {
        _enemyManager = GameManager.instance.EnemyManager;
        if (_playerManager.CompareLayer(other.gameObject.layer))
        {
            if(_inputs.IsInteractClicked)
                foreach (Enemy enemy in _enemyManager.Enemies)
                {
                    if (enemy is Turret turret)
                    {
                        turret.Neutralize();
                    }
                }
        }
    }

}
