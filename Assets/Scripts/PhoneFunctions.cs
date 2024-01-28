using UnityEngine;

public class PhoneFunctions : MonoBehaviour
{
    public DialogBox m_dialogBox;
    
    private bool m_dialogWasOpen;
    
    private void OnEnable()
    {
        m_dialogWasOpen = GameManager.Instance.m_dialogBox.gameObject.activeSelf;
        GameManager.Instance.m_dialogBox.gameObject.SetActive(false);
    }

    public void Call(int _callTarget)
    {
        
    }

    public void Close()
    {
        GameManager.Instance.ClosePhone();
        if (m_dialogWasOpen)
            GameManager.Instance.m_dialogBox.gameObject.SetActive(true);
    }
}