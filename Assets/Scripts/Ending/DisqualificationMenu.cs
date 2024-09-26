using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisqualificationMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _disqText;
    private void Awake()
    {
        _disqText.text = $"Player {Data.DisqualifiedPlayerNo} is disqualified.";
    }
    public void ReturnToTitle()
    {
        Data.ResetData();
        SceneManager.LoadScene(SceneNames.Scenes.MainScreen.ToString());
    }
}
