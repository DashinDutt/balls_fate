
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text Score;
    public Text LoadedScore;
    public Text Res;
    bool gameHasEnded = false;

    public void EndGame()
    {
        if (gameHasEnded==false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Score.text = LoadedScore.text;
            Res.text = "Restarting Game in a few seconds... Press Esc to quit";
            Invoke("Restart", 10f);
        }
        

    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
