using UnityEngine;
using TMPro;
using DG.Tweening;

public class TickTextHandler : MonoBehaviour
{
    private TextMeshPro textobject;

    private void Awake()
    {
        textobject = GetComponent(typeof(TextMeshPro)) as TextMeshPro;
    }

    public void init(int goldPerClick)
    {
        this.init(goldPerClick, transform.position);
    }

    public void init(int goldPerClick, Vector2 movePosition)
    {
        textobject.enabled = true;

        this.transform.position = movePosition;
        textobject.text = $"+{goldPerClick}";

        Tween tempo = DOTween.To(
            () => transform.position,
            (x) =>
            {
                transform.position = x;
            },
            new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z),
            1
        );
        tempo.OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
