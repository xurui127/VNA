using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]
public class DialogueDate
{
    public List<DialogueNode> nodes;
}
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextPanle TextPanle;
    [SerializeField] private OptionPanel optionPanel;

    private Dictionary<int, DialogueNode> dialogueTree;
    private Dictionary<int, CharacterInstance> characters;
    private Dictionary<int, float> relationDic;

    private int currentDialogue = 1;


    private float lastClickTime = 0;
    private float lastCooldown = 0.5f;
    private void Start()
    {
        LoadDialogue();
        LoadRelation();
        ShowDialogue(currentDialogue);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ClickCooldown())
        {
            lastClickTime = Time.time;
            if (dialogueTree[currentDialogue].options.Count == 0 && currentDialogue > 0 )
            {
                ShowDialogue(currentDialogue);
            }
        }
    }
 
    public void ShowDialogue(int dialogueID)
    {
        Debug.Log($"Current Dialogue ID: {dialogueID}");

        if (dialogueTree.ContainsKey(dialogueID))
        {
            var dialogue = dialogueTree[dialogueID];
            TextPanle.text.text = dialogue.dialogueText;
            optionPanel.ClearOptions();
 
            if (dialogue.options != null && dialogue.options.Count > 0)
            {
                foreach (var option in dialogue.options)
                {
                    int nextID = option.nextDialogueID;
                    Debug.Log($"Creating button: {option.text}, nextID = {nextID}");
                    optionPanel.InitOptionButton(option.text, () => ShowDialogue(nextID));
                }
            }
            else
            {
                currentDialogue = dialogue.nextDialogueID;
                Debug.Log($"Next Dialogue ID set to: {currentDialogue}");
            }


        }
        else
        {
            Debug.LogWarning($"DialogueID {dialogueID} not found.");
        }
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

    private bool ClickCooldown()
    {

        return Time.time - lastClickTime > lastCooldown;
    }


}
