using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelMenu : Menu
{
    [SerializeField] GameObject levelSlotPrefab;
    [SerializeField] Transform levelGrid;
    List<LevelSlot> levelSlots = new List<LevelSlot>();

    protected override void Start()
    {
        base.Start();
        StartCoroutine(CreateLevelSlots());
    }
    
    public override void UserInputButtonClicked(int x)
    {
        print("Click 2");
        switch (x)
        {
            case 1:
                MainMenuController.Instance.LoadMenu(0);
                break;
        }
    }

    public void PlayLevel(LevelSlot levelSlot)
    {
        GameManager.Instance.LoadScene(levelSlot.levelDataId);
    }

    protected override void UserInput()
    {
        base.UserInput();
        if (Input.GetButtonDown("Cancel"))
            MainMenuController.Instance.LoadMenu(0);
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
