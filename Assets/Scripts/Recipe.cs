using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Recipe : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Image m_productImage;
    public Image[] m_ingredientImages;
    
    [NonSerialized]
    public RectTransform m_rect;
    
    private RecipeManager m_recipeManager;
    private Canvas m_canvas;
    private Vector2 m_offset;

    public void Initialize(RecipeManager _recipeManager, Canvas _canvas, RecipeData _recipeData)
    {
        m_recipeManager = _recipeManager;
        m_canvas = _canvas;
        m_rect = (RectTransform)transform;

        m_productImage.sprite = _recipeData.m_sprite;
        for (int i = 0; i < m_ingredientImages.Length; i++)
        {
            Sprite sprite = GameManager.Instance.m_ingredientSprites.GetSprite(_recipeData.m_ingredients[i]);
            m_ingredientImages[i].sprite = sprite;
        }
    }
    
    public void OnPointerDown(PointerEventData _eventData)
    {
        m_recipeManager.OnRecipeDragStart(this);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rect, _eventData.position, m_canvas.worldCamera, out m_offset);
    }

    public void OnDrag(PointerEventData _eventData)
    {
        Vector2 screenPos = Input.mousePosition;
        float widthMultiplier = (float)GameManager.SCREEN_WIDTH / Screen.width;
        float heightMultiplier = (float)GameManager.SCREEN_HEIGHT / Screen.height;
        float xPos = screenPos.x * widthMultiplier;
        float yPos = screenPos.y * heightMultiplier;
        m_recipeManager.SetRecipePosition(m_rect, new Vector2(xPos, yPos) - m_offset);
    }
}