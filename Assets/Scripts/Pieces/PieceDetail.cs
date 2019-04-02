using UnityEngine;

[CreateAssetMenu(fileName = "New Piece", menuName = "Piece")]
public class PieceDetail : ScriptableObject {

    public string pieceName;
    public string effectDesc;

    public Sprite ultimateIcon;
    public Sprite portrait;
    public Mesh pieceModel;

    public float health;
    public float damage;
    public int armor;

    public int price;
    public int sellingPrice;

    public float atkSpeed;
}
