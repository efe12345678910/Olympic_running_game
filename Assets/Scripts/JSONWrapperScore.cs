using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JSONWrapperScore
{
    public float Score;
    public JSONWrapperScore(float score = 0)
    {
        Score = score;
    }
}
