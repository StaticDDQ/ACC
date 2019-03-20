using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Text countdownText;
    [SerializeField] private float minTimer;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    private void Start()
    {
        timer = minTimer;
    }

    // Update is called once per frame
    private void Update()
    {
        if(timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            countdownText.text = (int)timer + "";
        } else if(timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            countdownText.text = "0";
            timer = 0.0f;
            NextPhase();
        }
    }

    private void NextPhase()
    {

    }

    public void ResetTimer()
    {
        timer = minTimer;
        canCount = true;
        doOnce = false;
    }
}
