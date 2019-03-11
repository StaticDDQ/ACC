using UnityEngine;

[CreateAssetMenu(fileName = "New Piece", menuName = "Piece")]
public class PieceDetail : ScriptableObject {

    public string pieceName;
    public string effectDesc;

    public Sprite portrait;
    public Mesh pieceModel;

    public int health;
    public int damage;
    public int armor;

    public int price;

    public float atkSpeed;
    public float moveSpeed;
}
