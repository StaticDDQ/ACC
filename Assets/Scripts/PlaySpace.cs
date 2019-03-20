using UnityEngine;

public class PlaySpace : MonoBehaviour {

    [SerializeField] private SpaceController controller;
    [SerializeField] private PiecePosition occupiedPiece;
    private GameObject highlightPanel;

    private void Start()
    {
        highlightPanel = transform.GetChild(0).gameObject;
    }

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
        return occupiedPiece;
    }

    private void OnMouseEnter()
    {
        highlightPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlightPanel.SetActive(false);  
    }
}
