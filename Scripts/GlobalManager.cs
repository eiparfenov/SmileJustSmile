using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    [SerializeField] private GameObject level;

    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject deadUI;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Text reasonOfDeath;

    private GameObject _currentLevel;
    private void Start()
    {
        _currentLevel = Instantiate(level);
        GlobalEventsManager.OnPlayerDead.AddListener(DieHandler);
        restartButton.onClick.AddListener(LevelRestartListener);
        menuButton.onClick.AddListener(GoToMenuListener);
    }

    private void DieHandler(DieType type)
    {
        if (type == DieType.Mask)
            reasonOfDeath.text = "Вы слишком долго носили маску";
        else
            reasonOfDeath.text = "Вас заметили";
        
        gameUI.SetActive(false);
        deadUI.SetActive(true);
        
        Time.timeScale = 0f;
        GlobalEventsManager.RemoveAllListeners();
    }
   
    private void LevelRestartListener()
    {
        print("Restart");
        Destroy(_currentLevel);
        _currentLevel = Instantiate(level);
        GlobalEventsManager.OnPlayerDead.AddListener(DieHandler);
        Time.timeScale = 1f;
        gameUI.SetActive(true);
        deadUI.SetActive(false);
        FindObjectOfType<Joystick>().ResetHeadPosition();
    }

    private void GoToMenuListener()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
}