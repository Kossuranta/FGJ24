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
        for (int i = 0; i < 10; i++)
        {
            Recipe r = Instantiate(m_recipePrefab, transform);
            m_recipes.Add(r);
            r.Initialize(this, m_canvas);
            r.m_rect.anchoredPosition = new Vector2(400 + (i*20), 200 - (i*20));
        }
    }

    public void OnRecipeDragStart(Recipe _recipe)
    {
        _recipe.transform.SetSiblingIndex(m_recipes.Count);
    }
}