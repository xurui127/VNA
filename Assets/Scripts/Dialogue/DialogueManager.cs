using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class DialogueDate
{
    public List<DialogueNode> nodes;
}
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private TextPanle TextPanle;
    [SerializeField] private OptionPanel optionPanel;

    [SerializeField] private Image left;
    [SerializeField] private Image right;

    private Dictionary<int, DialogueNode> dialogueTree;
    private Dictionary<int, CharacterInstance> charactersDic;
    private Dictionary<int, float> relationDic;


    private int currentDialogue = 1;


    private float lastClickTime = 0;
    private float lastCooldown = 0.5f;

    [Header("Test")]
    [SerializeField] Color zero;
    [SerializeField] Color one;
    [SerializeField] Color two;
    [SerializeField] Color three;
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

            ShowDialogue(currentDialogue);

        }
    }

    public void ShowDialogue(int dialogueID)
    {
        Debug.Log($"Current Dialogue ID: {dialogueID}");
        ResetSlot();


        if (currentDialogue == dialogueID && optionPanel.HasOption())
        {
            Debug.Log($"Skipping duplicate ShowDialogue({dialogueID}) call because options already exist.");
            return;
        }
        if (dialogueTree.ContainsKey(dialogueID))
        {
            var dialogue = dialogueTree[dialogueID];
            TextPanle.text.text = dialogue.dialogueText;

            var id = dialogue.speakerID;
            IsLeftSlot(dialogue.isLeft, id);

            var scene = dialogue.scene;
            optionPanel.ClearOptions();

            if (dialogue.options != null && dialogue.options.Count > 0)
            {
                foreach (var option in dialogue.options)
                {
                    int nextID = option.nextDialogueID;

                    var speakerID = dialogueTree[nextID].speakerID;
                    var favorability = option.favorabilityChange;

                    Debug.Log($"Creating button: {option.text}, nextID = {nextID}");
                    optionPanel.InitOptionButton(option.text,
                                                 () => ShowDialogue(nextID),
                                                 () => ChangeFavorability(speakerID, favorability));
                }
            }
            else
            {
                currentDialogue = dialogue.nextDialogueID;
                Debug.Log($"Next Dialogue ID set to: {currentDialogue}");
            }
            SetSceneColor(scene);
        }
        else
        {
            Debug.LogWarning($"DialogueID {dialogueID} not found.");
        }

    }
    private void ChangeFavorability(int id, float favorability)
    {
        relationDic[id] += favorability;

        foreach (var key in relationDic.Keys)
        {
            Debug.Log($"Relations : {key}: {relationDic[key]}");
        }
    }

    private void IsLeftSlot(bool isLeft, int id)
    {

        if (isLeft)
        {
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(false);
            left.color = charactersDic[id].SetExpression(relationDic[id]);
        }
        else
        {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(true);
            right.color = charactersDic[id].SetExpression(relationDic[id]);
        }

    }

    private void ResetSlot()
    {
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
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
        charactersDic = new();
        if (characters.Length != 0)
        {
            foreach (var character in characters)
            {
                var newCharacter = new CharacterInstance(character);
                relationDic[newCharacter.characterData.id] = newCharacter.favorability;
                charactersDic.Add(newCharacter.characterData.id, newCharacter);

            }

            Debug.Log($"Character loaded successfully. Total Characrers:{characters.Length}");
        }
        else
        {
            Debug.LogWarning("NOT FOUND charcters files");
        }

    }

    //TODO: Change after 
    private bool ClickCooldown()
    {

        return Time.time - lastClickTime > lastCooldown;
    }

    //TODO: Change after
    private void SetSceneColor(int index)
    {
        if (index == 0)
        {
            camera.backgroundColor = zero;
        }
        else if (index == 1)
        {
            camera.backgroundColor = one;
        }
        else if (index == 2)
        {
            camera.backgroundColor = two;
        }
        else
        {
            camera.backgroundColor = three;
        }
    }
}
