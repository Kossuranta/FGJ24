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
    public Transform m_day2BossParent;
    public AudioClip m_customerEnter;

    [NonSerialized]
    public Customer m_boss;
    [NonSerialized]
    public int m_bossIndex;
    private Customer m_customer;
    private Vector2 m_velocity;
    [NonSerialized]
    public int m_customerIndex;
    
    public void Initialize()
    {
        m_customerIndex = 0;
        m_bossIndex = 0;
    }

    public void CustomerLeave()
    {
        GameManager.Instance.m_dialogBox.gameObject.SetActive(false);
        GameManager.Instance.StopCustomerAudio();
        StartCoroutine(CustomerExit());
    }

    public void NextCustomer()
    {
        if (m_customerIndex >= m_customers.Length)
        {
            GameManager.Instance.EndGame();
            return;
        }
        m_customer = Instantiate(m_customers[m_customerIndex], transform);
        m_customer.Initialize();
        m_customerIndex++;
        
        StartCoroutine(CustomerEnter());
        
        if (m_customer.m_isSatan)
            GameManager.Instance.m_satanHasVisited = true;
        
        if (m_customer.m_order != RecipeType.None && !m_customer.transform.name.Contains("Me"))
            GameManager.Instance.PlayEffect(m_customerEnter);
        
        if (m_customer.m_playAmbulanceInBackground)
            GameManager.Instance.m_vehicleManager.SpawnAmbulance();
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
        
        if (m_bossIndex == 1)
            m_boss = Instantiate(m_bosses[m_bossIndex], m_day2BossParent);
        else
            m_boss = Instantiate(m_bosses[m_bossIndex], transform);
        m_boss.Initialize();
        m_bossIndex++;
        StartCoroutine(BossEnter());
    }
    
    public void HideBoss()
    {
        GameManager.Instance.StopCustomerAudio();
        StartCoroutine(BossExit());
    }

    private IEnumerator CustomerEnter()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / m_customer.m_moveDuration;
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
            time += Time.deltaTime / m_customer.m_moveDuration;
            m_customer.m_rect.anchoredPosition = Vector2.Lerp(m_customerPosition, m_outOfScreenPos, time);
            yield return 0;
        }
        
        GameManager.Instance.CustomerLeft();
    }
    
    private IEnumerator BossEnter()
    {
        float time = 0;
        PositionPair pos = m_bossPositions[m_bossIndex - 1];
        m_boss.m_rect.localRotation = Quaternion.Euler(0, 0, pos.m_rotation);
        while (time < 1)
        {
            time += Time.deltaTime / m_boss.m_moveDuration;
            m_boss.m_rect.anchoredPosition = Vector2.Lerp(pos.m_outOfScreenPos, pos.m_targetPos, time);
            yield return 0;
        }
        
        GameManager.Instance.ShowBossDialog(m_boss);
    }
    
    private IEnumerator BossExit()
    {
        float time = 0;
        PositionPair pos = m_bossPositions[m_bossIndex - 1];
        while (time < 1)
        {
            time += Time.deltaTime / m_boss.m_moveDuration;
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