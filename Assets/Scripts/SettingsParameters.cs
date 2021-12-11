using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsParameters : MonoBehaviour
{
    [SerializeField] public int Music;
    [SerializeField] public int Sound;
    [SerializeField] public bool Mute = false;
    [SerializeField] public AudioMixer master;

    public void Awake()
    {
        Music = 100;
        Sound = 100;
    }

    public void MuteAudio()
    {
        Mute = !Mute;
        if (Mute)
        {
            master.SetFloat("MasterVolume", Mathf.Log10(0.001f) * 20);
        }
        if (!Mute)
        {
            master.SetFloat("MasterVolume", Mathf.Log10(1f) * 20);
        }
    }

    public void SetMusicVolume(float value)
    {
        Music = Mathf.RoundToInt(value * 100);
        master.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetSoundVolume(float value)
    {
        Sound = Mathf.RoundToInt(value * 100);
        master.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }

}