using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NewPlayerModel", menuName = "PlayerModel")]
public class PlayerModel : ScriptableObject
{
    private int _score;
    
    private int _fieldTry;


    [HideInInspector] public UnityEvent<int> onChangeScore = new UnityEvent<int>();
    
    [HideInInspector] public UnityEvent<int> onFieldTry = new UnityEvent<int>();

    public void ChangeData(PlayerData playerData)
    {
        switch (playerData)
        {
            case PlayerData.Score:
                _score++;
                onChangeScore.Invoke(_score);
                break;
            
            case PlayerData.FieldTry:
                _fieldTry++;
                onFieldTry.Invoke(_fieldTry);
                break;
        }
    }

    public void ResetData()
    {
        _score = 0;
        _fieldTry = 0;
        onChangeScore.Invoke(_score);
        onFieldTry.Invoke(_fieldTry);
    }

    public int GetScore() => _score;
}

public enum PlayerData
{
    Score,
    FieldTry
}
