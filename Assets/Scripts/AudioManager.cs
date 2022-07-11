using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustMusicVolume(float _musicVolume)
    {
        musicVolume = _musicVolume;
    }

    public void AdjustSfxVolume(float _sfxVolume)
    {
        sfxVolume = _sfxVolume;
    }
}
