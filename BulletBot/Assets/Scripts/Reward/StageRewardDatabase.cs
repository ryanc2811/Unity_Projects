using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRewardDatabase : MonoBehaviour
{
    public static StageRewardDatabase instance;

    
    public StageReward[] stageRewards;

    [Serializable]
    public struct StageReward
    {
        public int stageNumber;
        public Reward reward;
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Reward GetRewardForLevel(int levelNumber)
    {
        for (int i = 0; i < stageRewards.Length; i++)
        {
            if (stageRewards[i].stageNumber == levelNumber)
            {
                return stageRewards[i].reward;
            }
        }
        return null;
    }
}
