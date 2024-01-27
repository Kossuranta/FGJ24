using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI m_dialogTextMesh;
    public Button m_buttonYes;
    public Button m_buttonNo;
    public Button m_nextDialog;

    private int m_dialogIndex;

    private void OnEnable()
    {
        m_nextDialog.gameObject.SetActive(true);
        m_dialogIndex = 0;
        NextDialog();
    }

    public void ButtonYes()
    {
        m_buttonYes.gameObject.SetActive(false);
        m_buttonNo.gameObject.SetActive(false);
        m_nextDialog.gameObject.SetActive(false);
        GameManager.Instance.MakeOrder();
    }

    public void ButtonNo()
    {
        
    }

    public void NextDialog()
    {
        string[] dialog = GameManager.Instance.m_currentCustomer.m_dialog;
        if (m_dialogIndex == dialog.Length)
        {
            GameManager.Instance.CustomerServed();
            gameObject.SetActive(false);
        }
        else
        {
            string line = dialog[m_dialogIndex];
            m_dialogIndex++;
            m_dialogTextMesh.text = line;
        
            m_buttonYes.gameObject.SetActive(m_dialogIndex == dialog.Length);
            m_buttonNo.gameObject.SetActive(m_dialogIndex == dialog.Length);
            m_nextDialog.gameObject.SetActive(m_dialogIndex != dialog.Length);
        }
    }

    public void CustomerResponse(string _response)
    {
        m_dialogTextMesh.text = _response;
        m_nextDialog.gameObject.SetActive(true);
    }
}
