using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            //SoundDictionary = new Dictionary<string, SoundClip>();
            BackgroundChanel.loop = true;
        }
        else
        {
            Destroy(this);
        }

    }

    [System.Serializable]
    public class SoundClip
    {
        [System.Serializable]
        public enum ClipType
        {
            Background,
            Effect
        }
        public string name;
        public AudioClip clip;
        public ClipType type;
    }

    [SerializeField]private AudioSource BackgroundChanel;
    
    [SerializeField]private SoundClip[] SoundArray;


    public void PlaySound(string key)
    {
        //Array.Find isn't the fastest search there is. If only Dictionary was easy to Serialize
        SoundClip clipToPlay = Array.Find(SoundArray, sound => sound.name == key);
        if(clipToPlay != null)
        {
            switch (clipToPlay.type)
            {
                case SoundClip.ClipType.Background:
                    {
                        BackgroundChanel.clip = clipToPlay.clip;
                        BackgroundChanel.Play();
                        break;
                    }
                case SoundClip.ClipType.Effect:
                    {
                        GameObject newSoundSourece = new GameObject("SoundSource");
                        AudioSource tempAudioSource = newSoundSourece.AddComponent<AudioSource>();
                        tempAudioSource.PlayOneShot(clipToPlay.clip);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        else
        {
            Debug.LogWarning("Did not found sound with key " + key + " !");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlaySound("Main Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
