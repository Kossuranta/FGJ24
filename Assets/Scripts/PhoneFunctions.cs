using System;
using UnityEngine;

public class PhoneFunctions : MonoBehaviour
{
    public GameObject m_contactsParent;
    public IngredientScreens[] m_ingredientScreens;
    public GameObject m_satanContact;

    private bool m_dialogWasOpen;

    private void OnEnable()
    {
        m_satanContact.SetActive(GameManager.Instance.m_satanHasVisited);
        m_dialogWasOpen = GameManager.Instance.m_dialogBox.gameObject.activeSelf;
        GameManager.Instance.m_dialogBox.gameObject.SetActive(false);
        ShowContacts();
    }

    public void Call(int _callTarget)
    {
        CallTarget target = (CallTarget) _callTarget;
        m_contactsParent.SetActive(false);
        foreach (IngredientScreens screen in m_ingredientScreens)
            screen.m_ingredientButtonParent.SetActive(target == screen.m_callTarget);
    }

    public void Order(int _ingredient)
    {
        IngredientType ingredient = (IngredientType) _ingredient;
        GameManager.Instance.AddIngredient(ingredient);
        ShowContacts();
        Close();
        GameManager.Instance.m_vehicleManager.SpawnTruck();
    }

    public void Close()
    {
        if (m_contactsParent.activeSelf)
        {
            GameManager.Instance.ClosePhone();
            if (m_dialogWasOpen)
                GameManager.Instance.m_dialogBox.gameObject.SetActive(true);
        }
        else
        {
            ShowContacts();
        }
    }

    private void ShowContacts()
    {
        m_contactsParent.SetActive(true);
        foreach (IngredientScreens screen in m_ingredientScreens)
            screen.m_ingredientButtonParent.SetActive(false);
    }
}

[Serializable]
public class IngredientScreens
{
    public CallTarget m_callTarget;
    public GameObject m_ingredientButtonParent;
}