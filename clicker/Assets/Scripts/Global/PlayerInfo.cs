using UnityEngine;

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
}
