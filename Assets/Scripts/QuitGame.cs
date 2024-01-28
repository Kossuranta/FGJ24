using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Image m_image;
    public Button m_button;
    
    private void Awake()
    {
        m_image.enabled = false;
        m_button.enabled = false;
        Invoke(nameof(ShowButton), 25);
    }

    private void ShowButton()
    {
        m_image.enabled = true;
        m_button.enabled = true;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}