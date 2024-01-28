using UnityEngine;
using UnityEngine.UI;

public class ButtonBroken : MonoBehaviour
{
    public Image m_image;
    public Sprite m_brokenSprite;
    public AudioClip m_audioClip;

    private bool m_spriteChanged;

    public void ChangeSprite()
    {
        if (m_spriteChanged)
            return;

        m_spriteChanged = true;
        m_image.sprite = m_brokenSprite;
        GameManager.Instance.PlayEffect(m_audioClip);
        GetComponent<Button>().interactable = false;
    }
}
