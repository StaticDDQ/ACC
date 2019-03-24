using UnityEngine;

public class PiecePosition : MonoBehaviour {

    [SerializeField] private PieceDetail lvl1;
    private Transform alocatedSpace;
    private ParticleSystem system;

    private void Start()
    {
        system = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void AssignSpace(Transform space)
    {
        if (alocatedSpace != null)
            alocatedSpace.GetComponent<PlaySpace>().AddPiece(null);
        alocatedSpace = space;
        alocatedSpace.GetComponent<PlaySpace>().AddPiece(gameObject);
    }

    public void RemovePiece()
    {
        if(alocatedSpace != null)
            alocatedSpace.GetComponent<PlaySpace>().AddPiece(null);
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        system.Play();
    }

    private void OnMouseExit()
    {
        system.Stop();
    }

    public void UpgradePiece()
    {
        Debug.Log("Has upgraded");
    }

    public PieceDetail GetPieceDetail()
    {
        return lvl1;
    }

    public string GetAlocatedSpaceTag()
    {
        return alocatedSpace.tag;
    }
}
