using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class LaneManager : MonoBehaviour
{
    [SerializeField] Lane[] lanes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var it in lanes)
        {
            Debug.LogError("call exe");
            it.Execute();
        }
    }
}
