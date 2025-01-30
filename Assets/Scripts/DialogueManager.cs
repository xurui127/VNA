using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;


[Serializable]
public class DialogueDate
{
    public List<DialogueNode> nodes;
}
public class DialogueManager : MonoBehaviour
{
    private Dictionary<int, DialogueNode> dialogueTree;
    private Dictionary<int, CharacterInstance> characters;
    private Dictionary<int, float> relationDic;

    private void Start()
    {
        LoadDialogue();
        LoadRelation();
    }
    public void ChooseOption(DialogueOption option)
    {

    }
    public void ShowDialogue(int dialogueID)
    {

    }
    private void LoadDialogue()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Dialogue.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DialogueDate dialogueData = JsonUtility.FromJson<DialogueDate>(json);

            dialogueTree = new Dictionary<int, DialogueNode>();
            foreach (var node in dialogueData.nodes)
            {
                dialogueTree[node.ID] = node;
            }
            Debug.Log($"Dialogue loaded successfully. Total Nodes:{dialogueTree.Count}");
        }
        else
        {
            Debug.LogWarning("NOT FOUND Dialogue Data");
        }
    }

    //TODO: Change to JSON load after
    private void LoadRelation()
    {
        Character_SO[] characters = Resources.LoadAll<Character_SO>("Characters");
        relationDic = new();
        if (characters.Length != 0)
        {
            foreach (var character in characters)
            {
                var newCharacter = new CharacterInstance(character);
                relationDic[newCharacter.characterData.id] = newCharacter.favorability;
            }

            Debug.Log($"Character loaded successfully. Total Characrers:{characters.Length}");
        }
        else
        {
            Debug.LogWarning("NOT FOUND charcters files");
        }
       
    }


}
