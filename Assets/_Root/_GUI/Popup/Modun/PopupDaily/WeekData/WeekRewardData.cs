using Gamee.Hiuk.Popup.Daily;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeekRewardData", menuName = "Reward Data/Week Reward Data", order = 0)]
public class WeekRewardData : ScriptableObject
{
    public List<RewardData> rewards;
}
