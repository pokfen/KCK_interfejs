using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectCtrl : MonoBehaviour
{
    public CharacterTile[] tiles;
    public short char1Id = -1;
    public short char2Id = -1;
    public short char1Skin = -1;
    public short char2Skin = -1;
    public short tempId;
    public Slider Slider1;
    public Slider Slider2;
    public GameObject ConfirmB;
    [SerializeField]
    private byte m_progression = 0;
    public byte progression
    {
        get { return m_progression; }
        set
        {
            ConfirmB.SetActive(false);
            Slider1.gameObject.SetActive(false);
            Slider2.gameObject.SetActive(false);
            switch (value)
            {
                case 3:
                    {
                        Slider2.gameObject.SetActive(true);
                        char2Skin = -1;
                        break;
                    }
                case 2:
                    {
                        char2Id = -1;
                        char2Skin = -1;
                        break;
                    }
                case 1:
                    {
                        Slider1.gameObject.SetActive(true);
                        char2Id = -1;
                        char1Skin = -1;
                        char2Skin = -1;
                        break;
                    }
                case 0:
                    {
                        char1Id = -1;
                        char2Id = -1;
                        char1Skin = -1;
                        char2Skin = -1;
                        break;
                    }
                default:
                    {
                        Debug.Log("Here need to swap scenes to Game");
                        GlobalSettingsSO.GS.currentScene = GlobalSettingsSO.CurrentScene.GAME;
                        break;
                    }
            }
            m_progression = value;
        }
    }
    public Transform plat1;
    public Transform plat2;
    public CharacterPrefabCtrl char1;
    public CharacterPrefabCtrl char2;
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
            char1.mesh.material = (GlobalSettingsSO.GS.skin[(int)Slider1.value]);
        }
    }
    public int skin2
    {
        get { return char2Skin; }
        set
        {
            ConfirmB.SetActive(true);
            char2.mesh.material = (GlobalSettingsSO.GS.skin[(int)Slider2.value]);
        }
    }
    public void PickCharacter(short id)
    {
        tempId = id;
        ConfirmB.SetActive(true);
        if (char1Id == -1)
        {
            if (char1 != null)
                Destroy(char1.gameObject);
            char1 = Instantiate(GlobalSettingsSO.GS.character[id], plat1).GetComponent<CharacterPrefabCtrl>();
        }
        else
        {
            if (char2Id == -1)
            {
                if (char2 != null)
                    Destroy(char2.gameObject);
                char2 = Instantiate(GlobalSettingsSO.GS.character[id], plat2).GetComponent<CharacterPrefabCtrl>();
            }
        }
    }
    public void Confirm()
    {
        if (char1Id == -1 && (char1 != null))
        {
            char1Id = tempId;
        }
        else
        {
            if (char2Id == -1 && (char2 != null))
            {
                char2Id = tempId;
            }
        }
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
}
