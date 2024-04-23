using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GraphicOptionSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown refreshRateDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;

    private List<Resolution> resolutions;
    private List<int> refreshRates = new List<int>();

    void Start()
    {
        // �ػ� ���� �ʱ�ȭ
        InitializeResolutionSettings();
        InitializeRefreshRateSettings(); // �ֻ��� ���� �ʱ�ȭ

        // â ��� ���� �ʱ�ȭ
        fullscreenToggle.isOn = Screen.fullScreen;

        // ���� ����ȭ ���� �ʱ�ȭ
        vSyncToggle.isOn = QualitySettings.vSyncCount > 0;
    }

    private void InitializeResolutionSettings()
    {
        resolutions = new List<Resolution>(Screen.resolutions);
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
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

    private void InitializeRefreshRateSettings()
    {
        refreshRateDropdown.ClearOptions();

        // ���� ���� ������ ��� �ػ󵵿��� �ֻ��� ��� ����
        foreach (Resolution res in Screen.resolutions)
        {
            if (!refreshRates.Contains(res.refreshRate))
            {
                refreshRates.Add(res.refreshRate);
            }
        }

        refreshRateDropdown.AddOptions(refreshRates.ConvertAll<string>(rate => rate.ToString() + " Hz"));

        refreshRateDropdown.value = refreshRates.IndexOf(Screen.currentResolution.refreshRate);
        refreshRateDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetVSync(bool isVSync)
    {
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
    }

    public void SetRefreshRate(int refreshRateIndex)
    {
        int selectedRefreshRate = refreshRates[refreshRateIndex];
        Resolution currentResolution = Screen.currentResolution;
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen, selectedRefreshRate);
    }

}

