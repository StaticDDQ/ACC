using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour {

    protected List<PlaySpace> spaces;

    protected void Start()
    {
        spaces = new List<PlaySpace>();

        foreach  (Transform child in transform)
        {
            if (child.GetComponent<PlaySpace>() != null)
            {
                spaces.Add(child.GetComponent<PlaySpace>());
            }
        }
    }

    public void PieceControl(PiecePosition piece)
    {
        bool noChanges = true;
        PiecePosition copy1 = null;

        foreach (PlaySpace space in spaces)
        {
            if (space.GetPiece() != null && space.GetPiece().GetName().Equals(piece.GetName()) && space.GetPiece() != piece)
            {
                if (copy1 == null)
                {
                    copy1 = space.GetPiece();
                }
                else
                {
                    copy1.RemovePiece();
                    space.GetPiece().RemovePiece();
                    piece.UpgradePiece();
                    noChanges = false;
                    break;
                }
            }
        }

        if (!noChanges)
            PieceControl(piece);
    }
}
