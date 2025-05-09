using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{
    public AudioMixerGroup master;
    public AudioMixerGroup effects;
    public AudioMixerGroup music;
    public AudioSource villagerSpawnNoise;
    public AudioSource winSFX;
    public AudioSource backgroundMusic;

    void Awake()
    {
        villagerSpawnNoise.outputAudioMixerGroup = effects;
        winSFX.outputAudioMixerGroup = effects;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlaySpawnNoise(){
        villagerSpawnNoise.Play();
    }
    public void PlayWinNoise(){
        backgroundMusic.Stop();
        winSFX.Play();
    }
}
