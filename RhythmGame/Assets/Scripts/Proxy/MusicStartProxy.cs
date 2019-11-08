using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStartProxy : MonoBehaviour
{
    [SerializeField] Music musicEngine;
    [SerializeField] AudioSource source;

    private void Reset()
    {
        musicEngine = GetComponent<Music>();
        source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        musicEngine.Setup();
    }

    // Start is called before the first frame update
    void Start()
    {
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
