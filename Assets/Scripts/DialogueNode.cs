using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    public int id;
    public int scene;
    public string dialogueText;
    public List<DialogueOption> options;
    public Character speaker;
}
