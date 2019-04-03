using UnityEngine;

public class RangeFinder : MonoBehaviour
{
    public PieceViewRange range;

    private void OnTriggerEnter(Collider other)
    {
        range.foundTarget = true;
    }

    private void OnTriggerExit(Collider other)
    {
        range.foundTarget = false;
    }
}
