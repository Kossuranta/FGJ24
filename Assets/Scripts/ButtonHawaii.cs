using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHawaii : MonoBehaviour
{
    public Image m_imageDefault;
    public Image m_imageHawaii;
    public AudioSource m_audioSource;
    public float m_targetPosX;
    public float m_speed;

    private bool m_spriteChanged;

    public void Leave()
    {
        if (m_spriteChanged)
            return;

        m_spriteChanged = true;
        m_imageDefault.enabled = false;
        m_imageHawaii.enabled = true;
        m_audioSource.Play();
        GetComponent<Button>().interactable = false;
        StartCoroutine(LeaveRoutine());
    }

    private IEnumerator LeaveRoutine()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 startPos = rect.anchoredPosition;
        Vector2 targetPos = startPos;
        targetPos.x = m_targetPosX;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * m_speed;
            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, time);
            m_audioSource.panStereo = -time;
            yield return 0;
        }
    }
}
