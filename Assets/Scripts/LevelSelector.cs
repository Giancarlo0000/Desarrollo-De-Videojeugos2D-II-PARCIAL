using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button Level1;
    [SerializeField] private Button Level2;
    [SerializeField] private Button Back;

    void Start()
    {
        if (Level1 != null) Level1.onClick.AddListener(ChangeScenePlay);
        if (Level2 != null) Level2.onClick.AddListener(ChangeSceneCredits);
        if (Back != null) Back.onClick.AddListener(ButtonBack);
    }

    void ChangeScenePlay()
    {
        SceneManager.LoadScene("Level 1");
    }

    void ChangeSceneCredits()
    {
        SceneManager.LoadScene("Level 2");
    }

    void ButtonBack()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
