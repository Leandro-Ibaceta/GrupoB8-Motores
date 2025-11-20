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
   
    private TMP_Text _interactMessage;
    private PlayerInputs _inputs;
    private GameObject _activeMenu;

    public static UIManager Instance { get; private set; }
    
    public TMP_Text InteractMessage { get { return _interactMessage; } }

    public GameObject ControlMenu
    {
        get 
        { 
            if(_controlMenu == null)
            {
                var cm = FindFirstObjectByType<ControlMenu>(FindObjectsInactive.Include);
                if (cm != null) _controlMenu = cm.gameObject;
            }
            return _controlMenu; 
        }
    }

    public GameObject InGameMenu
    {
        get
        {
            if (_inGameMenu == null)
            {
                var igm = FindFirstObjectByType<InGameMenu>(FindObjectsInactive.Include);
                if (igm != null) _inGameMenu = igm.gameObject;
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

        // Cuando cambia de escena, solo buscamos refs si es la GameScene
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Si ya estamos en la GameScene al arrancar, buscamos refs
        if (SceneManager.GetActiveScene().name == GameManager.instance.GameSceneName)
        {
            SearchRefereeces();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GameManager.instance.GameSceneName)
        {
            SearchRefereeces();
        }
    }

    private void Update()
    {
        if (_inputs != null && _inputs.IsEscapeClicked)
        {
            ActiveMenu(_activeMenu);
        }
    }

    public void PopUpMessage(string message)
    {
        if (_interactMessage == null)
        {
            Debug.LogWarning("UIManager: InteractMessage es null, no puedo mostrar mensaje: " + message);
            return;
        }

        _interactMessage.text = message;
        _interactMessage.enabled = true;
    }

    public void PopUpMessageTimed(string message)
    {
        if (_interactMessage == null)
        {
            Debug.LogWarning("UIManager: InteractMessage es null, no puedo mostrar mensaje: " + message);
            return;
        }

        if (_messageCoroutine != null)
        {
            StopCoroutine(_messageCoroutine);
        }

        _messageCoroutine = StartCoroutine(PopUpAndFade(message));
    }

    private IEnumerator PopUpAndFade(string message)
    {
        _interactMessage.text = message;
        _interactMessage.enabled = true;

        Color c = _interactMessage.color;
        c.a = 1f;
        _interactMessage.color = c;

        yield return new WaitForSeconds(_popUpMessageTime);

        float t = 0f;
        while (t < _fadeOutDuration)
        {
            t += Time.unscaledDeltaTime;
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

        if (_interactMessage != null)
        {
            Color c = _interactMessage.color;
            c.a = 0f;
            _interactMessage.color = c;
            _interactMessage.enabled = false;
        }
    }

    private void SearchRefereeces()
    {
        // InGameMenu
        var inGameMenu = FindFirstObjectByType<InGameMenu>(FindObjectsInactive.Include);
        if (inGameMenu != null)
        {
            _inGameMenu = inGameMenu.gameObject;
        }
        else
        {
            _inGameMenu = null;
            Debug.LogWarning("UIManager: no encontré InGameMenu en la escena " + SceneManager.GetActiveScene().name);
        }

        // ControlMenu
        var controlMenu = FindFirstObjectByType<ControlMenu>(FindObjectsInactive.Include);
        if (controlMenu != null)
        {
            _controlMenu = controlMenu.gameObject;
        }
        else
        {
            _controlMenu = null;
            Debug.LogWarning("UIManager: no encontré ControlMenu en la escena " + SceneManager.GetActiveScene().name);
        }

        // HUD
        _HUD = GameObject.Find("HUD");
        if (_HUD == null)
        {
            Debug.LogWarning("UIManager: no encontré HUD en la escena " + SceneManager.GetActiveScene().name);
        }

        // Interact_message
        var interactGO = GameObject.Find("Interact_message");
        if (interactGO != null)
        {
            _interactMessage = interactGO.GetComponent<TMP_Text>();
        }
        else
        {
            _interactMessage = null;
            Debug.LogWarning("UIManager: no encontré Interact_message en la escena " + SceneManager.GetActiveScene().name);
        }

        _activeMenu = _inGameMenu;
    }

    public void ActiveMenu(GameObject activeMenu)
    {
        // Si en esta escena no hay menú, no hacemos nada
        if (_activeMenu == null && activeMenu == null)
        {
            return;
        }

        if (activeMenu == null)
        {
            SearchRefereeces();
        }
        else
        {
            _activeMenu = activeMenu;
        }

        if (_activeMenu == null) return;

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