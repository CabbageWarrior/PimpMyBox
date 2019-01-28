using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        SelectButton(selectedMenuVoice);
    }

    private void Update()
    {
        if (isMenuEnabled)
        {
            float yAxis1 = Input.GetAxis("Vertical-P1");
            float yAxis2 = Input.GetAxis("Vertical-P2");

            if (yAxis1 < deadAmount * -1 || yAxis1 > deadAmount)
            {
                yAxis1 = (yAxis1 < 0 ? -1 : 1);
            }
            else
            {
                yAxis1 = 0;
            }

            if (yAxis2 < deadAmount * -1 || yAxis2 > deadAmount)
            {
                yAxis2 = (yAxis2 < 0 ? -1 : 1);
            }
            else
            {
                yAxis2 = 0;
            }

            float yAxis = yAxis1 + yAxis2;

            if (yAxis != 0)
            {
                yAxis = (yAxis < 0 ? -1 : 1);
            
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

            if (Input.GetButton("AButton-P1") || Input.GetButton("AButton-P2"))
            {
                PressButton(buttons[selectedMenuVoice]);
            }
        }

        else if (isCreditsPanelOpen)
        {
            if (Input.GetButton("BButton-P1") || Input.GetButton("BButton-P2"))
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

        SelectButton(selectedMenuVoice);
    }
    private void Next()
    {
        if (selectedMenuVoice >= buttons.Length - 1)
        {
            selectedMenuVoice = buttons.Length - 1;
            return;
        }

        selectedMenuVoice++;

        SelectButton(selectedMenuVoice);
    }

    private void SelectButton(int buttonIndex)
    {
        Button button = buttons[buttonIndex];

        button.Select();

        AudioSingleton.PlaySound(AudioSingleton.Sound.MenuButton);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetChild(0).gameObject.SetActive(i == buttonIndex);
        }
    }
    private void PressButton(Button button)
    {
        isMenuEnabled = false;
        button.onClick.Invoke();
        
        AudioSingleton.PlaySound(AudioSingleton.Sound.MenuButton);
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
        CreditsPanel.SetActive(false);
        isMenuEnabled = true;
        isCreditsPanelOpen = false;

        AudioSingleton.PlaySound(AudioSingleton.Sound.MenuButton);
    }
}
