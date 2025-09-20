using UnityEngine;

public class TorretConsole : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private LayerMask _playerLayer;


    private void Start()
    {
        _playerLayer = PlayerManager.instance.PlayerLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemyManager = GameManager.instance.EnemyManager;
        if ((1 << other.gameObject.layer & _playerLayer) != 0)
        {
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
