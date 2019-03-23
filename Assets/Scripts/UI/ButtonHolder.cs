using UnityEngine;
using UnityEngine.UI;

public class ButtonHolder : MonoBehaviour {

    [SerializeField] private Text desc;
    [SerializeField] private BenchPlacement bench;
    [SerializeField] private GameController controller;
    [SerializeField] private Transform placeHolder;
    private GameObject piece;
    private int price;

	public void AssignGeneratedPiece(GameObject pieceObj)
    {
        var details = pieceObj.GetComponent<PiecePosition>().GetPieceDetail();
        piece = pieceObj;
        desc.text = details.effectDesc;
        GetComponent<Image>().sprite = details.portrait;
        price = details.price;
    }

    public void CreateBoughtPiece()
    {
        if(piece != null && controller.GetGold() >= price)
        {
            var obj = Instantiate(piece, placeHolder);

            if (bench.AlocatePiece(obj.transform))
            {
                piece = null;
                desc.text = "";
                GetComponent<Image>().sprite = null;
                controller.BuySellPiece(-price);
            } else
            {
                Destroy(obj);
            }
        }
    }
}
