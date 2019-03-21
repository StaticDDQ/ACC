﻿using UnityEngine;

public class SpaceSelect : MonoBehaviour {

    [SerializeField] private Camera cam;
    [SerializeField] private BenchPlacement bench;
    private bool canControl = true;
    private GameController controller;
    private bool pieceSelected = false;
    private Transform selectedPiece;

    private SelectPiece selection = SelectPiece.Idling;

    private void Start()
    {
        controller = this.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            Return();
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            Move();
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            Sell();
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            selection = SelectPiece.Idling;
        }

        RaycastHit hitInfo = new RaycastHit();
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                string hitTag = hitInfo.transform.tag;

                if (hitTag.Equals("piece") && !pieceSelected)
                {
                    ManipulatePiece(hitInfo.transform);
                }
                else if (pieceSelected && hitTag.Equals("space"))
                {
                    UseSpace(hitInfo.transform);
                }
                else if (pieceSelected && hitTag.Equals("benchSpace"))
                {
                    UseBench(hitInfo.transform);
                }
            }
        }
    }

    private void ManipulatePiece(Transform piece)
    {
        PiecePosition pieceScript = piece.transform.GetComponent<PiecePosition>();
        if (selection == SelectPiece.Returning && canControl)
        {
            if (piece.GetComponent<PiecePosition>().GetAlocatedSpaceTag().Equals("space"))
            {
                controller.SetPlayedUnits(-1);
            }
            bench.AlocatePiece(pieceScript);
        }
        else if (selection == SelectPiece.Moving && canControl)
        {
            selectedPiece = piece;
            pieceSelected = true;
        }
        else if (selection == SelectPiece.Selling)
        {
            if (pieceScript.GetAlocatedSpaceTag().Equals("space"))
                controller.SetPlayedUnits(-1);

            int amount = pieceScript.GetSellingPrice();
            controller.BuySellPiece(amount);
            Destroy(piece.gameObject);

        }
        selection = SelectPiece.Idling;
    }

    private void UseBench(Transform bench)
    {
        if (selectedPiece.GetComponent<PiecePosition>().RelocatePiece(bench))
        {
            pieceSelected = false;
            selectedPiece = null;

            string lastPlacedSpace = selectedPiece.GetComponent<PiecePosition>().GetAlocatedSpaceTag();

            if (lastPlacedSpace.Equals("space"))
                controller.SetPlayedUnits(-1);
        }
    }

    private void UseSpace(Transform space)
    {
        string lastPlacedSpace = selectedPiece.GetComponent<PiecePosition>().GetAlocatedSpaceTag();

        if (selectedPiece.GetComponent<PiecePosition>().RelocatePiece(space))
        {
            pieceSelected = false;
            selectedPiece = null;

            if (lastPlacedSpace.Equals("benchSpace"))
                controller.SetPlayedUnits(1);
        }
    }

    public void Move()
    {
        selection = SelectPiece.Moving;
    }

    public void Return()
    {
        selection = SelectPiece.Returning;
    }

    public void Sell()
    {
        selection = SelectPiece.Selling;
    }

    public void DisableUnitControl(bool isDisabled)
    {
        canControl = isDisabled;
        if (!canControl)
        {
            selectedPiece = null;
            pieceSelected = false;
        }
    }
}
