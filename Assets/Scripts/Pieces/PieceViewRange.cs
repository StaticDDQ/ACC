using UnityEngine;

public class PieceViewRange : MonoBehaviour
{
    public bool isOccupied = false;
    public bool foundTarget = false;

    protected virtual void OnTriggerEnter(Collider other)
    {
        isOccupied = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        isOccupied = false;
    }
}
