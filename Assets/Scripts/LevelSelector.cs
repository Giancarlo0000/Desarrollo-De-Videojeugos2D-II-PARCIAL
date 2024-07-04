using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button Level1;
    [SerializeField] private Button Level2;
    [SerializeField] private Button Level3;
    [SerializeField] private Button Back;

    void Start()
    {
        if (Level1 != null) Level1.onClick.AddListener(ChangeSceneLevel1);
        if (Level2 != null) Level2.onClick.AddListener(ChangeSceneLevel2);
        if (Level3 != null) Level3.onClick.AddListener(ChangeSceneLevel3);
        if (Back != null) Back.onClick.AddListener(ButtonBack);
    }

    void ChangeSceneLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    void ChangeSceneLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    void ChangeSceneLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    void ButtonBack()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
