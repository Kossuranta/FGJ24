using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Vector2 m_minPos;
    public Vector2 m_maxPos;
    public Canvas m_canvas;
    public Recipe m_recipePrefab;
    public Vector2 m_recipeSpawnPos;
    public float m_recipeOffset;
    
    private readonly List<Recipe> m_recipes = new();

    public void Initialize()
    {
        
    }

    public void GenerateRecipes(int _day)
    {
        RecipeData[] recipes = GameManager.Instance.m_recipes.m_recipes;
        for (int i = 0; i < recipes.Length; i++)
        {
            Recipe r = Instantiate(m_recipePrefab, transform);
            m_recipes.Add(r);
            r.Initialize(this, m_canvas, recipes[i]);
            Vector2 spawnPos = m_recipeSpawnPos;
            float offset = m_recipeOffset * i;
            spawnPos.x += offset;
            spawnPos.y -= offset;
            SetRecipePosition(r.m_rect, spawnPos);
        }
    }

    public void OnRecipeDragStart(Recipe _recipe)
    {
        _recipe.transform.SetSiblingIndex(m_recipes.Count);
    }

    public void SetRecipePosition(RectTransform _recipe, Vector2 _targetPos)
    {
        _targetPos.x = Mathf.Clamp(_targetPos.x, m_minPos.x, m_maxPos.x);
        _targetPos.y = Mathf.Clamp(_targetPos.y, m_minPos.y, m_maxPos.y);
        _recipe.anchoredPosition = _targetPos;
    }
}