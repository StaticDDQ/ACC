using UnityEngine;

public class PieceView : PieceViewRange
{
    public bool isDiagonal = false;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if ((other.tag.Equals("enemyPiece") && transform.parent.tag.Equals("playPiece")) || 
           (other.tag.Equals("playPiece") && transform.parent.tag.Equals("enemyPiece")) && !isDiagonal)
        {
            foundTarget = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        foundTarget = false;
    }
}
