using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DefaultRenderOrder : MonoBehaviour
{

    public bool callOnUpdate;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SortingGroup>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

    }

    // Update is called once per frame
    void Update()
    {
        if(callOnUpdate)
            GetComponent<SortingGroup>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
