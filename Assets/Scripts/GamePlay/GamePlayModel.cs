using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    [CreateAssetMenu(fileName = "NewGamePlayModel", menuName = "GamePlayModel")]
    public class GamePlayModel : ScriptableObject
    {
       
        #region GridData
        [Header("Set Card Count"),Range(3,10)]
        public int cardCount;
        
        [Header("Set Timer")]
        public float timer;
 
    
        [Header("Set Grid Space Data")]
        public float deckSpaceSizeX;
        public float deckSpaceSizeY;
    
        [Header("Set SpawnDelay")]
        public float delayTime = 0.1f;

    
        [Header("Set CardPrefab")]
        public GameObject cardCallPrefab;

        [HideInInspector]
        public int deckWidth;
        [HideInInspector]
        public int deckHight;

        #endregion

        #region CardData

        [Header("Set Cards Sprites")]
        [SerializeField] private List<Sprite> allCards;

   

        public List<Sprite> GetCards()
        {
            return allCards;
        }
        #endregion

      

        public void CalculateSize()
        {
            int numberOne = CalculateTheLargestDivisibleNumber();
            int numberTwo = (cardCount * 2) / numberOne;

            if (numberOne > numberTwo)
            {
                deckWidth = numberOne;
                deckHight = numberTwo;
            }
            else
            {
                deckWidth = numberTwo;
                deckHight = numberOne;
            }
        }
    
        private int CalculateTheLargestDivisibleNumber()
        {
            var cal = cardCount * 2;
            Debug.LogWarning(cardCount);
            for (int number = cardCount-1; number < cal; number--)
            {
                var result = cal % number;
                if (result == 0)
                    return number;
            }

            return 0;
        }
    }
}

