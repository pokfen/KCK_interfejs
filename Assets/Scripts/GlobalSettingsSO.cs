using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GlobalSettingsSO", menuName = "Scriptable Objects/GlobalSettingsSO")]
public class GlobalSettingsSO : ScriptableObject
{
    #region singletonning
    public static GlobalSettingsSO GS { get; private set; }
    GlobalSettingsSO()
    {
        if (GS == null)
            GS = this;
        else
            Destroy(this);
    }
    ~GlobalSettingsSO()
    {
        if (GS == this)
            GS = null;
    }
    #endregion
    #region SceneManagment
    public int scenesCount { get { return SceneManager.sceneCount; } }
    public enum CurrentScene : byte
    {
        MAIN,
        CHAR_SEL,
        GAME
    };

    private CurrentScene m_currentScene;
    public CurrentScene currentScene
    {
        get { return m_currentScene; }
        set
        {
            if (!SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex((int)value)))
            {
                m_currentScene = value;
                SceneManager.LoadScene((int)value);
            }
        }
    }

    public bool inTheSettings;
    public Sprite[] charSelSprites;
    public GameObject[] character;
    public Material[] skin;

    public short char1Id = -1;
    public short char2Id = -1;

    public short char1SkinId = -1;
    public short char2SkinId = -1;
    public void GameExit()
    {
        Debug.Log("Quit The game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    #endregion
}
