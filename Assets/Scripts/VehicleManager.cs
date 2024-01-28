using System.Collections;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public RectTransform m_truck;
    public AudioSource m_truckAudio;
    
    public RectTransform m_ambulance;
    public AudioSource m_ambulanceAudio;
    public float m_truckSpeed;
    public float m_ambulanceSpeed;

    public Vector2 m_leftPos;
    public Vector2 m_rightPos;
    
    public void SpawnTruck()
    {
        StartCoroutine(MoveTruck());
    }

    private IEnumerator MoveTruck()
    {
        float time = 0;
        m_truckAudio.Play();
        while (time < 1)
        {
            time += Time.deltaTime * m_truckSpeed;
            m_truck.anchoredPosition = Vector2.Lerp(m_leftPos, m_rightPos, time);
            m_truckAudio.panStereo = time * 2f - 1f;
            yield return 0;
        }
        m_truckAudio.Stop();
    }

    public void SpawnAmbulance()
    {
        StartCoroutine(MoveAmbulance());
    }
    
    private IEnumerator MoveAmbulance()
    {
        float time = 0;
        m_ambulanceAudio.Play();
        while (time < 1)
        {
            time += Time.deltaTime * m_ambulanceSpeed;
            m_ambulance.anchoredPosition = Vector2.Lerp(m_rightPos, m_leftPos, time);
            m_ambulanceAudio.panStereo = (time * 2f - 1f) * -1f;
            yield return 0;
        }
        m_ambulanceAudio.Stop();
    }
}
