using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Quitter : MonoBehaviour
{
    public Button B_QUIT;
    public Button B_CANCEL;
    public delegate void CancelHandler();
    public CancelHandler handler;
    public void ConfirmQuit()
    {
        GlobalSettingsSO.GS.GameExit();
    }
    public void CancelQuit()
    {
        if (handler != null)
        {
            gameObject.SetActive(false);
            handler();
        }
    }
}
