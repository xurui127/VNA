using UnityEngine.UI;

using System;
using System.Collections.Generic;

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

[Serializable]
public class Character
{
    public int id;
    public Role role;
    public string name;
    public CharacterState state;
    public Dictionary<CharacterState, Image> expression;
    public Dictionary<Character, float> relationShip;

    public void SetExpression(float expression)
    {

    }
    public void ChangeFavorability(float  favorability)
    {

    }
    public void ReactToPlayer(int choice)
    {

    }
}
