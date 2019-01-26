using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private int selectedMenuVoice = 0;
    private bool isMenuEnabled = true;
    private bool canSwitchMenuVoice = true;
    
    private bool isCreditsPanelOpen = false;

    public Button[] buttons;
    public float deadAmount = .19f;
    [Space]
    public GameObject CreditsPanel;

    private void Start()
    {
        SelectButton(buttons[selectedMenuVoice]);
    }
    private void Update()
    {
        if (isMenuEnabled)
        {
            float yAxis = Input.GetAxis("Vertical-P1");

            if (yAxis < deadAmount * -1 || yAxis > deadAmount)
            {
                // Thumb in Switch position.
                if (canSwitchMenuVoice)
                {
                    canSwitchMenuVoice = false;
                    if (yAxis < 0)
                    {
                        Previous();
                    }
                    else
                    {
                        Next();
                    }
                }
            }
            else
            {
                // Thumb in Relax position.
                if (!canSwitchMenuVoice)
                    canSwitchMenuVoice = true;
            }

            if (Input.GetButton("AButton-P1"))
            {
                PressButton(buttons[selectedMenuVoice]);
            }
        }

        else if (isCreditsPanelOpen)
        {
            if (Input.GetButton("BButton-P1"))
            {
                CloseCreditsPanel();
            }
        }
    }

    private void Previous()
    {
        if (selectedMenuVoice <= 0)
        {
            selectedMenuVoice = 0;
            return;
        }

        selectedMenuVoice--;

        SelectButton(buttons[selectedMenuVoice]);
    }
    private void Next()
    {
        if (selectedMenuVoice >= buttons.Length - 1)
        {
            selectedMenuVoice = buttons.Length - 1;
            return;
        }

        selectedMenuVoice++;

        SelectButton(buttons[selectedMenuVoice]);
    }

    private void SelectButton(Button button)
    {
        button.Select();
    }
    private void PressButton(Button button)
    {
        isMenuEnabled = false;
        button.onClick.Invoke();
    }

    public void StartMatch()
    {
        SceneManager.LoadScene("Game");
    }
    public void OpenCreditsPanel()
    {
        // ToDo: Toggle a panel.
        isMenuEnabled = false;
        isCreditsPanelOpen = true;
        CreditsPanel.SetActive(true);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    private void CloseCreditsPanel()
    {
        // ToDo: Toggle a panel.
        CreditsPanel.SetActive(false);
        isMenuEnabled = true;
        isCreditsPanelOpen = false;
    }
}
