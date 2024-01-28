using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Recipes", fileName = "Recipes", order = 0)]
public class Recipes : ScriptableObject
{
    public RecipeData[] m_recipes;

    public RecipeData GetRecipe(RecipeType _type)
    {
        foreach (RecipeData recipe in m_recipes)
        {
            if (recipe.m_type == _type)
                return recipe;
        }

        return m_recipes[0];
    }
    
    private void OnValidate()
    {
        foreach (RecipeData recipe in m_recipes)
        {
            recipe.UpdateName();
        }
    }
}

[Serializable]
public class RecipeData
{
    [HideInInspector]
    public string m_name;
    public RecipeType m_type;
    public Sprite m_sprite;
    public IngredientType[] m_ingredients;
    public bool m_generateRecipe;
    
    public void UpdateName()
    {
        m_name = m_type.ToString();
    }
}