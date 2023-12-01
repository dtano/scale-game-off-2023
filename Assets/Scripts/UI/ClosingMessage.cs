using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingMessage : UIElement
{
    [SerializeField] private GameDetailsSO _gameDetailsSO;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        if(_gameDetailsSO != null)
        {
            if (_gameDetailsSO.HasCompletedEntireGame)
            {
                Show();
            }
        }
    }
}
