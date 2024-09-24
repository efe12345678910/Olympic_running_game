using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public enum GameMode { OnePlayer,TwoPlayers} 
    public static GameMode GameModeSelected { get; private set; } = GameMode.OnePlayer;
    private static int _runner1FoulCount = 0;
    private static int _runner2FoulCount  = 0;
    public static float WinningTime { get; private set; }
    public static string Winner;
    public static void AddFouls(int runnerNo)
    {
        if (runnerNo == 1)
        {
            _runner1FoulCount++;
        }
        else
        {
            _runner2FoulCount++;
        }
    }
    public static int GetFoulCount(int runnerNo)
    {
        if (runnerNo == 1)
        {
            return _runner1FoulCount;
        }
        else
        {
            return _runner2FoulCount;
        }
    }
    public static void SetWinnerData(float winningTime, int playerNo)
    {
        WinningTime = winningTime;
        if (playerNo == 1)
        {
            Winner = "Player 1";
        }
        else
        {
            Winner = "Player 2";
        }
    }
    public static void SetGameMode(GameMode gm)
    {
        GameModeSelected = gm;
    }
    
}
