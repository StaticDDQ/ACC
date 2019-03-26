using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private FindTarget piece;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag.Equals("enemyPiece") && transform.parent.tag.Equals("playPiece") ||
             other.tag.Equals("playPiece") && transform.parent.tag.Equals("enemyPiece"))
             && piece.target == null)
        {
           piece.target = other.transform;
        }
    }
}
