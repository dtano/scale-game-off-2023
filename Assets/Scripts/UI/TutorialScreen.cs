using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : UIElement
{
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEndTutorial()
    {
        // We need to hide this screen and trigger some sort of event again
        _gameStateEventChannel.OnEndTutorial();
        Hide();
    }
}
