using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    Vector2 pos;
    Vector2 initPos;
    RectTransform rect;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        initPos = rect.anchoredPosition;
        Debug.Log(initPos);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length == 1)
        {
            Touch t1 = Input.touches[0];

            if (t1.phase == TouchPhase.Began)
            {
                pos = t1.position;
            }
            else if (t1.phase == TouchPhase.Moved)
            {
                rect.anchoredPosition = t1.position - pos + initPos;
            }
            else if(t1.phase == TouchPhase.Ended)
            {
                initPos = rect.anchoredPosition;
            }
            
        }

        if(rect.anchoredPosition.x > Screen.width / 2)
        {
            rect.anchoredPosition = new Vector2(Screen.width / 2, rect.anchoredPosition.y);
        }
        if(rect.anchoredPosition.x < - Screen.width / 2)
        {
            rect.anchoredPosition = new Vector2( - Screen.width / 2, rect.anchoredPosition.y);
        }

        if (rect.anchoredPosition.y > Screen.height / 2)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, Screen.height / 2);
        }
        if (rect.anchoredPosition.y < -Screen.height / 2)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, - Screen.height / 2);
        }

    }
}
