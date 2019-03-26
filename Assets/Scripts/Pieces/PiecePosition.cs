using UnityEngine;

public class PiecePosition : MonoBehaviour {

    [SerializeField] private PieceDetail lvl1;
    private Transform alocatedSpace;

    public void AssignSpace(Transform space)
    {
        if (alocatedSpace != null)
            alocatedSpace.GetComponent<PlaySpace>().AddPiece(null);
        alocatedSpace = space;
        alocatedSpace.GetComponent<PlaySpace>().AddPiece(gameObject);
    }

    public void RemovePiece()
    {
        if(alocatedSpace != null)
            alocatedSpace.GetComponent<PlaySpace>().AddPiece(null);
        Destroy(gameObject);
    }

    public void UpgradePiece()
    {
        Debug.Log("Has upgraded");
    }

    public PieceDetail GetPieceDetail()
    {
        return lvl1;
    }

    public string GetAlocatedSpaceTag()
    {
        return alocatedSpace.tag;
    }
}
