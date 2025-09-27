using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    

    [SerializeField] private GameObject _controlMenu;
    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private GameObject _HUD;
    // private MainMenu _mainMenu
    // private PopUpMessage
    private PlayerInputs _inputs;
    private GameObject _activeMenu;
    


    public GameObject ControlMenu
    {
        get { 
            if(_controlMenu == null)
            {
                return FindFirstObjectByType<ControlMenu>(FindObjectsInactive.Include).gameObject;
            }
            return _controlMenu; }
    }
    public GameObject InGameMenu
    {
        get
        {
            if (_inGameMenu == null)
            {
                return FindFirstObjectByType<InGameMenu>(FindObjectsInactive.Include).gameObject;
            }
            return _inGameMenu;
        }
    }
    private void Start()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            _inGameMenu = FindFirstObjectByType<InGameMenu>(FindObjectsInactive.Include).gameObject;
            _controlMenu = FindFirstObjectByType<ControlMenu>(FindObjectsInactive.Include).gameObject;
        };
        _inputs = GameManager.instance.Inputs;
        _activeMenu = InGameMenu;
    }
    private void Update()
    {

        if(_inputs.IsEscapeClicked)
        {
            ActiveMenu(_activeMenu);
        }
    }

    public void ActiveMenu(GameObject activeMenu)
    {
       
        if(activeMenu == null) _activeMenu = GameObject.Find(activeMenu.name);
        else _activeMenu = activeMenu;

        if (!_activeMenu.activeSelf)
        {
            Time.timeScale = 0;
            _inputs.ChangeCursorLockState(CursorLockMode.Confined);
            _activeMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _inputs.ChangeCursorLockState(CursorLockMode.Locked);
            _activeMenu.SetActive(false);
        }
    }

}
