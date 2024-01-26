using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public DeskManager m_deskManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("ERROR! Multiple GameManagers are not allowed!");
            DestroyImmediate(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        m_deskManager.Initialize();
    }
}
