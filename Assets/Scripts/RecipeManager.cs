using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Canvas m_canvas;
    public Recipe m_recipePrefab;
    private readonly List<Recipe> m_recipes = new();

    public void Initialize()
    {
        GenerateRecipes();
    }

    private void GenerateRecipes()
    {
        Recipe r = Instantiate(m_recipePrefab, transform);
        m_recipes.Add(r);
        foreach (Recipe recipe in m_recipes)
        {
            recipe.Initialize(m_canvas);
        }
    }
}