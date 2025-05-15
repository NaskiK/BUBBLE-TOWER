using UnityEngine;
using TMPro;

public class DisplayModeSettings : MonoBehaviour
{
    public TMP_Dropdown displayModeDropdown;

    void Start()
    {
        // Set dropdown to current mode
        displayModeDropdown.value = Screen.fullScreen ? 0 : 1;
        displayModeDropdown.onValueChanged.AddListener(SetDisplayMode);
    }

    public void SetDisplayMode(int option)
    {
        if (option == 0) // Fullscreen
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else if (option == 1) // Windowed
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
        }
    }
}
