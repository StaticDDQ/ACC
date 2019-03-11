using UnityEngine;

public class SpaceSelect : MonoBehaviour {

    [SerializeField] private BenchPlacement bench;
    private GameController controller;
    private bool pieceSelected = false;
    private Transform selectedPiece;

    private SelectPiece selection = SelectPiece.Idling;

    private void Start()
    {
        controller = this.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {
            selection = SelectPiece.Returning;
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            selection = SelectPiece.Moving;
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            selection = SelectPiece.Selling;
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            selection = SelectPiece.Idling;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                string hitTag = hitInfo.transform.tag;
                if (hitTag.Equals("piece") && !pieceSelected)
                {
                    if (selection == SelectPiece.Returning)
                    {
                        bench.AlocatePiece(hitInfo.transform.GetComponent<PiecePosition>());
                    } else if (selection == SelectPiece.Moving)
                    {
                        selectedPiece = hitInfo.transform;
                        pieceSelected = true;
                    } else if (selection == SelectPiece.Selling)
                    {
                        if (hitInfo.transform.GetComponent<PiecePosition>().GetAlocatedSpaceTag().Equals("space"))
                            controller.SetPlayedUnits(-1);

                        int amount = hitInfo.transform.GetComponent<PiecePosition>().GetSellingPrice();
                        controller.BuySellPiece(amount);
                        Destroy(hitInfo.transform.gameObject);

                    }
                    selection = SelectPiece.Idling;

                } else if(pieceSelected && ((hitTag.Equals("space") && controller.CanPlayUnit()) || hitTag.Equals("benchSpace")))
                {
                    string lastPlacedSpace = selectedPiece.GetComponent<PiecePosition>().GetAlocatedSpaceTag();

                    if(hitTag.Equals("space") && selectedPiece.GetComponent<PiecePosition>().RelocatePiece(hitInfo.transform))
                    {
                        pieceSelected = false;
                        selectedPiece = null;

                        if(lastPlacedSpace.Equals("benchSpace"))
                            controller.SetPlayedUnits(1);
                    } else if(hitTag.Equals("benchSpace") && selectedPiece.GetComponent<PiecePosition>().RelocatePiece(hitInfo.transform))
                    {
                        pieceSelected = false;
                        selectedPiece = null;

                        if(lastPlacedSpace.Equals("space"))
                            controller.SetPlayedUnits(-1);
                    }
                    
                } 
            }
        }
	}
}
