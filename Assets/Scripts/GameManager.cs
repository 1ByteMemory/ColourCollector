using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject ModuleUI;

    // Start is called before the first frame update
    void Start()
    {
        ToggleCursor(false);
        ToggleUI(PauseScreen, false);
        ToggleUI(ModuleUI, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            ToggleCursor();

            ToggleUI(PauseScreen, Cursor.visible);
            Time.timeScale = Cursor.visible ? 0 : 1;
		}

        if (Input.GetKeyDown(KeyCode.E))
		{
            if (!PauseScreen.activeSelf)
            {
                ToggleCursor();
                ToggleUI(ModuleUI, Cursor.visible);
            }
		}
    }

    void ToggleUI(GameObject ui, bool visable)
	{
        if (ui != null) ui.SetActive(visable);
    }

    void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
    void ToggleCursor(bool visable)
	{
	    Cursor.visible = visable;
        Cursor.lockState = visable ? CursorLockMode.None : CursorLockMode.Locked;
	}
}
