using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuHandler : MonoBehaviour
{
    public void startGame(){
        SceneManager.LoadScene("GameScene");
    }
    public void quitGame(){
        Application.Quit();
    }
}
