using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceController : MonoBehaviour {

    [SerializeField] private Text unitText;
    private List<PlaySpace> spaces;
    private int currUnits = 0;
    private int maxUnits = 1;

    private void Start()
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

    public int ExcessUnits()
    {
        return currUnits - maxUnits;
    }

    public void AddUnit(int unitAmount)
    {
        currUnits += unitAmount;
        SetText();
    }

    public void AdjustMaxUnits(int newMax)
    {
        maxUnits = newMax;
        SetText();
    }

    private void SetText()
    {
        unitText.text = currUnits + "/" + maxUnits;
        if (currUnits > maxUnits)
            unitText.color = Color.red;
        else
            unitText.color = Color.white;
    }

    public List<PlaySpace> GetSpaces()
    {
        return this.spaces;
    }
}
