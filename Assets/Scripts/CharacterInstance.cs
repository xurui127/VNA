using System;

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

    public void SetExpression(float expression)
    {

    }
    public void ChangeFavorability(float favorability)
    {

    }
    public void ReactToPlayer(int choice)
    {

    }
}
