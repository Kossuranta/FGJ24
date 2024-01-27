using System.Collections;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Customer[] m_customers;
    
    public Vector2 m_outOfScreenPos;
    public Vector2 m_customerPosition;
    public float m_speed;
    
    private Customer m_customer;
    private Vector2 m_velocity;
    private int m_index;
    
    public void Initialize()
    {
        m_index = 0;
    }

    public void CustomerLeave()
    {
        StartCoroutine(CustomerExit());
    }

    public void NextCustomer()
    {
        m_customer = Instantiate(m_customers[m_index], transform);
        m_customer.Initialize();
        m_index++;

        StartCoroutine(CustomerEnter());
    }

    private IEnumerator CustomerEnter()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * m_speed;
            m_customer.m_rect.anchoredPosition = Vector2.Lerp(m_outOfScreenPos, m_customerPosition, time);
            yield return 0;
        }

        GameManager.Instance.CustomerReady(m_customer);
    }
    
    private IEnumerator CustomerExit()
    {
        GameManager.Instance.m_currentCustomer = null;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * m_speed;
            m_customer.m_rect.anchoredPosition = Vector2.Lerp(m_customerPosition, m_outOfScreenPos, time);
            yield return 0;
        }
        
        GameManager.Instance.CustomerLeft();
    }
}