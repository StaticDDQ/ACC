using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private ButtonHolder[] pieceSelectors;
    [SerializeField] private Text goldAmt;
    [SerializeField] private Text roundsTxt;
    [SerializeField] private Text maxUnitTxt;
    [SerializeField] private Text levelTxt;
    [SerializeField] private Animator packsBoardAnim;

    private bool isLock = false;
    private bool displayPack = true;

    private int level = 1;
    private int exp = 0;
    private int expCap = 3;
    private float expMod = 1.15f;

    private int gold = 0;
    private int rounds = 1;

    private int currUnits = 0;

    private void Start()
    {
        levelTxt.text = "1";
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
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            Reroll();
        } else if (Input.GetKeyDown(KeyCode.T))
        {
            GainEXP(true);
        }
    }

    public void LockPacks()
    {
        isLock = !isLock;
    }

    public void Reroll()
    {
        if (!isLock && gold >= 2)
        {
            SetPieceSelectors();
            if (!displayPack)
            {
                displayPack = true;
                packsBoardAnim.Play("DisplayPackBoard");
            }
            BuySellPiece(-2);
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
        return currUnits < level;
    }

    public void SetPlayedUnits(int amount)
    {
        currUnits += amount;
        maxUnitTxt.text = currUnits + "/" + level;
    }

    public void BuySellPiece(int amount)
    {
        gold += amount;
        goldAmt.text = gold.ToString();
    }

    public void GainEXP(bool manualGain)
    {
        if(gold >= 5 && manualGain)
        {
            BuySellPiece(-5);
        }
        exp += 5;
        if(exp >= expCap)
        {
            exp -= expCap;
            level++;
            float t = Mathf.Pow(expMod, level);
            expCap = (int)Mathf.Floor(3 * t);
        }

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
