using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparePhase : MonoBehaviour
{
    [SerializeField] private SpaceController playboard;
    [SerializeField] private BenchPlacement bench;
    [SerializeField] private GameController controller;
    private List<GameObject> activePieces;

    public void CommencePreparation()
    {
        List<PlaySpace> spaces = playboard.GetSpaces();
        activePieces = new List<GameObject>();

        foreach (PlaySpace space in spaces)
        {
            if (space.GetPiece() != null)
                activePieces.Add(space.GetPiece());
        }

        int unitsToRemove = controller.ExcessUnits();
        if(unitsToRemove > 0)
        { 
            while(unitsToRemove != 0)
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
                activePieces.Remove(removedUnit.gameObject);
                unitsToRemove--;
            }
        }

        foreach (GameObject piece in activePieces)
        {
            piece.tag = "Untagged";
        }
    }
}
