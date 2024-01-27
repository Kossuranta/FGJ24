using System;
using System.Collections;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Customer[] m_customers;
    public Customer[] m_bosses;
    
    public Vector2 m_outOfScreenPos;
    public Vector2 m_customerPosition;
    public PositionPair[] m_bossPositions;
    public float m_speed;

    [NonSerialized]
    public Customer m_boss;
    private int m_bossIndex;
    private int m_bossPosIndex;
    private Customer m_customer;
    private Vector2 m_velocity;
    private int m_customerIndex;
    
    public void Initialize()
    {
        m_customerIndex = 0;
        m_bossIndex = 0;
        m_bossPosIndex = 0;
    }

    public void CustomerLeave()
    {
        StartCoroutine(CustomerExit());
    }

    public void NextCustomer()
    {
        m_customer = Instantiate(m_customers[m_customerIndex], transform);
        m_customer.Initialize();
        m_customerIndex++;

        StartCoroutine(CustomerEnter());
    }

    public void ShowBoss()
    {
        if (m_bossIndex >= m_bosses.Length)
        {
            m_bossIndex = 0;
            Debug.LogError("Out of bosses, replaying first!!!");
        }
        
        if (m_boss != null)
            Destroy(m_boss.gameObject);
        
        m_boss = Instantiate(m_bosses[m_bossIndex], transform);
        m_boss.Initialize();
        m_bossIndex++;
        StartCoroutine(BossEnter());
    }
    
    public void HideBoss()
    {
        StartCoroutine(BossExit());
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
    
    private IEnumerator BossEnter()
    {
        float time = 0;
        PositionPair pos = m_bossPositions[m_bossPosIndex];
        while (time < 1)
        {
            time += Time.deltaTime * m_speed;
            m_boss.m_rect.anchoredPosition = Vector2.Lerp(pos.m_outOfScreenPos, pos.m_targetPos, time);
            yield return 0;
        }
        
        GameManager.Instance.ShowBossDialog(m_boss);
    }
    
    private IEnumerator BossExit()
    {
        float time = 0;
        PositionPair pos = m_bossPositions[m_bossPosIndex];
        m_bossPosIndex++;
        while (time < 1)
        {
            time += Time.deltaTime * m_speed;
            m_boss.m_rect.anchoredPosition = Vector2.Lerp(pos.m_targetPos, pos.m_outOfScreenPos, time);
            yield return 0;
        }
        
        Destroy(m_boss.gameObject);
        m_boss = null;
    }
}

[Serializable]
public class PositionPair
{
    public Vector2 m_outOfScreenPos;
    public Vector2 m_targetPos;
    public float m_rotation;
}