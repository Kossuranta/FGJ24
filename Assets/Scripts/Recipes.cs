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
}

[Serializable]
public class RecipeData
{
    public RecipeType m_type;
    public IngredientType[] m_ingredients;
}