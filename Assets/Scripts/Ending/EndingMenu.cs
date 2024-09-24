using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EndingMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreInfo;
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(SceneNames.Scenes.MainScreen.ToString());
    }
    private void Awake()
    {
        _scoreInfo.text = $"Winner: {Data.Winner}\nTime: {Math.Round(Data.WinningTime,2)}\n\nBest Time:";
    }
}
