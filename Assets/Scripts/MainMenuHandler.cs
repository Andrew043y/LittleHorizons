using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainMenuHandler : MonoBehaviour
{
    public AudioMixer masterMix;
    public void startGame(){
        SceneManager.LoadScene("GameScene");
    }
    public void quitGame(){
        Application.Quit();
    }

    
    public void setMasterVolume(float val){
        masterMix.SetFloat("MasterVolume", val);
    }
    public void setMusicVolume(float val){
        masterMix.SetFloat("MusicVolume", val);
    }
    public void setEffectsVolume(float val){
        masterMix.SetFloat("EffectsVolume", val);
    }
    public void setFullScreen(bool isFullScreen){
        Screen.fullScreen=isFullScreen;
    }
}
