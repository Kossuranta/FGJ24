using UnityEngine;
using UnityEngine.EventSystems;

public class Recipe : MonoBehaviour, IDragHandler
{
    private Canvas m_canvas;
    private RectTransform m_rect;

    public void Initialize(Canvas _canvas)
    {
        m_canvas = _canvas;
        m_rect = (RectTransform)transform;
    }
    
    public void OnDrag(PointerEventData _eventData)
    {
        Vector2 screenPos = Input.mousePosition;
        float widthMultiplier = 1920f / Screen.width;
        float heightMultiplier = 1080f / Screen.height;
        float xPos = screenPos.x * widthMultiplier;
        float yPos = screenPos.y * heightMultiplier;
        m_rect.anchoredPosition = new Vector2(xPos, yPos);
    }
}