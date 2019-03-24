using System.Collections.Generic;
using UnityEngine;

public class DistributeUnits : MonoBehaviour
{
    public static DistributeUnits instance;
    [SerializeField] private List<PreparePhase> players;
    private PreparePhase player;

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
            player = players[Random.Range(0, players.Count)];
        } while (player.GetHasEnemies());
        player.ReceiveEnemyUnits(enemies);
        player.SetHasEnemies(true);
        
    }
}
