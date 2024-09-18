using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioBank : MonoBehaviour
{
    private static SceneAudioBank _instance;
    public static SceneAudioBank Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    public List<AudioDict> RunnerSounds = new List<AudioDict>() ;
    
    
}
