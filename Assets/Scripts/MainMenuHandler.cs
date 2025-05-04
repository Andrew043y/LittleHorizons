using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuHandler : MonoBehaviour
{
    public AudioMixer masterMix;
    public Dropdown resolutionDropDown;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject UIHandler;
    Resolution[] resolutions;

    void Start(){
        resolutions=Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex=0;
        for(int i=0; i<resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex=i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value=currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
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
    public void setResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void openOptions(){
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void closeOptions(){
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void resume(){
        MainMenu.SetActive(false);
        UIHandler.GetComponent<UIHandler>().gamePaused=false;
    }
        
}
