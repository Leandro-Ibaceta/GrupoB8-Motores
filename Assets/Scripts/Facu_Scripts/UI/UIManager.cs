using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _controlMenu;
    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private GameObject _HUD;
    // private MainMenu _mainMenu
    // private PopUpMessage
    private PlayerInputs _inputs;
    private GameObject _activeMenu;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public GameObject ControlMenu
    {
        get { 
            if(_controlMenu == null)
            {
                return FindAnyObjectByType<ControlMenu>().gameObject;
            }
            return _controlMenu; }
    }
    public GameObject InGameMenu
    {
        get
        {
            if (_inGameMenu == null)
            {
                return FindAnyObjectByType<InGameMenu>().gameObject;
            }
            return _inGameMenu;
        }
    }
    private void Start()
    {
        _inputs = GameManager.instance.Inputs;
        _activeMenu = _inGameMenu;


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
        Debug.Log(activeMenu.name);
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
