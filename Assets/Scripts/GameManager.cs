using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MainMenu m_mainMenu;
    public DeskManager m_deskManager;
    public CustomerManager m_customerManager;

    [NonSerialized]
    public readonly List<IngredientType> m_selectedIngredients = new();

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
        if (m_selectedIngredients.Count >= 6)
            return;
        
        if (m_customer == null)
            return;
        
        m_selectedIngredients.Add(_ingredient);
    }

    private void BakeBun()
    {
        
    }

    public void CustomerServed()
    {
        m_customerManager.CustomerLeave();
    }

    public void CustomerLeft()
    {
        m_customerManager.NextCustomer();
    }
}
