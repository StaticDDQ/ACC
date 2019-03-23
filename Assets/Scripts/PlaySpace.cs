using UnityEngine;

public class PlaySpace : MonoBehaviour {

    [SerializeField] private SpaceController controller;
    protected GameObject occupiedPiece;
    protected GameObject highlightPanel;

    protected void Start()
    {
        highlightPanel = transform.GetChild(0).gameObject;
    }

    // Assign piece to this space, afterwards notify parent if piece can be evolved
    public virtual void AddPiece(GameObject newPiece)
    {
        occupiedPiece = newPiece;
        if (occupiedPiece != null)
        {
            controller.AddUnit(1);
            controller.PieceControl(occupiedPiece);
        }
        else
            controller.AddUnit(-1);
    }

    public GameObject GetPiece()
    {
        return occupiedPiece;
    }

    protected void OnMouseEnter()
    {
        highlightPanel.SetActive(true);
    }

    protected void OnMouseExit()
    {
        highlightPanel.SetActive(false);  
    }
}
