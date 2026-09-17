using UnityEngine;
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
