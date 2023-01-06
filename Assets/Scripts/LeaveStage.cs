using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveStage : MonoBehaviour
{
    [SerializeField] GameEvent _gameEvents;
    private Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Go);
    }
    void Go() => _gameEvents.ShowInterstitial();
        
        
    
}
