using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackTypes
{
    MeleeAttack,
    RangeAttack,
    JumpAttack,
    ComboAttack1,
    ComboAttack2,
    ChargeShot
}



public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}



public class SeasonManager : MonoBehaviour
{
    public GameObject tree;

    private Season currentSeason;

    private Animator treeAnimator;
    public AnimationClip springAnimation;
    public AnimationClip summerAnimation;
    public AnimationClip autumnAnimation;
    public AnimationClip winterAnimation;

  
    void Start()
    {
        currentSeason = Season.Spring;  // ���� ���� ��  (���� �ӽ�)
        treeAnimator = tree.GetComponent<Animator>();
        UpdateTreeAnimation();
    }

    void �����𼱰���¥�ٲ��ִ¸޼���()  
    {
        // 28�� �ʰ�? ���� �ٱ�
            ChangeSeason();
    }

    void ChangeSeason()
    {
        if (currentSeason == Season.Winter)
        {
            currentSeason = Season.Spring;
        }
        else
        {
            currentSeason += 1;
        }
        UpdateTreeAnimation();
    }

    void UpdateTreeAnimation()
    {
        switch (currentSeason)
        {
            case Season.Spring:
                treeAnimator.Play(springAnimation.name);
                break;
            case Season.Summer:
                treeAnimator.Play(summerAnimation.name);
                break;
            case Season.Autumn:
                treeAnimator.Play(autumnAnimation.name);
                break;
            case Season.Winter:
                treeAnimator.Play(winterAnimation.name);
                break;
        }
    }
}
