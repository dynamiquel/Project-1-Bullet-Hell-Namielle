using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : Menu
{
    [SerializeField] GameObject levelSlotPrefab;
    [SerializeField] Transform levelGrid;
    List<LevelSlot> levelSlots = new List<LevelSlot>();

    private void Start()
    {
        StartCoroutine(CreateLevelSlots());
    }

    public void PlayLevel(LevelSlot levelSlot)
    {
        GameManager.Instance.LoadScene(levelSlot.levelDataId);
    }

    IEnumerator CreateLevelSlots()
    {
        yield return new WaitUntil(() => ItemDatabase.Instance.Loaded);

        foreach (var level in ItemDatabase.Instance.LevelDatas)
        {
            LevelSlot levelSlot = Instantiate(levelSlotPrefab, levelGrid).GetComponent<LevelSlot>();
            levelSlot.Setup(level.Key, this);
            levelSlots.Add(levelSlot);
        }

        if (levelSlots.Count > 0)
            GetComponent<UnityEngine.EventSystems.EventSystem>().firstSelectedGameObject = levelSlots[0].gameObject;
    }
}
