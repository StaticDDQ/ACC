using UnityEngine;
using System.Collections.Generic;

public enum Phase { Build, Prepare, Play }

public class PhaseController : MonoBehaviour
{
    public static PhaseController instance;
    private Phase currPhase = Phase.Build;
    [SerializeField] private List<PreparePhase> players;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void NextPhase(Phase nextPhase)
    {
        switch(nextPhase)
        {
            case Phase.Prepare:
                foreach(PreparePhase player in players)
                {
                    StartCoroutine(player.CommencePreparation());
                }
                break;
            case Phase.Play:
                foreach (PreparePhase player in players)
                {
                    player.CommencePlay();
                }
                break;
            case Phase.Build:
                foreach (PreparePhase player in players)
                {
                    player.CommenceBuild();
                }
                break;
        }
        currPhase = nextPhase;
    }
}
