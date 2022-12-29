using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameEvent _gameEvents;
    public string HomeScene;
    public string SceneToReload;
    public GameObject GameOverPanel;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _backToHomeButton;
    [SerializeField] Button _extraLifeButton;
    // Start is called before the first frame update
    void Start()
    {
        _restartButton.onClick.AddListener(Restart);
        _backToHomeButton.onClick.AddListener(GoBackToHome);
        _extraLifeButton.onClick.AddListener(ExtraLife);

        GameOverPanel.SetActive(false);
        _gameEvents.OnGameOver()
            .Subscribe(_ => EndGame())
            .AddTo(this);

        
    }

    void EndGame(){
        GameOverPanel.SetActive(true);
        //_gameEvents.PauseGame();
    }

    void Restart(){
        _gameEvents.LoadScene.OnNext(SceneToReload);
        _gameEvents.ResumeGame();
    }
    void GoBackToHome(){
        _gameEvents.LoadScene.OnNext(HomeScene);
        _gameEvents.ResumeGame();
    }
    void ExtraLife(){
        _gameEvents.Revive();
        GameOverPanel.SetActive(false);
    }

   
}
