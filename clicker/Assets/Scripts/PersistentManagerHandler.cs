using UnityEngine;

public class PersistentManagerHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
