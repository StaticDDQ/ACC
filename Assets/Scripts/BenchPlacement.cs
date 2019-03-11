using UnityEngine;

public class BenchPlacement : SpaceController {

    public void AlocatePiece(PiecePosition piece)
    {
        foreach  (PlaySpace bench in spaces)
        {
            if(bench.GetPiece() == null)
            {
                piece.RelocatePiece(bench.transform);
                return;
            }
        }
    }
}
