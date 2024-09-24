using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _player1;
    [SerializeField] private TextMeshProUGUI _player2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ColorTitleAnimation());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator ColorTitleAnimation()
    {
        while (true)
        {
            _title.color = Color.blue;
            _player1.color = Color.blue;
            _player2.color = Color.blue;
            yield return new WaitForSeconds(1f);
            _title.color = Color.yellow;
            _player1.color = Color.yellow;
            _player2.color = Color.yellow;
            yield return new WaitForSeconds(1f);
            _title.color = Color.black;
            _player1.color = Color.black;
            _player2.color = Color.black;
            yield return new WaitForSeconds(1f);
            _title.color = Color.green;
            _player1.color = Color.green;
            _player2.color = Color.green;
            yield return new WaitForSeconds(1f);
            _title.color = Color.red;
            _player1.color = Color.red;
            _player2.color = Color.red;
            yield return new WaitForSeconds(1f);
        }
    }
    public void Start1PlayerGame()
    {
        Data.SetGameMode(Data.GameMode.OnePlayer);
        SceneManager.LoadScene(SceneNames.Scenes.OnePlayerTrack.ToString());
    }
    public void Start2PlayerGame()
    {
        Data.SetGameMode(Data.GameMode.TwoPlayers);
        SceneManager.LoadScene(SceneNames.Scenes.TwoPlayerTrack.ToString());
    }
}
