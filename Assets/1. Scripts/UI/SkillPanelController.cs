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
        int index = (int)actionType; // Paction을 인덱스로 변환
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

    public void ShowSkillPopup(int index) // 위와 변수만 다름, 메서드 오버로딩 
    {
        Time.timeScale = 0f;

        skillPanel.SetActive(true);

        if (index >= 0 && index < videoClips.Count)
        {
            Time.timeScale = 0f; // 게임 일시정지
            skillPanel.SetActive(true); // 스킬 패널 활성화

            videoPlayer.clip = videoClips[index];
            skillDescriptionText.text = skillDescriptions[index];
            skillTitleText.text = skillTitles[index];

            videoPlayer.Play(); // 비디오 재생
        }
    }

    public void CloseSkillPanel()
    {
        Time.timeScale = 1f;

        skillPanel.SetActive(false);
    }
}

