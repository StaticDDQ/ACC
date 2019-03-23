﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparePhase : MonoBehaviour
{
    [SerializeField] private SpaceSelect spaceSelect;
    [SerializeField] private SpaceController playboard;
    [SerializeField] private BenchPlacement bench;
    [SerializeField] private GameController controller;
    private List<Transform> activePieces;

    public IEnumerator CommenceReady()
    {
        spaceSelect.DisableUnitControl(true);

        List<PlaySpace> spaces = playboard.GetSpaces();
        activePieces = new List<Transform>();

        foreach (PlaySpace space in spaces)
        {
            if (space.GetPiece() != null)
                activePieces.Add(space.GetPiece().transform);
        }

        foreach (Transform piece in activePieces)
        {
            piece.tag = "Untagged";
        }

        yield return new WaitForSeconds(1);

        int unitsToRemove = playboard.ExcessUnits();

        while(unitsToRemove > 0)
        {
            var removedUnit = activePieces[Random.Range(0, activePieces.Count - 1)];
                
            if (!bench.AlocatePiece(removedUnit))
            {
                removedUnit.GetComponent<PiecePosition>().AssignSpace(null);

                int amount = removedUnit.GetComponent<PiecePosition>().GetPieceDetail().sellingPrice;
                controller.BuySellPiece(amount);
                Destroy(removedUnit.gameObject);
            }
            else
            {
                removedUnit.gameObject.tag = "piece";
            }
            activePieces.Remove(removedUnit);
            unitsToRemove--;

            yield return null;
        }
    }

    public void CommenceBattle()
    {

    }

    public void CommencePrepare()
    {
        spaceSelect.DisableUnitControl(false);
    }
}
