using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonRunAway : MonoBehaviour, IPointerEnterHandler
{
    public RectTransform m_gameObjectToMove;
    public float m_speed;

    private Vector2 m_targetPosition;
    private Vector2 m_startPosition;
    private float m_time;

    public void OnPointerEnter(PointerEventData _eventData)
    {
        int lowerBoundX = Random.Range(-480, 480);
        int upperBoundY = Random.Range(-280, 280);

        m_targetPosition = new Vector2(lowerBoundX, upperBoundY);
        m_startPosition = m_gameObjectToMove.anchoredPosition;
        m_time = 0;
    }

    private void Update()
    {
        if (m_time > 1)
            return;
        
        m_time += Time.deltaTime * m_speed;
        m_gameObjectToMove.anchoredPosition = Vector2.Lerp(m_startPosition, m_targetPosition, m_time);
    }
}
