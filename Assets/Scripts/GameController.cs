using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private ButtonHolder[] pieceSelectors;
    [SerializeField] private Text goldAmt;
    [SerializeField] private Text roundsTxt;
    [SerializeField] private Text maxUnitTxt;
    [SerializeField] private Animator packsBoardAnim;

    private bool displayPack = true;

    private int gold = 0;
    private int level = 1;
    private int rounds = 1;

    private int maxUnits = 1;
    private int currUnits = 0;

    private void Start()
    {
        maxUnitTxt.text = "0/1";

        BuySellPiece(1);

        SetPieceSelectors();

        packsBoardAnim.Play("DisplayPackBoard");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            displayPack = !displayPack;
            if (displayPack)
                packsBoardAnim.Play("DisplayPackBoard");
            else
                packsBoardAnim.Play("RemovePackBoard");
        } else if (Input.GetKeyDown(KeyCode.R) && gold >= 2)
        {
            SetPieceSelectors();
            if (!displayPack)
            {
                displayPack = true;
                packsBoardAnim.Play("DisplayPackBoard");
            }
            BuySellPiece(-2);
        } else if (Input.GetKeyDown(KeyCode.T) && gold >= 5)
        {
            GainEXP();
            BuySellPiece(-5);
        }
    }

    private void SetPieceSelectors()
    {
        GameObject[] pack = PackGenerator.instance.RequestPack(level);
        for (int i = 0; i < 5; i++)
        {
            pieceSelectors[i].AssignGeneratedPiece(pack[i]);
        }
    }

    public bool CanPlayUnit()
    {
        return currUnits < maxUnits;
    }

    public void SetPlayedUnits(int amount)
    {
        currUnits += amount;
        maxUnitTxt.text = currUnits + "/" + maxUnits;
    }

    public void BuySellPiece(int amount)
    {
        gold += amount;
        goldAmt.text = gold.ToString();
    }

    private void GainEXP()
    {

    }

    public void NextRound()
    {
        roundsTxt.text = (rounds++).ToString();

    }

    public int GetGold()
    {
        return gold;
    }
}
