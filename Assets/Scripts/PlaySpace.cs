using UnityEngine;

public class PlaySpace : MonoBehaviour {

    [SerializeField] private SpaceController controller;
    [SerializeField] private GameObject occupiedPiece;
    private GameObject highlightPanel;

    private void Start()
    {
        highlightPanel = transform.GetChild(0).gameObject;
    }

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
