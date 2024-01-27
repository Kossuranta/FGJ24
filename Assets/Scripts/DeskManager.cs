using UnityEngine;
using UnityEngine.UI;

public class DeskManager : MonoBehaviour
{
    public RecipeManager m_recipeManager;
    public Image[] m_ingredientSlots;

    public void Initialize()
    {
        m_recipeManager.Initialize();
    }

    public void StartDay(int _day)
    {
        m_recipeManager.GenerateRecipes(_day);
        ClearIngredients();
    }

    public void ClearIngredients()
    {
        foreach (Image image in m_ingredientSlots)
            image.enabled = false;
    }

    public void SetIngredient(int _index, IngredientType _ingredient)
    {
        Sprite sprite = GameManager.Instance.m_ingredientSprites.GetSprite(_ingredient);
        m_ingredientSlots[_index].sprite = sprite;
        m_ingredientSlots[_index].enabled = true;
    }
}
