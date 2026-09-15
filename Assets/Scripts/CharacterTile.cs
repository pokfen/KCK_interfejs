using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class CharacterTile : MonoBehaviour
{
    public short TileID;
    public CharacterSelectCtrl parent;
    public Image im;
    public void ForwardChoice()
    {
        parent.PickCharacter(TileID);
    }
}
