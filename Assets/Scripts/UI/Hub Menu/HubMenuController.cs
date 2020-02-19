using UnityEngine;

public class HubMenuController : MonoBehaviour
{
    [SerializeField] PauseMenuController pauseMenuController;
    [SerializeField] GameObject perksMenu;

    public HubMenuState HubMenuState { get; private set; } = HubMenuState.Closed;
    HubMenuState lastValidHubMenu = HubMenuState.Perks;

    private void Update()
    {
        // Prevents the Hub from opening if the Pause Menu is open.
        if (pauseMenuController.PauseMenuState == PauseMenuState.Closed)
        {
            if (Input.GetButtonDown("Hub"))
                switch (HubMenuState)
                {
                    case HubMenuState.Closed:
                        OpenMenu();
                        break;
                    default:
                        CloseMenu();
                        break;
                }
        }
    }

    public void OpenMenu()
    {
        pauseMenuController.PauseGame(true);

        // Opens the last opened menu.
        switch (lastValidHubMenu)
        {
            case HubMenuState.Perks:
                OpenPerkMenu();
                break;
        }
    }

    public void CloseMenu()
    {
        perksMenu.SetActive(false);

        pauseMenuController.PauseGame(false);
        // Saves the last opened menu.
        lastValidHubMenu = HubMenuState;
        HubMenuState = HubMenuState.Closed;
    }

    void OpenPerkMenu()
    {
        HubMenuState = HubMenuState.Perks;

        perksMenu.SetActive(true);
    }
}

public enum HubMenuState
{
    Closed,
    Perks
}