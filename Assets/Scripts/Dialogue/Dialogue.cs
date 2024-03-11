using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] contexts;
}

public class DialogueEvent
{
    public Vector2 line;
    public Dialogue[] dia;
}
