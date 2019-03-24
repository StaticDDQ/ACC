using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparePhase : MonoBehaviour
{
    [SerializeField] private SpaceSelect spaceSelect;
    [SerializeField] private SpaceController playboard;
    [SerializeField] private BenchPlacement bench;
    [SerializeField] private GameController controller;
    [SerializeField] private Transform placeHolder;
    private List<Transform> activePieces;
    private bool hasEnemies = false;

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

        while (unitsToRemove > 0)
        {
            var removedUnit = activePieces[Random.Range(0, activePieces.Count - 1)];

            if (!bench.AlocatePiece(removedUnit))
            {
                int amount = removedUnit.GetComponent<PiecePosition>().GetPieceDetail().sellingPrice;
                controller.BuySellPiece(amount);
                removedUnit.GetComponent<PiecePosition>().RemovePiece();
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

    public void ReceiveEnemyUnits(List<Transform> enemies)
    {
        foreach (var enemy in enemies)
        {
            var enem = Instantiate(enemy, placeHolder);
            enem.localPosition = new Vector3(-enem.localPosition.x, enem.localPosition.y, -enem.localPosition.z);
            enem.tag = "enemyPiece";
            enem.GetComponent<PieceBehaviour>().enabled = true;
        }
    }

    public void CommenceBattle()
    {
        //DistributeUnits.instance.SendUnitsToRandomPlayer(activePieces);
        ReceiveEnemyUnits(activePieces);
        foreach(var units in activePieces)
        {
            units.GetComponent<PieceBehaviour>().enabled = true;
        }
    }

    public void CommencePrepare()
    {
        hasEnemies = false;
        spaceSelect.DisableUnitControl(false);
    }

    public bool GetHasEnemies()
    {
        return hasEnemies;
    }

    public void SetHasEnemies(bool newEnemies)
    {
        this.hasEnemies = newEnemies;
    }
}
