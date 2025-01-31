using System;
using UnityEngine;

[Serializable]
public class CharacterInstance
{
    public Character_SO characterData;
    public float favorability;
    public CharacterState characterState;

    public CharacterInstance(Character_SO data)
    {
        this.characterData = data;
        favorability = 0;
        this.characterState = CharacterState.Normal;
    }

    public Color SetExpression(float favobility)
    {
        if (favobility == 0)
        {
            return characterData.normal;
        }
        else if (favobility >= 10)
        {
            return characterData.happy;
        }
        else if (favobility < 0)
        {
            return characterData.sad;
        }

        return characterData.normal;
    }

}
