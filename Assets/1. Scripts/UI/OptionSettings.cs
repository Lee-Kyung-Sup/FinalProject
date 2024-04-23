using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private List<Resolution> resolutions;

    void Start()
    {
        resolutions = new List<Resolution>(Screen.resolutions);


        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0; // 현재 설정된 해상도의 인덱스

        for (int i = 0; i < resolutions.Count; i++)
        {
            // 해상도 문자열 생성 ex. 1920 x 1080
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // 현재 화면의 해상도와 일치 확인
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Debug.Log("Changing resolution to: " + resolution.width + "x" + resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

