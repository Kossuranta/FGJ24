using UnityEngine;

public class DeskManager : MonoBehaviour
{
    public RecipeManager m_recipeManager;

    public void Initialize()
    {
        m_recipeManager.Initialize();
    }

    public void StartDay(int _day)
    {
        m_recipeManager.GenerateRecipes(_day);
    }
}
