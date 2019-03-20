
public class BenchPlacement : SpaceController {

    public bool AlocatePiece(PiecePosition piece)
    {
        foreach  (PlaySpace bench in spaces)
        {
            if(bench.GetPiece() == null)
            {
                piece.RelocatePiece(bench.transform);
                return true;
            }
        }
        return false;
    }
}
