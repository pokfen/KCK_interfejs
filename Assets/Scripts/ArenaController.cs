using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ArenaController : MonoBehaviour
{
    [SerializeField] Transform P1Transform;
    [SerializeField] Transform P2Transform;
    [SerializeField] ProgressBar Progress1;
    [SerializeField] ProgressBar Progress2;
    [SerializeField] ProgressBar ProgressPause;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject VictoryPanel;
    [SerializeField] Button PausePanelResume;
    [SerializeField] Button VictoryPanelRematch;
    [SerializeField] TMP_Text Winner;
    [Header("AutoFilled")]
    public CharacterPrefabCtrl P1Character;
    public CharacterPrefabCtrl P2Character;
    GlobalSettingsSO GS;
    [SerializeField] PlayerInput input;
    InputAction Q;
    InputAction E;
    bool b_pausing;
    float timeTillPause = 0;
    float timeTillPauseLimit = 1.0f;

    bool PauseScreen;
    bool VictoryScreen;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        InputAction back = input.actions.FindAction("Cancel");
        Q = input.actions.FindAction("Q");
        E = input.actions.FindAction("E");
        Q.performed += DMGTEST;
        E.performed += DMGTEST;
        back.performed += pauseHold;
        back.canceled += pauseHold;
       // back.performed += QuitRefocus;
        //back.canceled += QuitEnd;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GS = GlobalSettingsSO.GS;
        P1Character = Instantiate(GS.character[GS.char1Id], P1Transform).GetComponent<CharacterPrefabCtrl>();
        P2Character = Instantiate(GS.character[GS.char2Id], P2Transform).GetComponent<CharacterPrefabCtrl>();
        P1Character.mesh.material = GS.skin[GS.char1SkinId];
        P2Character.mesh.material = GS.skin[GS.char2SkinId];
        Progress1.current = Progress1.maximum;
        Progress2.current = Progress2.maximum;
        ProgressPause.maximum = timeTillPauseLimit;
    }

    private void Update()
    {
        if (!PauseScreen && !VictoryScreen)
        {
            pauseBar();
            if (timeTillPause >= timeTillPauseLimit)
            {
                PauseScreen = true;
                PausePanel.SetActive(true);
                PausePanelResume.Select();
            }
        }
    }

    private void DMGTEST(InputAction.CallbackContext obj)
    {
        if(!(PauseScreen || VictoryScreen))
        {
            if (obj.action == Q)
            {
                Progress1.current -= 20;
                Progress1.getCurrentFill();
                if (Progress1.current <= 0)
                    Victory(true);

            }
            else
            {
                Progress2.current -= 20;
                Progress2.getCurrentFill();
                if (Progress2.current <= 0)
                    Victory(false);
            }
        }
    }

    private void pauseBar()
    {
        if (b_pausing)
        {

            timeTillPause += Time.deltaTime;
            ProgressPause.gameObject.SetActive(true);
            ProgressPause.current = timeTillPause;
            ProgressPause.getCurrentFill();
        }
        else
        {
            timeTillPause = 0;
            ProgressPause.gameObject.SetActive(false);
        }
            
    }
    private void pauseHold(InputAction.CallbackContext obj)
    {
        b_pausing = obj.performed;
    }
    private void Victory(bool p1)
    {
        Winner.text = p1 ? "p1 wins" : "p2 winds";
        VictoryScreen = true;
        VictoryPanel.SetActive(true);
        VictoryPanelRematch.Select();
    }

    public void CharSel()
    {
        GlobalSettingsSO.GS.currentScene = GlobalSettingsSO.CurrentScene.CHAR_SEL;
    }
    public void Quit()
    {
        GlobalSettingsSO.GS.currentScene = GlobalSettingsSO.CurrentScene.MAIN;
    }
    public void Rematch()
    {
        VictoryScreen = false;
        VictoryPanel.SetActive(false);
        Progress1.current = Progress2.current = Progress2.maximum;
        Progress1.getCurrentFill();
        Progress2.getCurrentFill();
    }
    public void Resumes()
    {
        PauseScreen = false;
        PausePanel.SetActive(false);
    }
}
