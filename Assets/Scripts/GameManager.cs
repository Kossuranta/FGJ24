using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public DeskManager m_deskManager;
    public CustomerManager m_customerManager;

    [NonSerialized]
    public readonly List<IngredientType> m_selectedIngredients = new();

    [NonSerialized]
    public Customer m_customer;

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
        m_deskManager.Initialize();
        m_customerManager.Initialize();
        
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
}
