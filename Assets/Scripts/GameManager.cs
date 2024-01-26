using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public DeskManager m_deskManager;

    public const int SCREEN_HEIGHT = 1080;
    public const int SCREEN_WIDTH = 1920;

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
