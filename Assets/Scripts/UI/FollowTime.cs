using UnityEngine;
using UnityEngine.UI;

public class FollowTime : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = (int)Countdown.instance.timer + "";
    }
}
