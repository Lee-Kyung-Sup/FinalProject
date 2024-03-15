using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    [Tooltip("��� ġ�� ĳ���� �̸�")]
    public string name;

    [Tooltip("��� ����")]
    public string[] contexts;
}

public class DialogueEvent
{
    public Vector2 line;
    public Dialogue[] dia;
}
