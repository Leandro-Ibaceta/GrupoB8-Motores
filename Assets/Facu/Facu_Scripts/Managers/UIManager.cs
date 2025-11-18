using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    

    [SerializeField] private GameObject _controlMenu;
    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private GameObject _HUD;
    [SerializeField] private float _popUpMessageTime = 2;
    [SerializeField] private float _fadeOutDuration = 0.5f; // tiempo del fade
    private Coroutine _messageCoroutine;

    [Header("Audio")]
    [SerializeField] private AudioClip inventoryFullSFX;
    [SerializeField] private AudioSource audioSource;

    [Header("Inventory Sounds")]
    [SerializeField] private AudioClip itemPickupSFX;
   
    // private MainMenu _mainMenu
    private TMP_Text _interactMessage;
    private PlayerInputs _inputs;
    private GameObject _activeMenu;

    public static UIManager Instance { get; private set; }
    

    public TMP_Text InteractMessage { get { return _interactMessage; } }
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

     private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        _inputs = GameManager.instance.Inputs;
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            SearchRefereeces();
        };
        SearchRefereeces();
    }
    private void Update()
    {

        if(_inputs.IsEscapeClicked)
        {
            ActiveMenu(_activeMenu);
        }
    }

    public void PopUpMessage(string message)
    {
        _interactMessage.text = message;
        _interactMessage.enabled = true;
    }

    public void PopUpMessageTimed(string message)
    {
        // Si ya había un fade en curso, lo cortamos
        if (_messageCoroutine != null)
        {
            StopCoroutine(_messageCoroutine);
        }

        _messageCoroutine = StartCoroutine(PopUpAndFade(message));
    }

    private IEnumerator PopUpAndFade(string message)
    {
        // Mostrar mensaje
        _interactMessage.text = message;
        _interactMessage.enabled = true;

        // Aseguramos alpha en 1 al inicio
        Color c = _interactMessage.color;
        c.a = 1f;
        _interactMessage.color = c;

        // Tiempo que se queda quieto el texto antes de empezar a desvanecerse
        yield return new WaitForSeconds(_popUpMessageTime);

        // Fade out
        float t = 0f;
        while (t < _fadeOutDuration)
        {
            t += Time.unscaledDeltaTime; // por si esta en pausa (Time.timeScale = 0)
            float normalized = 1f - (t / _fadeOutDuration);
            c.a = Mathf.Clamp01(normalized);
            _interactMessage.color = c;
            yield return null;
        }

        _interactMessage.enabled = false;
        _messageCoroutine = null;
    }
    
    public void ShowInventoryFullMessage()
    {
        PopUpMessageTimed("El inventario está lleno");
        PlayInventoryFullSFX();
    }

    public void HideMessage()
    {
        if (_messageCoroutine != null)
        {
            StopCoroutine(_messageCoroutine);
            _messageCoroutine = null;
        }

        // Opcional: dejar el alpha en 0 por las dudas
        Color c = _interactMessage.color;
        c.a = 0f;
        _interactMessage.color = c;

        _interactMessage.enabled = false;
    }


    private void SearchRefereeces()
    {
        _inGameMenu = FindFirstObjectByType<InGameMenu>(FindObjectsInactive.Include).gameObject;
        _controlMenu = FindFirstObjectByType<ControlMenu>(FindObjectsInactive.Include).gameObject;
        _HUD = GameObject.Find("HUD");
        _interactMessage = GameObject.Find("Interact_message").GetComponent<TMP_Text>();
        _activeMenu = InGameMenu;
    }

    public void ActiveMenu(GameObject activeMenu)
    {
       
        if(activeMenu == null) SearchRefereeces();
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

    public void PlayInventoryFullSFX()
    {
        if (audioSource != null && inventoryFullSFX != null)
        {
            audioSource.PlayOneShot(inventoryFullSFX);
        }
    }

    public void PlayItemPickupSFX()
    {
        if (audioSource != null && itemPickupSFX != null)
        {
            audioSource.PlayOneShot(itemPickupSFX);
        }
    }

    public void ShowPickupItemMessage(Item item)
    {
        string msg = $"+1 {item.ItemName}";
        PopUpMessageTimed(msg);
    }


}
