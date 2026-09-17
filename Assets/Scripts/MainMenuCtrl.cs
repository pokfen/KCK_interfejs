using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuCtrl : MonoBehaviour, ISettingsBacktracker
{
    public GlobalSettingsSO GS;
    [SerializeField] SettingsCtrl settings;
    [SerializeField] GameObject MainPanel;
    [SerializeField] Quitter quitPanel;
    [SerializeField] Button B_gameStartButton;
    [SerializeField] Button B_settingsButton;
    [SerializeField] Button B_quitButton;
    [Header("AutoFilled")]
    [SerializeField] PlayerInput input;
    [SerializeField] EventSystem UIEvent;
    private void OnDestroy()
    {
        InputAction back = input.actions.FindAction("Cancel");
        back.started -= QuitRefocus;
        back.canceled -= QuitEnd;
    }
    private void QuitRefocus(InputAction.CallbackContext obj)
    {
        if (GS.inTheSettings)
        {
            if (UIEvent.currentSelectedGameObject != settings.B_Back)
            {
                settings.B_Back.Select();
            }
        }
        else
        {
            if (quitPanel.gameObject.activeSelf)
            {
                quitPanel.B_CANCEL.Select();
            }
            else
            if (UIEvent.currentSelectedGameObject != B_quitButton.gameObject)
            {
                B_quitButton.Select();
            }
        }
    }
    public void QuitEnd(InputAction.CallbackContext obj)
    {
        if (!GS.inTheSettings)
        {
            if (quitPanel.gameObject.activeSelf)
            {
                quitPanel.B_CANCEL.Select();
            }
            else
            {
                GameExit();
            }
        }
    }

    private void QuitCanceled()
    {
        MainPanel.SetActive(true);
        B_quitButton.Select();
    }

    private void Start()
    {
        GS.makeSureItsGS();
        UIEvent = GameObject.FindFirstObjectByType<EventSystem>();
        GS.currentScene = GlobalSettingsSO.CurrentScene.MAIN;
        quitPanel.handler = QuitCanceled;
        SettingsClose();
        settings.backtracker = this;
        input = GetComponent<PlayerInput>();
        InputAction back = input.actions.FindAction("Cancel");
        back.started += QuitRefocus;
        back.canceled += QuitEnd;
        B_gameStartButton.Select();
    }
    public void GameStart()
    {
        Debug.Log("Here need to swap scenes to CharacterSelect");
        GS.currentScene = GlobalSettingsSO.CurrentScene.CHAR_SEL;
    }

    public void GameExit()
    {
        MainPanel.SetActive(false);
        quitPanel.gameObject.SetActive(true);
        quitPanel.B_CANCEL.Select();
    }
    public void SettingsOpen()
    {
        GS.inTheSettings = true;
        settings.gameObject.SetActive(true);
        settings.B_Back.Select();
        MainPanel.SetActive(false);
    }
    public void SettingsClose()
    {
        settings.gameObject.SetActive(false);
        MainPanel.SetActive(true);
        GS.inTheSettings = false;
    }
}
