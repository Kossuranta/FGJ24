using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [NonSerialized]
    public RectTransform m_rect;

    public void Initialize()
    {
        m_rect = GetComponent<RectTransform>();
    }
}