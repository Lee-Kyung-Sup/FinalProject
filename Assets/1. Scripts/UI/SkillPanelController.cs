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

        Time.timeScale = 0f;

        skillPanel.SetActive(true);


        switch (actionType)
        {
            case Paction.DoubleJump:
                videoPlayer.clip = videoClips[0];
                skillDescriptionText.text = skillDescriptions[0];
                skillTitleText.text = skillTitles[0];
                break;
            case Paction.Dash:
                videoPlayer.clip = videoClips[1];
                skillDescriptionText.text = skillDescriptions[1];
                skillTitleText.text = skillTitles[1];
                break;
            case Paction.RangeAttack:
                videoPlayer.clip = videoClips[2];
                skillDescriptionText.text = skillDescriptions[2];
                skillTitleText.text = skillTitles[2];
                break;
            case Paction.ChargeShot:
                videoPlayer.clip = videoClips[3];
                skillDescriptionText.text = skillDescriptions[3];
                skillTitleText.text = skillTitles[3];
                break;
            case Paction.JumpAttack:
                videoPlayer.clip = videoClips[4];
                skillDescriptionText.text = skillDescriptions[4];
                skillTitleText.text = skillTitles[4];
                break;
            case Paction.Deflect:
                videoPlayer.clip = videoClips[5];
                skillDescriptionText.text = skillDescriptions[5];
                skillTitleText.text = skillTitles[5];
                break;
            case Paction.ComboAttack:
                videoPlayer.clip = videoClips[6];
                skillDescriptionText.text = skillDescriptions[6];
                skillTitleText.text = skillTitles[6];
                break;

            default:
                break;
        }

        videoPlayer.Play();
    }

    public void CloseSkillPanel()
    {
        Time.timeScale = 1f;

        skillPanel.SetActive(false);
    }
}

