using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotGameObject : MonoBehaviour
{

    public GameObject map;

    private void Start()
    {
        foreach(var state in map.GetComponentsInChildren<Transform>())
        {
            if (!state.gameObject.name.Contains("Cap"))
            {

                GameObject parent = new GameObject();
                parent.name = state.name;
                parent.transform.SetParent(map.transform);
                state.transform.SetParent(parent.transform);

            }
        }
    }
}
