using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectCtrl : MonoBehaviour
{
    public CharacterTile[] tiles;
    public Slider Slider1;
    public Slider Slider2;
    public GameObject ConfirmB;
    public Transform plat1;
    public Transform plat2;
    [Header("AutoInitializable")]
    public CharacterPrefabCtrl char1Prefab;
    public CharacterPrefabCtrl char2Prefab;
    public short char1Id = -1;
    public short char2Id = -1;
    public short char1Skin = -1;
    public short char2Skin = -1;
    [SerializeField]
    private byte m_progression = 0;
    public byte progression
    {
        get { return m_progression; }
        set
        {
            Slider1.gameObject.SetActive(false);
            Slider2.gameObject.SetActive(false);
            switch (value)
            {
                case 3:
                    {
                        Slider2.gameObject.SetActive(true);
                        break;
                    }
                case 2:
                    {
                        if (char2Prefab != null)
                        {
                            char2Skin = ChromaCorrection(true);
                            Slider2.value = char2Skin;
                            char2Prefab.mesh.material = (GlobalSettingsSO.GS.skin[char2Skin]);
                        }
                        else
                            ConfirmB.gameObject.SetActive(false);
                        break;
                    }
                case 1:
                    {
                        Slider1.gameObject.SetActive(true);
                        char2Id = -1;
                        char2Skin = -1;
                        if (char2Prefab != null)
                            Destroy(char2Prefab.gameObject);
                        break;
                    }
                case 0:
                    {
                        char2Id = -1;
                        char2Skin = -1;
                        if (char1Prefab != null)
                        {
                            char1Skin = ChromaCorrection(true);
                            Slider1.value = char1Skin;
                            char1Prefab.mesh.material = (GlobalSettingsSO.GS.skin[char1Skin]);
                        }
                        else
                            ConfirmB.gameObject.SetActive(false);
                        break;
                    }
                default:
                    {
                        Debug.Log("Here need to swap scenes to Game");
                        GlobalSettingsSO GS = GlobalSettingsSO.GS;
                        GS.char1Id = char1Id;
                        GS.char2Id = char2Id;
                        GS.char1SkinId = char1Skin;
                        GS.char2SkinId = char2Skin;
                        GS.currentScene = GlobalSettingsSO.CurrentScene.GAME;
                        break;
                    }
            }
            m_progression = value;
        }
    }
    private void Start()
    {
        tiles[0].im.sprite = GlobalSettingsSO.GS.charSelSprites[0];
        tiles[1].im.sprite = GlobalSettingsSO.GS.charSelSprites[1];
        tiles[2].im.sprite = GlobalSettingsSO.GS.charSelSprites[2];
        progression = 0;
    }

    public int skin1 {
        get { return char1Skin; }
        set {
            ConfirmB.SetActive(true);
            Slider1.value = char1Skin = ChromaCorrection(true, (short)Slider1.value);
            char1Prefab.mesh.material = (GlobalSettingsSO.GS.skin[char1Skin]);

        }
    }
    public int skin2
    {
        get { return char2Skin; }
        set
        {
            ConfirmB.SetActive(true);
            Slider2.value = char2Skin = ChromaCorrection(true, (short)Slider2.value);
            char2Prefab.mesh.material = (GlobalSettingsSO.GS.skin[char2Skin]);
        }
    }
    public void PickCharacter(short id)
    {
        if (progression % 2 == 1)
        {
            return;
        }
        if (progression < 2)
        {
            char1Id = id;
            if (char1Id >= 0)
            {
                if (char1Prefab != null)
                    Destroy(char1Prefab.gameObject);
                char1Prefab = Instantiate(GlobalSettingsSO.GS.character[id], plat1).GetComponent<CharacterPrefabCtrl>();
                Slider1.value = char1Skin = ChromaCorrection(true);
                char1Prefab.mesh.material = (GlobalSettingsSO.GS.skin[char1Skin]);
                ConfirmB.SetActive(true);
            }
        }
        else
        {
            char2Id = id;
            if (char2Id >= 0)
            {
                if (char2Prefab != null)
                    Destroy(char2Prefab.gameObject);
                char2Prefab = Instantiate(GlobalSettingsSO.GS.character[id], plat2).GetComponent<CharacterPrefabCtrl>();
                Slider2.value = char2Skin = ChromaCorrection(true);
                char2Prefab.mesh.material = (GlobalSettingsSO.GS.skin[char2Skin]);
                ConfirmB.SetActive(true);
            }
        }
    }
    public void Confirm()
    {
        progression++;
    }
    public void GoBack()
    {
        if(progression>0)
            progression--;
        else
        {
            Debug.Log("Here need to swap scenes to Main");
            GlobalSettingsSO.GS.currentScene = GlobalSettingsSO.CurrentScene.MAIN;
        }
    }

    public short ChromaCorrection(bool player1WasFirst,short test = 0)
    {
        if(player1WasFirst)
        {
            if (char1Id == char2Id & char1Skin == test)
                return (short)((test + 1) % GlobalSettingsSO.GS.skin.Length);
            return (test);
        }
        else
        {
            if (char1Id == char2Id & char2Skin == test)
                return (short)((test + 1) % GlobalSettingsSO.GS.skin.Length);
            return (test);
        }

    }
}
