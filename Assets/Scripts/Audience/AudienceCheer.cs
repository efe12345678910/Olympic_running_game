using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceCheer : MonoBehaviour
{
    [SerializeField] Animator _audience;
    [SerializeField] Animator _audience2;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeAudienceCheer()
    {
        _audience.SetTrigger("cheer");
        _audience2.SetTrigger("cheer");
    }
    private void OnEnable()
    {
        GameManager.Instance.VictoryEvent += MakeAudienceCheer;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.VictoryEvent -= MakeAudienceCheer;
        }
    }
}
