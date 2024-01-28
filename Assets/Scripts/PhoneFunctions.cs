using UnityEngine;

public class PhoneFunctions : MonoBehaviour
{
    public void Call(int _callTarget)
    {
        
    }

    public void Close()
    {
        GameManager.Instance.ClosePhone();
    }
}