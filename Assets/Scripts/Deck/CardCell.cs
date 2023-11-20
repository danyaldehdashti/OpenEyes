using UnityEngine;

namespace Deck
{
    public class CardCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cardData;

        [SerializeField] private Animation cardAnimation;
        
        [HideInInspector]
        public string cardId;

        private void Start()
        {
            cardAnimation.Play("CreateCard");
        }

        public void SetCardSprite(Sprite sprite) => cardData.sprite = sprite;
    
        public void SetCardId(string id) => cardId = id;
    
        public void SetAnimation(string method) => cardAnimation.Play(method);

        public void DestroyCard() => Destroy(gameObject);

    }
}
