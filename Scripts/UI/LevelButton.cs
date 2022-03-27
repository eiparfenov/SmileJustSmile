using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private int level;

        public UnityEvent onClick = new UnityEvent();
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(SelectLevel);
        }

        private void SelectLevel()
        {
            FindObjectOfType<GlobalManager>().SelectedLevel = level;
            onClick.Invoke();
        }
    }
}