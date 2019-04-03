using System.Collections.Generic;
using UnityEngine;

public class DistributeUnits : MonoBehaviour
{
    public static DistributeUnits instance;
    [SerializeField] private List<GameObject> players;
    private PreparePhase player;
    private int finishedBattle = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SendUnitsToRandomPlayer(List<Transform> enemies)
    {
        do
        {
            player = players[Random.Range(0, players.Count)].GetComponent<PreparePhase>();
        } while (player.GetHasEnemies());
        player.ReceiveEnemyUnits(enemies);
        player.SetHasEnemies(true);
        
    }

    public void PlayerFinished()
    {
        finishedBattle += 1;
        if(finishedBattle == players.Count)
        {
            finishedBattle = 0;
            foreach (var player in players)
            {
                player.GetComponent<Countdown>().NextPhase();
            }
        }
    }
}
