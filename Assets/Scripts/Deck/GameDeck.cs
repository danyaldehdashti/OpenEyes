using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Deck
{
    public class GameDeck : MonoBehaviour
    {
        [SerializeField] private GamePlayModel gamePlayModel;
        
        private List<CardCell> _allCardCell = new List<CardCell>();

        private List<Sprite> _inGameCardSprite = new List<Sprite>();

        private GameObject[,] _gameDeck;

        private int _cardCount;
        

        public static GameDeck Instance;

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
        }


        public void StartGame()
        {
            StartCoroutine(CreateDeck());
        
            float time = (gamePlayModel.deckWidth * gamePlayModel.deckHight) * gamePlayModel.delayTime + 1f;
            Invoke("CreateSpriteDeck", time);
        }

   
        private IEnumerator CreateDeck()
        {
            _gameDeck = new GameObject[gamePlayModel.deckWidth,gamePlayModel.deckHight];
        
            if (gamePlayModel.cardCallPrefab == null)
            {
                Debug.LogError("Prefab Not Exist");
                yield return  null;
            }

            for (int y = 0; y < gamePlayModel.deckHight; y++)
            {
                for (int x = 0; x < gamePlayModel.deckWidth; x++)
                {
                    _gameDeck[x, y] = Instantiate(gamePlayModel.cardCallPrefab,
                        new Vector3(x * gamePlayModel.deckSpaceSizeX, 0,y * gamePlayModel.deckSpaceSizeY),Quaternion.identity);
                
                    _allCardCell.Add(_gameDeck[x, y].GetComponent<CardCell>());
                
                    _gameDeck[x, y].transform.parent = transform;
                    _gameDeck[x, y].gameObject.name = "Grid Space ( X: " + x.ToString() + ",Y:" + y.ToString() + ")";
                    _cardCount++;
                
                    yield return new WaitForSeconds(gamePlayModel.delayTime);
                }
            }
        }
    
        private void CreateSpriteDeck()
        {
            int index = 0;
            for (int count = 0; count < _cardCount; count++)
            {
                if (index == _cardCount / 2)
                    index = 0;
            
                _inGameCardSprite.Add(gamePlayModel.GetCards()[index]);
                index++;
            }
        
            for (int card = 0; card < _allCardCell.Count; card++)
            {
                var randNumber = Random.Range(0, _inGameCardSprite.Count); 
                _allCardCell[card].SetCardSprite(_inGameCardSprite[randNumber]);
                _allCardCell[card].SetCardId(_inGameCardSprite[randNumber].name);
            
                _inGameCardSprite.RemoveAt(randNumber);
            }
            OnDeckDone();
        }

        public void DestroyAllCard()
        {
            for (int i = 0; i < _allCardCell.Count; i++)
            {
                if (_allCardCell[i] != null)
                {
                    Destroy(_allCardCell[i]);
                }
            }
            _allCardCell.Clear();
        }

        private void OnDeckDone()
        {
            GamePlayPresenter.Instance.cantChoseCard = false;
            GamePlayPresenter.Instance.StartTimer();
        }
    }
}

