using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeUi : MonoBehaviour
{
    private Text myText;

    [SerializeField] private float myTime = 60f;

    private void Start()
    {
        myText = GetComponent<Text>();
        StartCoroutine(StartCoundown());
    }

    IEnumerator StartCoundown()
    {
        //float currentTime = myTime;

        for(int i = (int)myTime; i >= 0; i--)
        {
            myText.text = "Time: " + i;

            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
