using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColoredBoxSpawner : OnMessage<WorkdayStarted, WorkdayEnded>
{   
    [SerializeField]
    private List<GameObject> spawnPool = new List<GameObject>();
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField] 
    private SpawnType spawnType;

    [SerializeField] private SortingColor[] colors;
    
    private GameObject GetRandomObject()
    {
        return spawnPool[Random.Range(0, spawnPool.Count)];
    }
    
    private int currentIndex = 0;
    
    private GameObject GetNextObject()
    {
        var obj = spawnPool[currentIndex];
        currentIndex = (currentIndex + 1) % spawnPool.Count;
        return obj;
    }

    private void SpawnObject()
    {
        GameObject toSpawn = spawnType switch
        {
            SpawnType.Random => GetRandomObject(),
            SpawnType.Cycle => GetNextObject(),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var go = Instantiate(toSpawn, spawnPoint.position, Quaternion.identity, transform.parent);
        if (go.TryGetComponent(out ColoredBox box))
        {
            var spawnColor = colors.Random();
            if (spawnColor == SortingColor.All)
                spawnColor = SortingColors.All.Random();
            box.SetColor(spawnColor);
        }
    }

    protected override void Execute(WorkdayStarted msg)
    {
        InvokeRepeating(nameof(SpawnObject), 0.0f, CurrentGameState.State.BoxSpawnInterval);
    }

    protected override void Execute(WorkdayEnded msg)
    {
        CancelInvoke();
    }

    private enum SpawnType
    {
        Random,
        Cycle
    }
}