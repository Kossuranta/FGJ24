using UnityEngine;

public class Mixer : MonoBehaviour
{
    public Animator m_animator;
    public AudioSource m_audioSource;

    private void Awake()
    {
        StopMixer();
    }

    public void StartMixer()
    {
        m_animator.enabled = true;
        m_audioSource.Play();
    }

    public void StopMixer()
    {
        m_animator.enabled = false;
        m_audioSource.Stop();
    }
}
