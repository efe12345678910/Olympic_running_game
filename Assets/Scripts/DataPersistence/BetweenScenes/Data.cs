using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public enum GameMode { OnePlayer,TwoPlayers} 
    public static GameMode GameModeSelected { get; private set; } = GameMode.OnePlayer;
    public static int Runner1FoulCount { get; private set; } = 0;
    public static int Runner2FoulCount { get; private set; } = 0;
    public static float WinningTime { get; private set; }
    public static string Winner;
    public static int DisqualifiedPlayerNo { get; private set; }
    public static void AddDisqualifiedPlayer(int no)
    {
        if (no == 1 || no == 2)
        {
            DisqualifiedPlayerNo = no;
        }
    }
    public static void AddFouls(int runnerNo)
    {
        if (runnerNo == 1)
        {
            Runner1FoulCount++;
        }
        else
        {
            Runner2FoulCount++;
        }
    }
    public static int GetFoulCount(int runnerNo)
    {
        if (runnerNo == 1)
        {
            return Runner1FoulCount;
        }
        else
        {
            return Runner2FoulCount;
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
    public static  void ResetData()
    {
        Runner1FoulCount = 0;
        Runner2FoulCount = 0;
        DisqualifiedPlayerNo = 0;
    }
}
