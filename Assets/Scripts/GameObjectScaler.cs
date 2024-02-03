using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectScaler : MonoBehaviour
{
    private float _targetWidth = 1920f;
    private float _targetHeight = 1080f;

    // Start is called before the first frame update
    void Start()
    {
        float width = Screen.width;
        float height = Screen.height;

        //transform.localScale = new Vector3(width/_targetWidth, height/_targetHeight, 0);

        Debug.Log($"Screen resolution: {width} x {height}");
        Debug.Log($"Local scale: {transform.localScale}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
