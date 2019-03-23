using UnityEngine;

public class BenchSpace : PlaySpace
{
    // Assign piece to this space, afterwards notify parent if piece can be evolved
    public override void AddPiece(GameObject newPiece)
    {
        occupiedPiece = newPiece;
    }
}
