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

    public void PieceControl(GameObject piece)
    {
        bool noChanges = true;
        GameObject copy1 = null;

        foreach (PlaySpace space in spaces)
        {
            if (space.GetPiece() != null && space.GetPiece().name.Equals(piece.name) && space.GetPiece() != piece)
            {
                if (copy1 == null)
                {
                    copy1 = space.GetPiece();
                }
                else
                {
                    Destroy(copy1);
                    Destroy(space.GetPiece());
                    piece.GetComponent<PiecePosition>().UpgradePiece();
                    noChanges = false;
                    break;
                }
            }
        }

        if (!noChanges)
            PieceControl(piece);
    }

    public List<PlaySpace> GetSpaces()
    {
        return this.spaces;
    }
}
