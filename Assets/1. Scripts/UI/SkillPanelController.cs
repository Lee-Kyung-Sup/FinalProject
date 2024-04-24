using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SkillPanelController : MonoBehaviour
{
    public GameObject skillPanel;
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI skillTitleText;
    public TextMeshProUGUI skillDescriptionText;

    public List<VideoClip> videoClips = new List<VideoClip>();
    public string[] skillTitles;
    public string[] skillDescriptions;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseSkillPanel();
        }
    }

    public void ShowSkillPopup(Paction actionType)
    {
        int index = (int)actionType; // Paction�� �ε����� ��ȯ
        if (index >= 0 && index < videoClips.Count)
        {
            Time.timeScale = 0f;
            skillPanel.SetActive(true);

            videoPlayer.clip = videoClips[index];
            skillDescriptionText.text = skillDescriptions[index];
            skillTitleText.text = skillTitles[index];

            videoPlayer.Play();
        }
    }

    public void ShowSkillPopup(int index) // ���� ������ �ٸ�, �޼��� �����ε� 
    {
        Time.timeScale = 0f;

        skillPanel.SetActive(true);

        if (index >= 0 && index < videoClips.Count)
        {
            Time.timeScale = 0f; // ���� �Ͻ�����
            skillPanel.SetActive(true); // ��ų �г� Ȱ��ȭ

            videoPlayer.clip = videoClips[index];
            skillDescriptionText.text = skillDescriptions[index];
            skillTitleText.text = skillTitles[index];

            videoPlayer.Play(); // ���� ���
        }
    }

    public void CloseSkillPanel()
    {
        Time.timeScale = 1f;

        skillPanel.SetActive(false);
    }
}

