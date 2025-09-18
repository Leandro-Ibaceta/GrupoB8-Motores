using UnityEngine;

public class UIManager : MonoBehaviour
{
   public static UIManager _instance;
    private ToolBeltUI _toolBeltUI;

    public ToolBeltUI ToolBeltUI { get { return _toolBeltUI; } set { _toolBeltUI = value; } }




    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


}
