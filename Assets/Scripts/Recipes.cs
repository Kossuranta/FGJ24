using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipes", fileName = "Recipes", order = 0)]
public class Recipes : ScriptableObject
{
    public RecipeData[] m_recipes;
}

[Serializable]
public class RecipeData
{
    public RecipeType m_type;
    public IngredientType[] m_ingredients;
}