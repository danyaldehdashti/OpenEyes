using System;
using Deck;
using GamePlay;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsDeckLayer;

    [SerializeField] private Camera mainCamera;
    
    public static InputSystem Instance;

    private void Start()
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

    private void Update()
    {
        if(GamePlayPresenter.Instance.cantChoseCard)return;
        if (Input.GetMouseButtonDown(0))
        {
            CardCell cardCell = IsMouseOverAGridSpace();
            if (cardCell != null)
            {
                GamePlayPresenter.Instance.ChoseNewCard(cardCell);
            }
        }
    }

    private CardCell IsMouseOverAGridSpace()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, whatIsDeckLayer))
        {
            return hit.transform.GetComponent<CardCell>();
        }
        else
        {
            return null;
        }
    }
    
}
