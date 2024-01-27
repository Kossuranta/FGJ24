using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [NonSerialized]
    public RectTransform m_rect;
    public string[] dialogArray;

    public void Initialize()
    {
        m_rect = GetComponent<RectTransform>();
    }
}