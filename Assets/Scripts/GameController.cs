using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private SpaceController spaceController;
    [SerializeField] private ButtonHolder[] pieceSelectors;
    [SerializeField] private Text goldAmt;
    [SerializeField] private Text roundsTxt;
    [SerializeField] private Text levelTxt;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthText;
    [SerializeField] private Animator packsBoardAnim;

    private bool isLock = false;
    private bool displayPack = false;

    private int health = 100;

    private int level = 1;
    private int exp = 0;
    private int expCap = 3;
    private float expMod = 1.15f;

    private int gold = 0;
    private int rounds = 0;

    private void Start()
    {
        levelTxt.text = "1";
        healthText.text = "100%";

        NextRound();
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
            Reroll(true);
        } else if (Input.GetKeyDown(KeyCode.T))
        {
            GainEXP(true);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthBar.fillAmount = health / 100f;
        healthText.text = health + "%";
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void LockPacks()
    {
        isLock = !isLock;
    }

    public void Reroll(bool manualCommand)
    {
        if (!isLock || (manualCommand && gold >= 2))
        {
            SetPieceSelectors();
            if (!displayPack)
            {
                displayPack = true;
                packsBoardAnim.Play("DisplayPackBoard");
            }

            if(manualCommand)
                BuySellPiece(-2);
        }
    }

    private void SetPieceSelectors()
    {
        GameObject[] pack = PackGenerator.Instance.RequestPack(level);
        for (int i = 0; i < 5; i++)
        {
            pieceSelectors[i].AssignGeneratedPiece(pack[i]);
        }
    }

    public void BuySellPiece(int amount)
    {
        gold += amount;
        goldAmt.text = gold.ToString();
    }

    public void GainEXP(bool manualGain)
    {
        exp += 1;
        if (gold >= 5 && manualGain)
        {
            BuySellPiece(-5);
            exp += 4;
        }
        if(exp >= expCap)
        {
            exp -= expCap;
            level++;

            spaceController.AdjustMaxUnits(level);
            levelTxt.text = level + "";

            float t = Mathf.Pow(expMod, level);
            expCap = (int)Mathf.Floor(3 * t);
        }
    }

    public void NextRound()
    {
        roundsTxt.text = (++rounds).ToString();
        Reroll(false);

        if (rounds < 6)
            BuySellPiece(rounds);
        else
            BuySellPiece(5);

        if (rounds > 1)
            GainEXP(false);
    }

    public int GetGold()
    {
        return gold;
    }
}
