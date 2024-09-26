using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;
using System;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    private string _highScoreFileName = "high_score.json";
    private string _highScorePath;
    private string _JSONScore;
    private float _scoreTime;
    private void Awake()
    {
        _highScorePath = Application.persistentDataPath + "/" + _highScoreFileName;
        _scoreTime = Data.WinningTime;
        Debug.Log(_highScorePath);
        if (CheckScore())
        {
            Save();
        }
        _bestTimeText.text = $"Best Time: {Math.Round(ReadScoreFromFile(), 2)}";
    }
    public void Save()
    {
        _JSONScore = JsonUtility.ToJson(new JSONWrapperScore(_scoreTime));
        File.WriteAllText(_highScorePath, _JSONScore);
    }
    private bool CheckScore()
    {
        if (ReadScoreFromFile() == 0)
        {
            return true;
        }
        if (Data.WinningTime < ReadScoreFromFile())
        {
            return true;
        }
        else return false;
    }
    private float ReadScoreFromFile()
    {
        try
        {
            string oldJSON = File.ReadAllText(_highScorePath);
            JSONWrapperScore wrapper = JsonUtility.FromJson<JSONWrapperScore>(oldJSON);
            return wrapper.Score;
        }
        catch
        {
            return 0.0f;
        }
    }
}
