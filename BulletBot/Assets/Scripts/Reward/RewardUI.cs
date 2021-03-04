using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardUI : MonoBehaviour
{
    TextMeshProUGUI stageNumberText;
    TextMeshProUGUI currencyRewardText;
    void Awake()
    {
        stageNumberText = transform.Find("StageNumber").GetComponent<TextMeshProUGUI>();
        currencyRewardText = transform.Find("CurrencyReward").GetComponent<TextMeshProUGUI>();
    }
    public void DisplayReward(Reward reward,int stageNumber)
    {
        GameState.instance.Pause();
        stageNumberText.text = "Stage " + stageNumber;
        currencyRewardText.text = "+$" + reward.currencyReward;
        EventManager.instance.XpAddedTrigger(reward.xpReward);
    }
}
