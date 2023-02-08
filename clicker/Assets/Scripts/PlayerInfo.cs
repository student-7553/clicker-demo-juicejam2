using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(10)]
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

    private LayerMask clickableLayerMask;

    // public Camera mainCamera;

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

    // Start is called before the first frame update
    void Start()
    {
        playerStats = new PlayerStats();
    }

    public void handlePlayerClick()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        // Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 40, clickableLayerMask);
        if (hit.collider == null)
        {
            return;
        }

        this.handleTick();
    }

    public void handleTick()
    {
        playerStats.totalGold = playerStats.totalGold + playerStats.goldPerClick;
    }
}
