using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Role
{
    Protagonist = 0,
    Supporting = 1,
    Villain = 2,
}
public enum CharacterState
{
    Normal = 0,
    Happy = 1,
    Sad = 2,
    Angry = 3,
}

[CreateAssetMenu(fileName = "NewCharacter",menuName = "Dialogue System/Character")]
public class Character_SO : ScriptableObject
{
    public int id;
    public Role role;
    public string characterName;

    public Sprite noramalExpression;
    public Sprite happyExpression;
    public Sprite sadExpression;
    public Sprite angryExpression;

   
}
