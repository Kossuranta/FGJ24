using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI m_dialogTextMesh;
    public Button m_buttonYes;
    public Button m_buttonNo;
    public Button m_nextDialog;

    public Image m_speechBubble;
    public Image m_thoughtBubble;

    private int m_dialogIndex;
    private string[] m_dialog;
    private int m_overrideIndex;
    private string[] m_overrideDialog;
    private bool m_bossSaidNo;

    public void ShowDialog(string[] _dialog)
    {
        Debug.Log($"Show Dialog");
        m_bossSaidNo = false;
        m_overrideDialog = null;
        m_dialog = _dialog;
        m_dialogIndex = 0;
        m_nextDialog.gameObject.SetActive(true);
        NextDialog();
        gameObject.SetActive(true);
    }

    public void ShowOverrideDialog(string[] _dialog)
    {
        Debug.Log($"Show Override Dialog");
        m_overrideDialog = _dialog;
        m_overrideIndex = 0;
        NextDialog();
        m_nextDialog.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void ButtonYes()
    {
        m_buttonYes.gameObject.SetActive(false);
        m_buttonNo.gameObject.SetActive(false);
        m_nextDialog.gameObject.SetActive(false);
        GameManager.Instance.OnButtonYes();
    }

    public void ButtonNo()
    {
        GameManager.Instance.OnButtonNo();
    }

    public void NextDialog()
    {
        if (m_overrideDialog == null)
        {
            if (m_dialogIndex >= m_dialog.Length)
            {
                GameManager.Instance.CustomerServed();
                gameObject.SetActive(false);
                return;
            }

            if (m_dialogIndex == 0)
            {
                GameManager.Instance.PlayCustomerAudio(GameManager.Instance.m_currentCustomer);
                Debug.Log("Play audio");
            }
            
            string line = m_dialog[m_dialogIndex];
            m_dialogIndex++;
            SetDialogText(line);
        
            m_buttonYes.gameObject.SetActive(m_dialogIndex == m_dialog.Length);
            if (!GameManager.Instance.m_currentCustomer.m_showNoButton || m_bossSaidNo)
                m_buttonNo.gameObject.SetActive(false);
            else
                m_buttonNo.gameObject.SetActive(m_dialogIndex == m_dialog.Length);
            m_nextDialog.gameObject.SetActive(m_dialogIndex != m_dialog.Length);
        }
        else
        {
            if (m_overrideIndex >= m_overrideDialog.Length)
            {
                m_overrideDialog = null;
                m_dialogIndex--;
                m_bossSaidNo = true;
                GameManager.Instance.m_customerManager.HideBoss();
                NextDialog();
                return;
            }
            
            if (m_overrideIndex == 0)
            {
                GameManager.Instance.PlayCustomerAudio(GameManager.Instance.Boss);
            }
            
            string line = m_overrideDialog[m_overrideIndex];
            m_overrideIndex++;
            SetDialogText(line);
        
            m_buttonYes.gameObject.SetActive(false);
            m_buttonNo.gameObject.SetActive(false);
            m_nextDialog.gameObject.SetActive(true);
        }
    }

    public void CustomerResponse(string _response)
    {
        SetDialogText(_response);
        m_nextDialog.gameObject.SetActive(true);
    }

    public void SetDialogText(string _text)
    {
        m_dialogTextMesh.text = _text;

        bool isPlayer = !string.IsNullOrEmpty(_text) && _text[0] == '(';
        m_speechBubble.enabled = !isPlayer;
        m_thoughtBubble.enabled = isPlayer;
    }
}
