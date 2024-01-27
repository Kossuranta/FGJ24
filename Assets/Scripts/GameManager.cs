using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MainMenu m_mainMenu;
    public DeskManager m_deskManager;
    public CustomerManager m_customerManager;

    public Recipes m_recipes;
    public IngredientSprites m_ingredientSprites;

    [NonSerialized]
    public readonly List<IngredientType> m_selectedIngredients = new(4);

    [NonSerialized]
    public Customer m_customer;

    public int m_day = 0;

    public const int SCREEN_HEIGHT = 1080;
    public const int SCREEN_WIDTH = 1920;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("ERROR! Multiple GameManagers are not allowed!");
            DestroyImmediate(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        m_mainMenu.gameObject.SetActive(true);
        m_deskManager.Initialize();
        m_customerManager.Initialize();
    }

    public void StartGame()
    {
        m_mainMenu.gameObject.SetActive(false);
        StartDay(0);
    }

    public void StartDay(int _day)
    {
        m_deskManager.StartDay(_day);
        m_customerManager.NextCustomer();
    }

    public void AddIngredient(IngredientType _ingredient)
    {
        if (m_selectedIngredients.Count >= 4)
            return;
        
        if (m_customer == null)
            return;
        
        m_deskManager.SetIngredient(m_selectedIngredients.Count, _ingredient);
        m_selectedIngredients.Add(_ingredient);
        Debug.Log($"Added ingredient {_ingredient}");
        if (m_selectedIngredients.Count == 4)
            Invoke(nameof(BakeBun), 1);
    }

    private void BakeBun()
    {
        RecipeData recipe = m_recipes.GetRecipe(m_customer.m_order);
        int correctIngredients = 0;
        foreach (IngredientType ingredient in recipe.m_ingredients)
        {
            if (m_selectedIngredients.Contains(ingredient))
                correctIngredients++;
        }
        m_selectedIngredients.Clear();
        m_deskManager.ClearIngredients();
        
        Debug.Log($"Bake completed, success: {correctIngredients}/4");
        if (correctIngredients == 4)
            BakeSuccess();
        else
            BakeFail();
    }

    public void CustomerServed()
    {
        m_customerManager.CustomerLeave();
    }

    public void CustomerLeft()
    {
        m_customerManager.NextCustomer();
    }
    
    private void BakeSuccess()
    {
        m_customer.MakeHappy();
        CustomerServed();
    }

    private void BakeFail()
    {
        m_customer.MakeSad();
        CustomerServed();
    }
}
