using UnityEngine;
using UnityEngine.UI;

public class SettingsCtrl : MonoBehaviour
{
    public ISettingsBacktracker backtracker;
    public Button B_Back;
    public void BackToPreviousMenu()
    {
        Debug.Log("Here need to close Settings");
        //should I make it so it know whether to return to MainMenu or gamepause screen?
        backtracker.SettingsClose();
    }
}
