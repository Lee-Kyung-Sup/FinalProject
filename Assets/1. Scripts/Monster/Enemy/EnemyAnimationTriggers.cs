using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    EnemyIceGolem enemy => GetComponentInParent<EnemyIceGolem>();
    void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

}