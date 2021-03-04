using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AISpawner : MonoBehaviour,ISpawner
{
    private List<Vector3> spawnLocations;
    public List<GameObject> AI;
    [Range (2,5)]
    public float spawnRate;
    float spawnTimer=0;
    //DECLARE A TILEMAP FOR THE AVAILABLE TILES THAT THE ENEMIES CAN SPAWN ON
    public Tilemap spawnMap;
    public int totalAIToBeSpawned=4;
    int AISpawned=1;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = new List<Vector3>();
        //SET THE SPAWNABLE TILES
        for(int i = spawnMap.cellBounds.xMin;i<spawnMap.cellBounds.xMax;i++)
        {
            for(int j = spawnMap.cellBounds.yMin; j < spawnMap.cellBounds.yMax; j++)
            {
                Vector3Int tileVectorInt = new Vector3Int(i, j, 0);
                Vector3 tileLocation = spawnMap.CellToWorld(tileVectorInt);
                if (spawnMap.HasTile(tileVectorInt))
                {
                    spawnLocations.Add(tileLocation);
                }
            }
        }
        spawnTimer = Time.time;
        
    }
    /// <summary>
    /// SPAWNS THE GAME OBJECTS INTITALLY
    /// </summary>
    public void InitialSpawn()
    {
        if (Time.time > spawnTimer + spawnRate)
        {
            AISpawned ++;
            Instantiate(AI[Random.Range(0,AI.Count)], spawnLocations[Random.Range(0, spawnLocations.Count)], Quaternion.identity);
            spawnTimer = Time.time;
        }
    }
    /// <summary>
    /// SPAWNS THE GAME OBJECTS CONSTANTLY
    /// </summary>
    public void ConstantSpawn()
    {
        for(int i = 0; i < AI.Count; i++)
        {
            if (Time.time > spawnRate+spawnTimer) { 
            Instantiate(AI[i], spawnLocations[Random.Range(0, spawnLocations.Count)], Quaternion.identity);
            spawnTimer = Time.time;
            }
        }
    }
    /// <summary>
    /// SPAWNS GAME OBJECT
    /// </summary>
    /// <param name="AI"></param>
    public void Spawn(GameObject AI)
    {
        Instantiate(AI,spawnLocations[Random.Range(0,spawnLocations.Count)],Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        //ConstantSpawn();
        if(AISpawned <= totalAIToBeSpawned)
            InitialSpawn();
      
    }

}
