using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    [SerializeField] private GameObject level;
    [SerializeField] private GameObject[] levels;

    private MainMenuUI _mainUI;
    private GameObject _currentLevel;
    private int _selectedLevel = 0;

    public int SelectedLevel
    {
        set => _selectedLevel = value % levels.Length;
        get => _selectedLevel;
    }
    private void Start()
    {
        GlobalEventsManager.OnPlayerDead.AddListener(DieHandler);
        GlobalEventsManager.OnLevelFinished.AddListener(FinishHandler);

        _mainUI = FindObjectOfType<MainMenuUI>();
        _mainUI.onNextLevel.AddListener(NextLevelHandler);
        _mainUI.onRestartLevel.AddListener(RestartLevelHandler);
        _mainUI.onStartGame.AddListener(StartLevelHandler);
        _mainUI.onReturnToMainMenu.AddListener(ReturnToMainMenuHandler);
    }

    private void DieHandler(DieType type)
    {
        Time.timeScale = 0f;
        _mainUI.ShowLose(type);
    }
    

    private void StartLevel()
    {
        Time.timeScale = 1f;
        _currentLevel = Instantiate(levels[_selectedLevel]);
    }

    private void DestroyLevel()
    {
        Destroy(_currentLevel);
    }

    private void ReturnToMainMenuHandler()
    {
        DestroyLevel();
    }

    private void RestartLevelHandler()
    {
        DestroyLevel();
        StartLevel();
    }

    private void NextLevelHandler()
    {
        DestroyLevel();
        SelectedLevel += 1;
        StartLevel();
    }

    private void StartLevelHandler()
    {
        StartLevel();
    }

    private void FinishHandler()
    {
        Time.timeScale = 0f;
        _mainUI.ShowWin();
    }
}