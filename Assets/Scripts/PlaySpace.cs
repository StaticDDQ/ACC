using UnityEngine;

public class PlaySpace : MonoBehaviour {

    [SerializeField] private SpaceController controller;
    protected GameObject occupiedPiece;
    protected Material mat;
    [SerializeField] protected Color newColor;
    protected Color oriColor;

    protected void Start()
    {
        mat = GetComponent<Renderer>().material;
        oriColor = mat.color;
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
        mat.color = newColor;
    }

    protected void OnMouseExit()
    {
        mat.color = oriColor;
    }
}
