using System;
using UnityEngine;
using Gamee.Hiuk.Adapter;

public static class GameTimeLocal
{
    public static bool IsNewDay = false;
    public static Action ActionNewDay;
    public static int Month
    {
        get => PlayerPrefsAdapter.GetInt("game_time_month", 1);

        set => PlayerPrefsAdapter.SetInt("game_time_month", value);
    }
    public static int Day
    {
        get => PlayerPrefsAdapter.GetInt("game_time_day", 0);

        set => PlayerPrefsAdapter.SetInt("game_time_day", value);
    }

    public static DateTime RewardTime
    {
        get => PlayerPrefsAdapter.GetDateTime("game_time_reward_time");
        set => PlayerPrefsAdapter.SetDateTime("game_time_reward_time", value);
    }
    public static int RewardDay
    {
        get => PlayerPrefsAdapter.GetInt("game_time_reward_day", 0);

        set => PlayerPrefsAdapter.SetInt("game_time_reward_day", value);
    }
    public static bool IsHasReward
    {
        get => PlayerPrefsAdapter.GetBool("game_time_is_has_reward", false);

        set => PlayerPrefsAdapter.SetBool("game_time_is_has_reward", value);
    }

    public static int RewardWeek => (RewardDay - 1) / 7 + 1;
    public static void CheckNewDay()
    {
        if ((DateTime.Now - RewardTime.AddDays(1).Date).TotalSeconds > 0)
        {
            IsNewDay = true;
            IsHasReward = true;
            RewardTime = DateTime.Now;
            RewardDay++;
            Day++;

            if (RewardDay > 28)
            {
                Month++;
                RewardDay = 1;
            }
            ActionNewDay?.Invoke();
        }
    }
}
