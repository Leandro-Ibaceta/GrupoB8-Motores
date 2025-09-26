using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _ControlsMenu;

    private UIManager _uiManager;
    private void OnEnable()
    {
        _uiManager = GameManager.instance.UIManager;
        _ControlsMenu = _uiManager.ControlMenu;
    }


    public void OnResumeButtonCLick()
    {
        _uiManager.ActiveMenu(gameObject);
    }

    public void OnControlsButtonClick()
    {

        _uiManager.ActiveMenu(_ControlsMenu);
        gameObject.SetActive(false);

    }

    public void OnExitButtonClick()
    {
        // load main menu scene
        GameManager.instance.QuitGame();
        Debug.Log("Ponele que salio del juego, pero Application.Quit solo funciona un vez hecha la build");
    }



}
