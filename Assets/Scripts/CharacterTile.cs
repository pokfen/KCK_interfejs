using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterTile : MonoBehaviour
{
    public short TileID;
    public CharacterSelectCtrl parent;
    public void ForwardChoice()
    {
        parent.check(TileID);
    }
}
