using UnityEngine;

public class PlaySpace : MonoBehaviour {

    [SerializeField] private SpaceController controller;
    [SerializeField] private PiecePosition occupiedPiece;

	public void AddPiece(PiecePosition newPiece)
    {
        occupiedPiece = newPiece;

        if (occupiedPiece != null)
        {
            controller.PieceControl(occupiedPiece);
        }
    }

    public PiecePosition GetPiece()
    {
        return this.occupiedPiece;
    }
}
