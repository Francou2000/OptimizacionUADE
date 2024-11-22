using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonToExit : MonoBehaviour
{
    Button _myButton;

    void Start()
    {
        _myButton = GetComponent<Button>();
        _myButton.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
