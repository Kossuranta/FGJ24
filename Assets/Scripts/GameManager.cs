using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MainMenu m_mainMenu;
    public DeskManager m_deskManager;
    public CustomerManager m_customerManager;
    public VehicleManager m_vehicleManager;
    public DialogBox m_dialogBox;
    public Phone m_phone;
    public PhoneFunctions m_phoneScreen;

    public Recipes m_recipes;
    public IngredientSprites m_ingredientSprites;

    public AudioSource m_customerAudio;
    public AudioSource m_effectAudio;

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

    private bool m_bakingSuccess;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("ERROR! Multiple GameManagers are not allowed!");
            DestroyImmediate(this);
            return;
        }

        Instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        m_deskManager.Initialize();
        m_customerManager.Initialize();
        
        m_phone.gameObject.SetActive(false);
        m_dialogBox.gameObject.SetActive(false);
        
        m_mainMenu.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        m_mainMenu.gameObject.SetActive(false);
        
        ClosePhone();
        StartDay(0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
            m_vehicleManager.SpawnAmbulance();
        if (Input.GetKeyUp(KeyCode.T))
            m_vehicleManager.SpawnTruck();
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndCredits");
    }

    public void StartDay(int _day)
    {
        m_currentOrder = RecipeType.None;
        m_deskManager.StartDay(_day);
        m_customerManager.NextCustomer();
    }

    public void OnButtonYes()
    {
        if (m_currentCustomer.m_order == RecipeType.None)
        {
            m_customerManager.CustomerLeave();
            return;
        }

        if (m_currentCustomer.m_order == RecipeType.KarelianPie)
            m_customerManager.m_bossIndex = 1;
        
        if (m_currentOrder == RecipeType.None)
        {
            if (m_currentCustomer.m_order == RecipeType.HotDog &&
                m_customerManager.m_bossIndex == 1)
                ShowNextBoss();
            else
                MakeOrder();
        }
        else
        {
            //Order resource
        }
    }

    public void OnButtonNo()
    {
        ShowNextBoss();
    }

    private void ShowNextBoss()
    {
        m_dialogBox.m_nextDialog.gameObject.SetActive(false);
        m_customerManager.ShowBoss();
    }

    public void ShowBossDialog(Customer _boss)
    {
        if (m_customerManager.m_bossIndex == 2)
            m_phone.gameObject.SetActive(true);
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
        
        Debug.Log($"Ingredients selected, correctness: {correctIngredients}/4");
        m_bakingSuccess = correctIngredients == 4;
    }

    public void CustomerServed()
    {
        m_customerManager.CustomerLeave();
    }

    public void CustomerLeft()
    {
        m_customerManager.NextCustomer();
    }

    public void MixingCompleted()
    {
        if (m_bakingSuccess)
            BakeSuccess();
        else
            BakeFail();
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

    public void OpenPhone()
    {
        if (m_currentOrder == RecipeType.None)
            return;
        
        m_phoneScreen.gameObject.SetActive(true);
    }

    public void ClosePhone()
    {
        m_phoneScreen.gameObject.SetActive(false);
    }

    public void PlayEffect(AudioClip _clip)
    {
        m_effectAudio.PlayOneShot(_clip);
    }
}
