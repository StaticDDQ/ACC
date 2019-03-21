using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparePhase : MonoBehaviour
{
    [SerializeField] private SpaceSelect spaceSelect;
    [SerializeField] private SpaceController playboard;
    [SerializeField] private BenchPlacement bench;
    [SerializeField] private GameController controller;
    private List<GameObject> activePieces;

    public IEnumerator CommencePreparation()
    {
        spaceSelect.DisableUnitControl(false);

        List<PlaySpace> spaces = playboard.GetSpaces();
        activePieces = new List<GameObject>();

        foreach (PlaySpace space in spaces)
        {
            if (space.GetPiece() != null)
                activePieces.Add(space.GetPiece());
        }

        foreach (GameObject piece in activePieces)
        {
            piece.tag = "Untagged";
        }

        yield return new WaitForSeconds(1);

        int unitsToRemove = controller.ExcessUnits();

        while(unitsToRemove > 0)
        {
            var removedUnit = activePieces[Random.Range(0, activePieces.Count - 1)].GetComponent<PiecePosition>();
                
            if (!bench.AlocatePiece(removedUnit))
            {
                if (removedUnit.GetAlocatedSpaceTag().Equals("space"))
                    controller.SetPlayedUnits(-1);

                int amount = removedUnit.GetSellingPrice();
                controller.BuySellPiece(amount);
                Destroy(removedUnit.gameObject);
            }
            else
            {
                removedUnit.gameObject.tag = "piece";
            }
            activePieces.Remove(removedUnit.gameObject);
            unitsToRemove--;

            yield return null;
        }
    }

    public void CommencePlay()
    {

    }

    public void CommenceBuild()
    {
        spaceSelect.DisableUnitControl(true);
    }
}
