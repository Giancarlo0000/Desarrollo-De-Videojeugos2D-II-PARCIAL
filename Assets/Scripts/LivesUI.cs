using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    private Text myText;
    public Player myPlayer;

    delegate void ChangeTextUI(); 
    ChangeTextUI myTextUI;

    private void Start()
    {
        myText = GetComponent<Text>();
        myText.text = "Lives: " + myPlayer.Lives;

        myTextUI += LivesCounter;

        if(myTextUI != null)
        {
            myTextUI();
        }
    }

    private void Update()
    {
        myTextUI();
        if (myPlayer.isLevelCompleted) //Cambia el delegado, por lo tanto el texto cambia
        {
            myTextUI -= LivesCounter;
            myTextUI += LevelCompleted;
        }
    }

    void LivesCounter()
    {
        myText.text = "Lives: " + myPlayer.Lives;
    }

    void LevelCompleted()
    {
        myText.text = "Nivel Completado";
    }
}
