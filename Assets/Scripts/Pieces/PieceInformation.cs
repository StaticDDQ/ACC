using UnityEngine;
using UnityEngine.UI;

public class PieceInformation : MonoBehaviour
{
    [SerializeField] private Image spriteImg;
    [SerializeField] private Image ultimateIcon;
    [SerializeField] private Text attack;
    [SerializeField] private Text armor;
    [SerializeField] private Text attackSpeed;
    private bool isDisplayed = false;

    public void DisplayPiece(PieceDetail detail)
    {
        spriteImg.sprite = detail.portrait;
        ultimateIcon.sprite = detail.ultimateIcon;
        attack.text = detail.damage.ToString();
        armor.text = detail.armor.ToString();
        attackSpeed.text = detail.atkSpeed.ToString();

        if (!isDisplayed)
        {
            GetComponent<Animator>().Play("DisplayUnitInfo");
            isDisplayed = true;
        }
    }

    public void RemoveDisplay()
    {
        if (isDisplayed)
        {
            GetComponent<Animator>().Play("RemoveUnitInfo");
            isDisplayed = false;
        }
    }
}
