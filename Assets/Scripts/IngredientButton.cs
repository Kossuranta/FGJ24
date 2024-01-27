using UnityEngine;

public class IngredientButton : MonoBehaviour
{
    public IngredientType m_ingredient;

    public void AddIngredient()
    {
        GameManager.Instance.AddIngredient(m_ingredient);
    }
}
