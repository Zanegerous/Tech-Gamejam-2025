using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum SoundType
{
    JUMP,
    WALK,
    IMPACT,
    WATER_DROPS,
    WIRES_BUZZING,
    MUSIC,
}



public class SoundCollection
{
    private AudioClip[] clips;
    private int lastClipIndex;

    public SoundCollection(params string[] clipNames)
    {
        this.clips = new AudioClip[clipNames.Length];
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i] = Resources.Load<AudioClip>(clipNames[i]);
            if (clips[i] == null)
            {
                Debug.Log($"can't find audio clip {clipNames[i]}");
            }
        }
        lastClipIndex = -1;
    }

    public AudioClip GetRandClip()
    {
        if (clips.Length == 0)
        {
            Debug.Log("No clips to give");
            return null;
        }
        else if (clips.Length == 1)
        {
            return clips[0];
        }
        else
        {
            int index = lastClipIndex;
            while (index == lastClipIndex)
            {
                index = Random.Range(0, clips.Length);
            }
            lastClipIndex = index;
            return clips[index];
        }
    }

}

public class SoundManager : MonoBehaviour
{
    public AudioClip GetClip(SoundType type)
    {
        if (sounds.ContainsKey(type))
        {
            return sounds[type].GetRandClip();
        }
        return null;
    }

    public float mainVolume = 1.0f;
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSrc;

    public static SoundManager Instance { get; private set; }

    // unity life cycle
    private void Awake()
    {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
        sounds = new Dictionary<SoundType, SoundCollection> {
            { SoundType.WATER_DROPS, new SoundCollection("Audio/waterdrip") },
            { SoundType.WIRES_BUZZING, new SoundCollection("Audio/electricity") },
            { SoundType.JUMP, new SoundCollection("SFX/Jump") },
            { SoundType.IMPACT, new SoundCollection("SFX/Impact") },
            { SoundType.WALK, new SoundCollection("SFX/Walk") },
        };
    }


    public void Play(SoundType type, AudioSource overrideSource = null)
    {
        if (sounds.ContainsKey(type))
        {
            var source = overrideSource ?? audioSrc;
            audioSrc.volume = Random.Range(0.70f, 1.0f) * mainVolume;
            audioSrc.pitch = Random.Range(0.75f, 1.25f);
            audioSrc.clip = sounds[type].GetRandClip();
            audioSrc.Play();
        }
    }
}