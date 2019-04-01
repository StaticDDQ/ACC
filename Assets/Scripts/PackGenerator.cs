using System;
using UnityEngine;

public class PackGenerator : MonoBehaviour {

    [SerializeField] private GameObject[] s1;
    [SerializeField] private GameObject[] s2;
    [SerializeField] private GameObject[] s3;
    [SerializeField] private GameObject[] s4;
    [SerializeField] private GameObject[] s5;

    [Serializable]
    public struct PieceRarity
    {
        [Range(0, 100)]
        public int s1;
        [Range(0, 100)]
        public int s2;
        [Range(0, 100)]
        public int s3;
        [Range(0, 100)]
        public int s4;
        [Range(0, 100)]
        public int s5;
    }

    public PieceRarity[] rarities;
    private static PackGenerator instance = null;
    private static object padlock = new object();
    public static PackGenerator Instance {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new PackGenerator();
                return instance;
            }
        }
    }

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
	}
	
    private GameObject GetRandomPiece(int level)
    {
        return s1[UnityEngine.Random.Range(0, s1.Length)];
    }

	public GameObject[] RequestPack(int level)
    {
        GameObject[] pack = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            pack[i] = GetRandomPiece(level);
        }

        return pack;
    }
}
