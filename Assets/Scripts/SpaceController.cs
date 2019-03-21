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
            var activePiece = space.GetPiece();

            if (activePiece != null && activePiece.name.Equals(piece.name) && activePiece != piece)
            {
                if (copy1 == null)
                {
                    copy1 = activePiece;
                }
                else
                {
                    Destroy(copy1);
                    Destroy(activePiece);
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
