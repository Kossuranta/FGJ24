using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create IngredientSprites", fileName = "IngredientSprites", order = 1)]
public class IngredientSprites : ScriptableObject
{
    public IngredientSprite[] m_ingredientSprites;

    public Sprite GetSprite(IngredientType _type)
    {
        foreach (IngredientSprite ingredientSprite in m_ingredientSprites)
        {
            if (ingredientSprite.m_type == _type)
                return ingredientSprite.m_sprite;
        }

        return m_ingredientSprites[0].m_sprite;
    }

    private void OnValidate()
    {
        foreach (IngredientSprite ingredientSprite in m_ingredientSprites)
        {
            ingredientSprite.UpdateName();
        }
    }
}

[Serializable]
public class IngredientSprite
{
    [HideInInspector]
    public string m_name;
    public IngredientType m_type;
    public Sprite m_sprite;

    public void UpdateName()
    {
        m_name = m_type.ToString();
    }
}