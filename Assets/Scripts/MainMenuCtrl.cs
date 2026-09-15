using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuCtrl : MonoBehaviour, ISettingsBacktracker
{
    GlobalSettingsSO GS;
    [SerializeField] SettingsCtrl settings;
    [SerializeField] GameObject MainPanel;
    [SerializeField] Quitter quitPanel;
    [SerializeField] Button gameStartButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;
    [Header("AutoFilled")]
    [SerializeField] PlayerInput input;
    [SerializeField] EventSystem UIEvent;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        InputAction back = input.actions.FindAction("Cancel");
        back.performed += QuitRefocus;
        back.canceled += QuitEnd;
    }
    private void OnDestroy()
    {
        InputAction back = input.actions.FindAction("Cancel");
        back.performed -= QuitRefocus;
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
            if (UIEvent.currentSelectedGameObject != quitButton.gameObject)
            {
                quitButton.Select();
            }
        }
    }
    public void QuitEnd(InputAction.CallbackContext obj)
    {
        GameExit();
        //if (UIevent.currentSelectedGameObject == quitButton.gameObject)
        //{
        //    Invoke("GameExit", 0);
        //}
    }

    private void QuitCanceled()
    {
        MainPanel.SetActive(true);
    }

    private void Start()
    {
        if ((GS = GlobalSettingsSO.GS) == null)
        {
            Destroy(gameObject);
        }
        UIEvent = GameObject.FindFirstObjectByType<EventSystem>();
        GS.currentScene = GlobalSettingsSO.CurrentScene.MAIN;
        quitPanel.handler = QuitCanceled;
        SettingsClose();
        settings.backtracker = this;
        gameStartButton.Select();
    }
    public void GameStart()
    {
        Debug.Log("Here need to swap scenes to CharacterSelect");
        GS.currentScene = GlobalSettingsSO.CurrentScene.CHAR_SEl;
    }

    public void GameExit()
    {
        MainPanel.SetActive(false);
        quitPanel.gameObject.SetActive(true);
    }
    public void SettingsOpen()
    {
        GS.inTheSettings = true;
        settings.gameObject.SetActive(true);
        MainPanel.SetActive(false);
    }
    public void SettingsClose()
    {
        settings.gameObject.SetActive(false);
        MainPanel.SetActive(true);
        GS.inTheSettings = false;
    }
}
