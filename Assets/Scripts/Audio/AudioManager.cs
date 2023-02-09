using UnityEngine;
using System;

public class AudioManager : SingletonNetworkPersistent<AudioManager>
{
    public Sound[] sounds;

    private void Start()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
        }
    }

    public void PlayAudio(string nameSound)
    {
        Array.Find(sounds, (s) => s.name == nameSound).source.Play();
    }

    public void StopAudio(string nameSound)
    {
        Array.Find(sounds, (s) => s.name == nameSound).source.Stop();

    }

    public void PlayAudioAtPoint(string nameSound, Vector3 position)
    {
        Sound sound = Array.Find(sounds, (s) => s.name == nameSound);
        AudioSource.PlayClipAtPoint(sound.clip, position);
    }
}
