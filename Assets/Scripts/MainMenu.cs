using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button Play;
    [SerializeField] private Button Credits;
    [SerializeField] private Button Back;

    void Start()
    {
        if (Play != null) Play.onClick.AddListener(ChangeScenePlay);
        if (Credits != null) Credits.onClick.AddListener(ChangeSceneCredits);
        if (Back != null) Back.onClick.AddListener(ButtonBack);
    }

    void ChangeScenePlay()
    {
        SceneManager.LoadScene("Levels");
    }

    void ChangeSceneCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    void ButtonBack()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
