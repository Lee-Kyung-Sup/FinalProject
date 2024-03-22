using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterTrigger : MonoBehaviour
{
    protected LayerMask pLayer;
    protected virtual void Awake()
    {
        pLayer = LayerMask.GetMask("Player", "PlayerInvincible");
    }
}
public class CharacterEnterTrigger : MonoBehaviour
{
    protected LayerMask characterLayer;
    protected virtual void Awake()
    {
        characterLayer = LayerMask.GetMask("Player","Monster");
    }
}