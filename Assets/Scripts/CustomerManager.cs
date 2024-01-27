using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Vector2 m_outOfScreenPos;
    public Vector2 m_customerPosition;
    public Customer m_customerPrefab;
    public float m_speed;
    
    private Customer m_customer;
    private Vector2 m_velocity;
    private float m_time;
    
    public void Initialize()
    {
        SpawnCustomer();
    }

    private void SpawnCustomer()
    {
        m_customer = Instantiate(m_customerPrefab, transform);
        m_customer.Initialize();
        m_time = 0;
    }

    private void Update()
    {
        m_time += Time.deltaTime * m_speed;
        m_customer.m_rect.anchoredPosition = Vector2.Lerp(m_outOfScreenPos, m_customerPosition, m_time);
    }
}