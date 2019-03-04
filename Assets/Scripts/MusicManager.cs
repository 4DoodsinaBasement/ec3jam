using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrack(SeasonType season)
    {
        source.clip = ResourceLoader.gameMusic[(int)season];
        source.volume = 1.0f;
        source.Play();
    }

    public void SetVolume(float vol)
    {
        source.volume = vol;
    }
}
