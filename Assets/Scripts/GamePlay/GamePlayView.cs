using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GamePlay
{
    public class GamePlayView : MonoBehaviour
    {
        [SerializeField] private PlayerModel playerModel;
        
        [Header("Panels")]
        [SerializeField] private GameObject endPanel;

        [Header("Texts")]
        
        [SerializeField] private TMP_Text scoreText;
        
        [SerializeField] private TMP_Text tryText;

        [SerializeField] private TMP_Text timer;
        
        [SerializeField] private TMP_Text panelText;


        [Header("Buttons")]

        [SerializeField] private Button reStartButton;
        
        [SerializeField] private Button goMainButton;

        [Header("Events")]

        
        [SerializeField] private UnityEvent onGoMainEvent;

        [Header("Cash")] 
        
        [SerializeField] private List<GameObject> cashes;
        
        
        private float _timerCount;
        
        private bool _isActiveTimer;


        public static GamePlayView Instance;

        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            SetListeners();
        }

        private void SetListeners()
        {
            reStartButton.onClick.AddListener(OnRestartGame);
            goMainButton.onClick.AddListener(onGoMainEvent.Invoke);
            
            playerModel.onFieldTry.AddListener(OnChangeTryText);
            playerModel.onChangeScore.AddListener(OnChangeScore);
        }
        
        public void OnChangeTimer(float time)
        {
            timer.text = "Time : " + time.ToString("00");
        }

        private void OnChangeTryText(int count) => tryText.text = "Try : " +  count.ToString();

        private void OnChangeScore(int count)
        {
            scoreText.text = "Cash : " + (count * 1000) + "$";
            for (int i = 0; i < count; i++)
            {
                cashes[i].SetActive(true);
            }
        }

        public void EndGameState(bool state)
        {
            endPanel.SetActive(true);
            if (state)
            {
                reStartButton.gameObject.SetActive(false);
                panelText.text = "Win :)";
            }
            else
            { 
                reStartButton.gameObject.SetActive(tryText);
                panelText.text = "Lose :)";
            }
        }

        private void OnRestartGame()
        {
            ResetCash();
            GamePlayPresenter.Instance.OnStartGame();
        }
        
        private void ResetCash()
        {
            foreach (var cash in cashes)
            {
                cash.SetActive(false);
            }
        }
    }
}
