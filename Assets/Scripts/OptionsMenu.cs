using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string exposedParameter;
    public Slider volumeSlider;

    private void Start()
    {

        float sliderValue;
        bool result = audioMixer.GetFloat(exposedParameter, out sliderValue);
        if (result)
        {
            volumeSlider.value = sliderValue;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(exposedParameter, volume);
    }
}