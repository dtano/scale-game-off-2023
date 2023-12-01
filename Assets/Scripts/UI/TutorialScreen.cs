using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class TutorialScreen : UIElement
{
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private VideoEventChannel _videoEventChannel;
    [SerializeField] private Transform _pagesParent;
    [SerializeField] private TextMeshProUGUI _pagesLeftText;

    [SerializeField] GameObject _nextBtn;
    [SerializeField] GameObject _backBtn;

    private TutorialPage[] pages;
    private int _currentPageIndex;
    
    void Awake()
    {
        pages = new TutorialPage[_pagesParent.transform.childCount];
        for (int i = 0; i < _pagesParent.transform.childCount; i++)
        {
            pages[i] = _pagesParent.transform.GetChild(i).GetComponent<TutorialPage>();
            pages[i].OnRequestPlayClipEvent += RequestPlayVideoClip;
            HidePage(i);
        }

        ShowPage(_currentPageIndex);
    }

    private void HidePage(int index)
    {
        pages[index].Hide();
    }

    private void ShowPage(int index)
    {
        int totalPages = pages.Length;
        
        pages[index].Show();
        if (index == 0)
        {
            _nextBtn.SetActive(true);
            _backBtn.SetActive(false);
        }
        else if (index == pages.Length - 1)
        {
            _backBtn.SetActive(true);
            _nextBtn.SetActive(false);
        }
        else
        {
            _backBtn.SetActive(true);
            _nextBtn.SetActive(true);
        }

        _pagesLeftText.text = $"{index + 1}/{totalPages}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        HidePage(_currentPageIndex);
        _currentPageIndex++;
        ShowPage(_currentPageIndex);
    }

    public void Back()
    {
        HidePage(_currentPageIndex);
        _currentPageIndex--;
        ShowPage(_currentPageIndex);
    }

    public void OnClickEndTutorial()
    {
        // We need to hide this screen and trigger some sort of event again
        _gameStateEventChannel.OnEndTutorial();
        Hide();
    }

    private void RequestPlayVideoClip(VideoClip videoClip)
    {
        if (_videoEventChannel != null) _videoEventChannel.RaiseVideoRequest(videoClip);
    }

    private void OnDestroy()
    {
        foreach(TutorialPage page in pages)
        {
            if(page != null)
            {
                page.OnRequestPlayClipEvent -= RequestPlayVideoClip;
            }
        }
    }
}
