using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStartProxy : MonoBehaviour
{
    [SerializeField] Music musicEngine;


    private void Reset()
    {
        musicEngine = GetComponent<Music>();
    }

    private void Awake()
    {
        musicEngine.Setup();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
