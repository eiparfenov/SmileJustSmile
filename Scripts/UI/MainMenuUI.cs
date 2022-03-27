using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [Header("GameUI")]
        [SerializeField] private GameObject gameUI;
        
        [Header("MainUI")]
        [SerializeField] private GameObject mainUI;
        [SerializeField] private Button startButton;
        [SerializeField] private Button levelsButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        
        [Header("LevelsUI")]
        [SerializeField] private GameObject levelsUI;
        [SerializeField] private Button backLevelsButton;
        [SerializeField] private LevelButton[] levelButtons;
        
        [Header("SettingsUI")]
        [SerializeField] private GameObject settingsUI;
        [SerializeField] private Button backSettingsButton;
        
        [Header("DeadUI")]
        [SerializeField] private GameObject deadUI;
        [SerializeField] private Button restartLevelButton;
        [SerializeField] private Button goToMainDeadButton;
        [SerializeField] private Text deadText;
        
        [Header("WinUI")]
        [SerializeField] private GameObject winUI;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button goToMainAliveButton;

        public UnityEvent onStartGame = new UnityEvent();
        public UnityEvent onRestartLevel = new UnityEvent();
        public UnityEvent onNextLevel = new UnityEvent();
        public UnityEvent onReturnToMainMenu = new UnityEvent();

        private void Start()
        {
            startButton.onClick.AddListener(StartButtonPressHandler);
            levelsButton.onClick.AddListener(LevelsButtonPressHandler);
            settingsButton.onClick.AddListener(SettingsButtonPressHandler);
            quitButton.onClick.AddListener(QuitButtonPressHandler);
            backLevelsButton.onClick.AddListener(BackButtonPressHandler);
            backSettingsButton.onClick.AddListener(BackButtonPressHandler);
            restartLevelButton.onClick.AddListener(RestartLevelButtonPressHandler);
            nextLevelButton.onClick.AddListener(NextLevelButtonPressHandler);
            goToMainDeadButton.onClick.AddListener(ReturnToMainMenuButtonHandler);
            goToMainAliveButton.onClick.AddListener(ReturnToMainMenuButtonHandler);
            foreach (LevelButton levelButton in levelButtons)
            {
                levelButton.onClick.AddListener(LevelSelectedHandler);
            }
        }
        public void ShowLose(DieType type)
        {
            if (type == DieType.Killed)
                deadText.text = "Обнаружение";
            else
                deadText.text = "Забвение";
            
            gameUI.SetActive(false);
            deadUI.SetActive(true);
        }
        public void ShowWin()
        {
            gameUI.SetActive(false);
            winUI.SetActive(true);
        }
        private void GoToMainMenu()
        {
            mainUI.SetActive(true);
            gameUI.SetActive(false);
            levelsUI.SetActive(false);
            settingsUI.SetActive(false);
            deadUI.SetActive(false);
            winUI.SetActive(false);
        }
        private void GoToGameUI()
        {
            mainUI.SetActive(false);
            gameUI.SetActive(true);
            levelsUI.SetActive(false);
            settingsUI.SetActive(false);
            deadUI.SetActive(false);
            winUI.SetActive(false);
        }
        
        private void StartButtonPressHandler()
        {
            onStartGame.Invoke();
            GoToGameUI();
        }

        private void LevelsButtonPressHandler()
        {
            mainUI.SetActive(false);
            gameUI.SetActive(false);
            levelsUI.SetActive(true);
            settingsUI.SetActive(false);
            deadUI.SetActive(false);
            winUI.SetActive(false);
        }
        private void SettingsButtonPressHandler()
        {
            mainUI.SetActive(false);
            gameUI.SetActive(false);
            levelsUI.SetActive(false);
            settingsUI.SetActive(true);
            deadUI.SetActive(false);
            winUI.SetActive(false);
        }
        private void BackButtonPressHandler()
        {
            GoToMainMenu();
        }
        private void QuitButtonPressHandler()
        {
            Application.Quit();
        }

        private void RestartLevelButtonPressHandler()
        {
            onRestartLevel.Invoke();
            GoToGameUI();
        }

        private void NextLevelButtonPressHandler()
        {
            onNextLevel.Invoke();
            GoToGameUI();
        }

        private void ReturnToMainMenuButtonHandler()
        {
            onReturnToMainMenu.Invoke();
            GoToMainMenu();
        }

        private void LevelSelectedHandler()
        {
            onStartGame.Invoke();
            GoToGameUI();
        }
    }
}