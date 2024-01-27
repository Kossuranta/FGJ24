using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Image m_image;
    public RectTransform m_rect;
    
    public RecipeType m_order;
    public string[] m_dialog;
    public Sprite m_happy;
    public Sprite m_sad;

    public void MakeHappy()
    {
        m_image.sprite = m_happy;
    }

    public void MakeSad()
    {
        m_image.sprite = m_sad;
    }
}