using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly GameManager s_instance = new();

    public static GameManager Instance => s_instance;

    public int m_test = 1;
}
