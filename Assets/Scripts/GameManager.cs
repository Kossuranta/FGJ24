using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MainMenu m_mainMenu;
    public DeskManager m_deskManager;
    public CustomerManager m_customerManager;
    public DialogBox m_dialogBox;

    public Recipes m_recipes;
    public IngredientSprites m_ingredientSprites;

    public AudioSource m_customerAudio;
    public AudioSource m_backgroundAudio;
    public AudioSource m_truckAudio;

    [NonSerialized]
    public readonly List<IngredientType> m_selectedIngredients = new(4);

    [NonSerialized]
    public Customer m_currentCustomer;

    public Customer Boss => m_customerManager.m_boss;
    
    [NonSerialized]
    public RecipeType m_currentOrder;

    [NonSerialized]
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
        
        m_dialogBox.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        m_mainMenu.gameObject.SetActive(false);
        StartDay(0);
    }

    public void StartDay(int _day)
    {
        m_currentOrder = RecipeType.None;
        m_deskManager.StartDay(_day);
        m_customerManager.NextCustomer();
    }

    public void OnButtonYes()
    {
        if (m_currentOrder == RecipeType.None)
        {
            MakeOrder();
        }
        else
        {
            //Order resource
        }
    }

    public void OnButtonNo()
    {
        m_dialogBox.m_nextDialog.gameObject.SetActive(false);
        m_customerManager.ShowBoss();
    }

    public void ShowBossDialog(Customer _boss)
    {
        m_dialogBox.ShowOverrideDialog(_boss.m_dialog);
    }

    public void CustomerReady(Customer _customer)
    {
        m_currentCustomer = _customer;
        m_dialogBox.ShowDialog(_customer.m_dialog);
    }

    public void MakeOrder()
    {
        m_currentOrder = m_currentCustomer.m_order;
    }

    public void AddIngredient(IngredientType _ingredient)
    {
        if (m_selectedIngredients.Count >= 4)
            return;
        
        if (m_currentOrder == RecipeType.None)
            return;
        
        m_deskManager.SetIngredient(m_selectedIngredients.Count, _ingredient);
        m_selectedIngredients.Add(_ingredient);
        Debug.Log($"Added ingredient {_ingredient}");
        if (m_selectedIngredients.Count == 4)
        {
            Invoke(nameof(BakeBun), 1);
        }
    }

    private void BakeBun()
    {
        RecipeData recipe = m_recipes.GetRecipe(m_currentOrder);
        int correctIngredients = 0;
        foreach (IngredientType ingredient in recipe.m_ingredients)
        {
            if (m_selectedIngredients.Contains(ingredient))
                correctIngredients++;
        }
        m_selectedIngredients.Clear();
        m_deskManager.ClearIngredients();
        m_currentOrder = RecipeType.None;
        
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
        m_currentCustomer.MakeHappy();
        m_dialogBox.CustomerResponse(m_currentCustomer.m_responseHappy);
    }

    private void BakeFail()
    {
        m_currentCustomer.MakeSad();
        m_dialogBox.CustomerResponse(m_currentCustomer.m_responseSad);
    }

    public void PlayCustomerAudio(Customer _customer)
    {
        StopCustomerAudio();
        if (_customer == null)
            return;
        
        m_customerAudio.clip = _customer.m_audio;
        m_customerAudio.Play();
    }

    public void StopCustomerAudio()
    {
        m_customerAudio.Stop();
    }
}
