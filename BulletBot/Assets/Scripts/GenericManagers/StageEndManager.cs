using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEndManager : MonoBehaviour
{
    [SerializeField]
    private RewardUI rewardUI;
    public int stageNumber=0;
    void Awake()
    {
        PlayerPrefs.SetInt("CurrentStage", stageNumber);
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnStageFailedTrigger += StageFailed;
        EventManager.instance.OnStagePassedTrigger += StagePassed;
    }
    void StagePassed()
    {
        StageRewardDatabase rewardDatabase = StageRewardDatabase.instance;
        Reward reward = rewardDatabase.GetRewardForLevel(stageNumber);
        rewardUI.gameObject.SetActive(true);
        rewardUI.DisplayReward(reward,stageNumber);
    }
    void StageFailed()
    {
        Debug.Log("FAILED");
        RestartStage();
    }
    public void LoadNextStage()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        if (SceneManager.GetSceneByBuildIndex(nextSceneIndex) != null)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
            
    }
    public void RestartStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnDestroy()
    {
        EventManager.instance.OnStageFailedTrigger -= StageFailed;
        EventManager.instance.OnStagePassedTrigger -= StagePassed;
    }
}
