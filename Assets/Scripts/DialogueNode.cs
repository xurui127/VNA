using System;
using System.Collections.Generic;

[Serializable]
public class DialogueNode
{
    public int id;
    public int scene;
    public string dialogueText;
    public int speakerID;
    public List<DialogueOption> options;
}
