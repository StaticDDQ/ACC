using UnityEngine;

public enum Phase { Prepare, Ready, Battle }

public class PhaseController : MonoBehaviour
{
    private PreparePhase phase;

    private void Start()
    {
        phase = GetComponent<PreparePhase>();
    }

    public void NextPhase(Phase nextPhase)
    {
        switch(nextPhase)
        {
            case Phase.Ready:
                StartCoroutine(phase.CommenceReady());
                break;
            case Phase.Battle:
                phase.CommenceBattle();
                break;
            case Phase.Prepare:
                phase.CommencePrepare();
                break;
        }
    }
}
