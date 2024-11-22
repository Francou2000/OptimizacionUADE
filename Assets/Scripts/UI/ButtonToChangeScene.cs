using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonToChangeScene : MonoBehaviour
{
    [SerializeField] string newScene;
    Button _myButton;
    
    void Start()
    {
        _myButton = GetComponent<Button>();
        _myButton.onClick.AddListener(ChangeScene);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(newScene);
    }
}
