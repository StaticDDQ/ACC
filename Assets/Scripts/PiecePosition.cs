using UnityEngine;

public class PiecePosition : MonoBehaviour {

    [SerializeField] private PieceDetail lvl1;
    [SerializeField] private PieceDetail lvl2;
    [SerializeField] private PieceDetail lvl3;
    [SerializeField] private PlaySpace alocatedSpace;
    [SerializeField] private string pieceName;

    public bool RelocatePiece(Transform newSpace)
    {
        if (newSpace.GetComponent<PlaySpace>().GetPiece() != null)
            return false;

        if (alocatedSpace != null)
        {
            if (newSpace.tag.Equals("benchSpace") && alocatedSpace.tag.Equals("benchSpace"))
                return false;

            alocatedSpace.AddPiece(null);
        }

        transform.position = newSpace.position;
        alocatedSpace = newSpace.GetComponent<PlaySpace>();
        alocatedSpace.AddPiece(this);

        return true;
    }

    public void UpgradePiece()
    {
        Debug.Log("Has upgraded");
    }

    public int GetSellingPrice()
    {
        return 1;
    }

    public void RemovePiece()
    {
        alocatedSpace.AddPiece(null);
        Destroy(this.gameObject);
    }

    public string GetName()
    {
        return this.pieceName;
    }

    public PieceDetail GetLvl1Detail()
    {
        return this.lvl1;
    }

    public string GetAlocatedSpaceTag()
    {
        return alocatedSpace.tag;
    }
}
