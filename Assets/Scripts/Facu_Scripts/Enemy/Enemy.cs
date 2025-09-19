using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    private CheckPointManager _checkPointManager;

    private void Start()
    {
        _checkPointManager = CheckPointManager.instance;
    }

    public abstract void Attack();
    public virtual void Neutralize()
    {
        _checkPointManager.DeleteEnemy(gameObject.name);
        gameObject.SetActive(false);
    }
   

    
}
