using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Image m_image;
    public RectTransform m_rect;
    
    public RecipeType m_order;
    public Sprite m_happy;
    public Sprite m_sad;

    public string[] m_dialog;
    public string m_responseHappy;
    public string m_responseSad;
    public bool m_showNoButton;

    public void Initialize()
    {
        MakeHappy();
    }

    public void MakeHappy()
    {
        m_image.sprite = m_happy;
    }

    public void MakeSad()
    {
        m_image.sprite = m_sad;
    }
}