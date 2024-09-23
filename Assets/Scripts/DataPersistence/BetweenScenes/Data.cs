using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    private static int _runner1FoulCount = 0;
    private static int _runner2FoulCount  = 0;
    
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
}
