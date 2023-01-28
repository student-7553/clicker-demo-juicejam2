using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int totalGold;
    public int goldPerClick;
    public int goldPerSecond;

    public PlayerStats(int _totalGold, int _goldPerClick, int _goldPerSecond)
    {
        totalGold = _totalGold;
        goldPerClick = _goldPerClick;
        goldPerSecond = _goldPerSecond;
    }

    public PlayerStats()
    {
        totalGold = 0;
        goldPerClick = 1;
        goldPerSecond = 0;
    }
}

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo current;

    public PlayerStats playerStats;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = new PlayerStats();
    }

    private bool isClickable = true;

    public void handlePlayerClick()
    {
        if (!isClickable)
        {
            return;
        }
        playerStats.totalGold = playerStats.totalGold + playerStats.goldPerClick;
    }
}
