using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Recipe : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [NonSerialized]
    public RectTransform m_rect;
    
    private RecipeManager m_recipeManager;
    private Canvas m_canvas;
    private Vector2 m_offset;

    public void Initialize(RecipeManager _recipeManager, Canvas _canvas)
    {
        m_recipeManager = _recipeManager;
        m_canvas = _canvas;
        m_rect = (RectTransform)transform;
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