using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : UIElement
{
    [SerializeField] private TutorialScreen _tutorialScreen;

    // Start is called before the first frame update
    void Awake()
    {
        _tutorialScreen.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTutorial()
    {
        _tutorialScreen.Show();
    }

    public void HideTutorial()
    {
        _tutorialScreen.Hide();
    }
}
