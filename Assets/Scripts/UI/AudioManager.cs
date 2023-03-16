using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    private Slider bgmSlider, sfxSlider;
    private AudioSource bgmSource;
    private List<AudioSource> sfxSources;

    private void Awake()
    {
        List<AudioSource> audioSources = GameObject.FindObjectsOfType<AudioSource>().ToList(); //todo: this doesn't find all audio sources?
        bgmSource = Camera.main.GetComponent<AudioSource>();
        audioSources.Remove(bgmSource);
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < audioSources.Count; i++)
        {
            sfxSources.Add(audioSources[i]);
        }

        bgmSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        LoadVolumeSettings();

        bgmSlider.onValueChanged.AddListener(SaveBGMValue);
        sfxSlider.onValueChanged.AddListener(SaveSFXValue);
    }

    public void SetBGMVolume(float value)
    {
        bgmSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        if (sfxSources != null)
        {
            foreach (AudioSource source in sfxSources)
            {
                source.volume = value;
            }
        }
    }

    public void SaveBGMValue(float value)
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
    }
    public void SaveSFXValue(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void LoadVolumeSettings()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume");
        if (sfxSources != null)
        {
            foreach (AudioSource source in sfxSources)
            {
                source.volume = PlayerPrefs.GetFloat("SFXVolume");
            }
        }
    }

    void OnDestroy() //to prevent memory leaks
    {
        bgmSlider.onValueChanged.RemoveListener(SaveBGMValue);
        sfxSlider.onValueChanged.RemoveListener(SaveSFXValue);
    }
}
