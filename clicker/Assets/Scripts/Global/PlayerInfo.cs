using UnityEngine;

// [DefaultExecutionOrder(10)]
// public class PlayerStats
// {
//     public int totalGold;

//     public int goldPerSecond;

//     public PlayerStats(int _totalGold, int _goldPerClick, int _goldPerSecond)
//     {
//         totalGold = _totalGold;
//         goldPerSecond = _goldPerSecond;
//     }

//     public PlayerStats()
//     {
//         totalGold = 0;
//         goldPerSecond = 0;
//     }
// }

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo current;

    public int totalGold;

    private LayerMask clickableLayerMask;

    [SerializeField]
    private Blocks blocksHandler;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        clickableLayerMask = LayerMask.GetMask("Clickable");
    }

    void Start()
    {
        totalGold = 0;
    }

    // private void FixedUpdate()
    // {
    //     Debug.Log(playerStats.totalGold);
    // }
}
