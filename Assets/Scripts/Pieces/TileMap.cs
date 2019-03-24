using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    int[,] tiles;

    int mapSize = 8;

    private void Start()
    {
        tiles = new int[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                tiles[i, j] = 0;
            }
        }
    }
}
