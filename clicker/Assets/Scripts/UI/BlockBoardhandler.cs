using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBoardhandler : MonoBehaviour
{
    [SerializeField]
    private Blocks blocksHandler;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private BoardManager boardManager;

    private List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        int index = 0;

        RectTransform prefabRectTransform =
            buttonPrefab.GetComponent(typeof(RectTransform)) as RectTransform;

        foreach (ClassBlock level in blocksHandler.blocks)
        {
            index++;
            GameObject buttonObject = Instantiate(buttonPrefab, this.transform);
            buttons.Add(buttonObject);

            BlockButtonHandler buttonHandler =
                buttonObject.GetComponent(typeof(BlockButtonHandler)) as BlockButtonHandler;

            buttonHandler.initlize(level, onLevelClick, this.boardManager);

            float topMargin = prefabRectTransform.rect.height + (index == 1 ? 0 : 1);

            buttonObject.transform.localPosition = new Vector3(0, (-topMargin * index), 0);
        }
    }

    public void onLevelClick(ClassBlock level)
    {
        SoundEffectManager.current.triggerSoundEffect(GameSoundEffects.ON_TICK);

        if (this.boardManager.phase != BoardPhases.normal)
        {
            this.boardManager.exitPhases();
        }

        this.boardManager.enterBuildPhase(level);
    }
}
