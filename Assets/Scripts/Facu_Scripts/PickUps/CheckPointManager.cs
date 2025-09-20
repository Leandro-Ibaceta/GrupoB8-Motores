using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;


   
    private List<string> _deletedPickUps = new List<string>();
    private List<string> _deletedEnemies = new List<string>();

    private void Awake()
    {

    
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += UpdateCheckPoint;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    } //singleton pattern

    public void ResetAll()
    {
        _deletedPickUps.Clear();
        _deletedEnemies.Clear();
    }

    public void UpdateCheckPoint(Scene SceneManager , LoadSceneMode mode)
    {
        foreach (string pickUp in _deletedPickUps)
        {
            GameObject.Find(pickUp).SetActive(false);
        }
        foreach (string enemy in _deletedEnemies)
        {
            GameObject.Find(enemy).SetActive(false);
        }

    }


   public void DeletePickUp(string pickUp)
    {
        // Evita agregar duplicados
        if (!_deletedPickUps.Contains(pickUp))
        {
            // Agrega el pickup a la lista de eliminados y desactívalo
            _deletedPickUps.Add(pickUp);
            GameObject.Find(pickUp).SetActive(false); 
        }
    }
    public void DeleteEnemy(string enemy)
    {
        // Evita agregar duplicados
        if (!_deletedEnemies.Contains(enemy))
        {
            // Agrega el pickup a la lista de eliminados y desactívalo
            _deletedEnemies.Add(enemy);
           
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= UpdateCheckPoint;
    }
}



