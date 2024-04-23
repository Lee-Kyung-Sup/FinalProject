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
        int currentResolutionIndex = 0; // ���� ������ �ػ��� �ε���

        for (int i = 0; i < resolutions.Count; i++)
        {
            // �ػ� ���ڿ� ���� ex. 1920 x 1080
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // ���� ȭ���� �ػ󵵿� ��ġ Ȯ��
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

