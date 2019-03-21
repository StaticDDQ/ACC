using UnityEngine;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;

    [SerializeField] private float minTimer;
    private Phase currPhase = Phase.Build;

    public float timer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        timer = minTimer;
    }

    // Update is called once per frame
    private void Update()
    {
        if(timer >= 0.0f)
        {
            timer -= Time.deltaTime;
        } else if(timer < 0.0f)
        {
            timer = 0.0f;

            switch (currPhase)
            {
                case Phase.Build:
                    currPhase = Phase.Prepare;
                    ResetTimer(5.0f);
                    break;
                case Phase.Prepare:
                    currPhase = Phase.Play;
                    ResetTimer(30.0f);
                    break;
                case Phase.Play:
                    currPhase = Phase.Build;
                    ResetTimer(30.0f);
                    break;
            }
            PhaseController.instance.NextPhase(currPhase);
        }
    }

    private void ResetTimer(float newTimer)
    {
        timer = newTimer;
    }
}
