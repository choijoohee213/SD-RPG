using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "RPG/Quest")]
public class Quest : ScriptableObject
{
    public string title;

    [TextArea(2, 6)]
    public string content;

    public Objectives[] objectives;
    public bool isCompleted = false;
}


[Serializable]
public class Objectives 
{
    public Item item;
    public int itemCount; 
}
