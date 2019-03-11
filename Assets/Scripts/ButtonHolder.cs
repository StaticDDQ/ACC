using UnityEngine;
using UnityEngine.UI;

public class ButtonHolder : MonoBehaviour {

    [SerializeField] private Text desc;
    [SerializeField] private BenchPlacement bench;
    [SerializeField] private GameController controller;
    private GameObject piece;
    private int price;

	public void AssignGeneratedPiece(GameObject pieceObj)
    {
        var details = pieceObj.GetComponent<PiecePosition>().GetLvl1Detail();
        piece = pieceObj;
        desc.text = details.effectDesc;
        GetComponent<Image>().sprite = details.portrait;
        price = details.price;
    }

    public void CreateBoughtPiece()
    {
        if(piece != null && controller.GetGold() >= price)
        {
            var obj = Instantiate(piece);
            bench.AlocatePiece(obj.GetComponent<PiecePosition>());

            piece = null;
            desc.text = "";
            GetComponent<Button>().image = null;
            controller.BuySellPiece(-price);
        }
    }
}
