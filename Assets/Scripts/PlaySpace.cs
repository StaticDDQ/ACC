using UnityEngine;

public class PlaySpace : MonoBehaviour {

    private SpaceController controller;
    [SerializeField] private GameObject occupiedPiece;
    private GameObject highlightPanel;

    private void Start()
    {
        highlightPanel = transform.GetChild(0).gameObject;
        controller = transform.parent.GetComponent<SpaceController>();
    }

    // Assign piece to this space, afterwards notify parent if piece can be evolved
    public void AddPiece(GameObject newPiece)
    {
        occupiedPiece = newPiece;

        if (occupiedPiece != null)
        {
            controller.PieceControl(occupiedPiece);
        }
    }

    public GameObject GetPiece()
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
