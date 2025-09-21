using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _ControlsMenu;


    private void OnEnable()
    {
        _ControlsMenu = UIManager.Instance.ControlMenu;
    }


    public void OnResumeButtonCLick()
    {
        UIManager.Instance.ActiveMenu(gameObject);
    }

    public void OnControlsButtonClick()
    {

        UIManager.Instance.ActiveMenu(_ControlsMenu);
        gameObject.SetActive(false);

    }

    public void OnExitButtonClick()
    {
        GameManager.instance.QuitGame();
        Debug.Log("Ponele que salio del juego, pero Application.Quit solo funciona un vez hecha la build");
    }



}
