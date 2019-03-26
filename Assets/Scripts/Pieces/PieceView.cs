using UnityEngine;

public class PieceView : MonoBehaviour
{
    public bool isOccupied = false;
    public bool foundTarget = false;
    private void OnTriggerEnter(Collider other)
    {
        isOccupied = true;
        if ((other.tag.Equals("enemyPiece") && transform.parent.tag.Equals("playPiece")) || 
           (other.tag.Equals("playPiece") && transform.parent.tag.Equals("enemyPiece")))
        {
            foundTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isOccupied = false;
        foundTarget = false;
    }
}
