using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public GameObject pauseMenu; 
     public Slider volumeSlider;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void ContinueGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f; // retoma o jogo se ele estiver pausado
        }
    }

    public void QuitGame()
    {
        Debug.Log("Fechando o jogo...");
        Application.Quit();
    }

    public void MuteVolume()
    {
        audioMixer.SetFloat("volume", -80f); // mutar
        if (volumeSlider != null)
            volumeSlider.value = 0f; // atualiza slider
    }

    public void MaxVolume()
    {
        audioMixer.SetFloat("volume", 0f); // volume máximo
        if (volumeSlider != null)
            volumeSlider.value = 1f; // atualiza slider
    }
}
