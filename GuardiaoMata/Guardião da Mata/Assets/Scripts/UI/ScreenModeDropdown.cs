using UnityEngine;
using TMPro; // Use isso se for Dropdown TMP

public class ScreenModeDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Arraste o Dropdown aqui via Inspector

    void Start()
    {
        dropdown.onValueChanged.AddListener(SetScreenMode);
        dropdown.value = Screen.fullScreen ? 1 : 0; // Sincroniza ao iniciar
    }

    void SetScreenMode(int index)
    {
        if (index == 0)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if (index == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
    }
}