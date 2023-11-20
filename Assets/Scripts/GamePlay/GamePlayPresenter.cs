using System.Collections.Generic;
using Deck;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayPresenter : MonoBehaviour
    {
        [Header("Models")]
        [SerializeField] private GamePlayModel gamePlayModel;
    
        [SerializeField] private PlayerModel playerModel;
    
        private List<CardCell> _cardChose = new List<CardCell>();
    

        [HideInInspector]
        public bool cantChoseCard = true;

        public static GamePlayPresenter Instance;
    
        private float _timerCount;
    
        private bool _isActiveTimer;

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
            gamePlayModel.CalculateSize();
        }
    

        public void ChoseNewCard(CardCell cell)
        { 
        
            if(_cardChose.Count == 1  && _cardChose[0] == cell) return;
        
            AudioManager.Instance.PlayAudio("Click");
        
            _cardChose.Add(cell);
            cell.SetAnimation("Show");

            if (_cardChose.Count == 2)
            {
                cantChoseCard = true;
                Invoke("CheckCardState",1f);
            }
        }

        private void CheckCardState()
        {
            if (_cardChose[0].cardId == _cardChose[1].cardId)
            {
                //Correct Chose
                foreach (var cell in _cardChose)
                {
                    cell.SetAnimation("Destroy");
                }
            
                playerModel.ChangeData(PlayerData.Score);
            
                OnCorrectChose();
                CheckGameState();
            }
            else
            {
                //False Chose
                for (int i = 0; i < _cardChose.Count; i++)
                {
                    _cardChose[i].SetAnimation("Hide");

                }
                playerModel.ChangeData(PlayerData.FieldTry);
            }

            cantChoseCard = false;
            _cardChose.Clear();
        }

        private void CheckGameState()
        {
            if (playerModel.GetScore() == gamePlayModel.cardCount)
                OnWinGame();
        }
    
        public void StartTimer()
        {
            _isActiveTimer = true;
            _timerCount = gamePlayModel.timer;
        }

        private void Update()
        {
            Timer();
        }

        private void Timer()
        {
            if(!_isActiveTimer)return;
            _timerCount -= Time.deltaTime;
            GamePlayView.Instance.OnChangeTimer(_timerCount);
        
        
            if (_timerCount < 0)
            {
                _isActiveTimer = false;
                OnLoseGame();
            }
        }


        public void OnStartGame()
        {
            playerModel.ResetData();
            GameDeck.Instance.StartGame();
        }

        private void OnCorrectChose()
        {
            AudioManager.Instance.PlayAudio("Correct");
        }

        public void OnWinGame()
        {
            _isActiveTimer = false;
            GamePlayView.Instance.OnChangeTimer(0);
            GamePlayView.Instance.EndGameState(true);
            GameDeck.Instance.DestroyAllCard();
            AudioManager.Instance.PlayAudio("Win");
        }

        public void OnLoseGame()
        {
            GamePlayView.Instance.EndGameState(false);
            GameDeck.Instance.DestroyAllCard();
            AudioManager.Instance.PlayAudio("Lose");
        }
    }
}
