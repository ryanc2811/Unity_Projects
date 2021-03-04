using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapHandler
{
    private readonly IReadOnlyCollection<string> maps;
    private readonly int numberOfRounds;

    private int currentRound;
    private List<string> remainingMaps;

    public MapHandler(MapSet mapSet,int numberOfRounds)
    {
        this.numberOfRounds = numberOfRounds;
        maps = mapSet.Maps;

        ResetMaps();
    }

    private bool IsComplete => currentRound == numberOfRounds;

    public string NextMap
    {
        get
        {
            if (IsComplete) { return null; }
            currentRound++;

            if (remainingMaps.Count == 0) { ResetMaps(); }

            string map = remainingMaps[UnityEngine.Random.Range(0, remainingMaps.Count)];

            remainingMaps.Remove(map);

            return map;
        }
    }

    private void ResetMaps() => remainingMaps = maps.ToList();
}
