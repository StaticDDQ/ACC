using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private PhaseController phaseController;
    [SerializeField] private Text clockText;
    [SerializeField] private Text phaseText;

    private float minTimer = 30.0f;
    private Phase currPhase = Phase.Prepare;

    private float timer;

    private void Awake()
    {
        timer = minTimer;
        phaseController = GetComponent<PhaseController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            clockText.text = ((int)timer).ToString();
        } else if(timer < 0.0f)
        {
            clockText.text = "0";
            NextPhase();
        }
    }

    public void NextPhase()
    {
        switch (currPhase)
        {
            case Phase.Prepare:
                phaseText.text = "Ready";
                phaseText.color = Color.yellow;
                currPhase = Phase.Ready;
                ResetTimer(6.0f);
                break;
            case Phase.Ready:
                phaseText.text = "Battle";
                phaseText.color = Color.red;
                currPhase = Phase.Battle;
                ResetTimer(61.0f);
                break;
            case Phase.Battle:
                phaseText.text = "Prepare";
                phaseText.color = Color.green;
                currPhase = Phase.Prepare;
                ResetTimer(31.0f);
                break;
        }
        phaseController.NextPhase(currPhase);
    }

    private void ResetTimer(float newTimer)
    {
        timer = newTimer;
    }
}
