using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;

    public void ActivatePanel()
    {
        bool isActive = pausePanel.activeSelf;
        pausePanel.SetActive(!isActive);
    }
}
