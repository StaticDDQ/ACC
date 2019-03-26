using System.Collections.Generic;
using UnityEngine;

public class BenchPlacement : MonoBehaviour {

    [SerializeField] private List<PlaySpace> spaces;

    public bool AlocatePiece(Transform piece)
    {
        foreach  (PlaySpace bench in spaces)
        {
            if(bench.GetPiece() == null)
            {
                piece.GetComponent<PiecePosition>().AssignSpace(bench.transform);
                piece.position = bench.transform.position + new Vector3(0, 1f, 0);
                return true;
            }
        }
        return false;
    }
}
