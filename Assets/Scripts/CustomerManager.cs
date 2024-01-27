using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Customer[] m_customers;
    
    public Vector2 m_outOfScreenPos;
    public Vector2 m_customerPosition;
    public float m_speed;
    
    private Customer m_customer;
    private Vector2 m_velocity;
    private float m_time;
    private int m_index;
    
    public void Initialize()
    {
        m_index = 0;
    }

    public void NextCustomer()
    {
        m_customer = Instantiate(m_customers[m_index], transform);
        m_index++;
        m_customer.Initialize();
        m_time = 0;
    }

    private void Update()
    {
        m_time += Time.deltaTime * m_speed;
        m_customer.m_rect.anchoredPosition = Vector2.Lerp(m_outOfScreenPos, m_customerPosition, m_time);
    }
}