using UnityEngine;

public class SpaceSelect : MonoBehaviour {

    private Camera cam;

    [SerializeField] private BenchPlacement bench;
    [SerializeField] private GameController controller;
    [SerializeField] private PieceInformation informer;
   
    private bool controlsDisabled = false;

    private bool pieceSelected = false;
    private Transform selectedPiece;

    private SelectPiece selection = SelectPiece.Idling;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            Return();
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            Move();
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            Sell();
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            selection = SelectPiece.Idling;
            informer.RemoveDisplay();
        }

        RaycastHit hitInfo = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                string hitTag = hitInfo.transform.tag;

                if ((hitTag.Equals("piece") || hitTag.Equals("playPiece")) && selection == SelectPiece.Idling)
                {
                    informer.DisplayPiece(hitInfo.transform.GetComponent<PiecePosition>().GetPieceDetail());
                }
                else if (hitTag.Equals("piece") && !pieceSelected)
                {
                    ManipulatePiece(hitInfo.transform);
                }
                else if (pieceSelected && (hitTag.Equals("space") || hitTag.Equals("benchSpace")))
                {
                    UseSpace(hitInfo.transform);
                }
            }
        }
    }

    private void ManipulatePiece(Transform piece)
    {
        PiecePosition pieceScript = piece.GetComponent<PiecePosition>();
        if (selection == SelectPiece.Returning)
        {
            if (!pieceScript.GetAlocatedSpaceTag().Equals("benchSpace"))
            {
                bench.AlocatePiece(piece);
            }
        }
        else if (selection == SelectPiece.Moving)
        {
            selectedPiece = piece;
            pieceSelected = true;
        }
        else if (selection == SelectPiece.Selling)
        {
            int amount = pieceScript.GetPieceDetail().sellingPrice;
            controller.BuySellPiece(amount);
            pieceScript.RemovePiece();
        }
        selection = SelectPiece.Idling;
    }

    private void UseSpace(Transform space)
    {
        if (space.GetComponent<PlaySpace>().GetPiece() == null)
        {
            selectedPiece.GetComponent<PiecePosition>().AssignSpace(space);
            selectedPiece.position = space.transform.position + new Vector3(0, 1f, 0);

            pieceSelected = false;
            selectedPiece = null;
        }
    }

    public void Move()
    {
        selection = SelectPiece.Moving;
    }

    public void Return()
    {
        selection = SelectPiece.Returning;
    }

    public void Sell()
    {
        selection = SelectPiece.Selling;
    }

    public void DisableUnitControl(bool isDisabled)
    {
        controlsDisabled = isDisabled;
        if (controlsDisabled)
        {
            selectedPiece = null;
            pieceSelected = false;
        }
    }
}
