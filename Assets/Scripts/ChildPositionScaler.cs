using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPositionScaler : MonoBehaviour
{
    [SerializeField] private float _horizontalGap = 3f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if(i > 0 && i < transform.childCount)
            {
                Transform previousChild = transform.GetChild(i - 1);
                BoxCollider2D collider = previousChild.GetComponent<BoxCollider2D>();
                Debug.Log($"Bounds size: {collider.bounds.size}");
                Debug.Log($"Previous x: {previousChild.position.x}");
                child.position = new Vector3(previousChild.position.x + collider.bounds.size.x/2 + _horizontalGap, child.position.y, child.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
