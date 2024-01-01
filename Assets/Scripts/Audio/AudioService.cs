using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoSingletonGeneric<AudioService>
{
    [SerializeField] List<Sound> sounds = new List<Sound>();
    [SerializeField] Sound backgroundMusic;

    private AudioSource soundEffectAudioSrc;
    private AudioSource backgroundMusicAudioSrc;

    private Dictionary<SoundType, Sound> soundDictionary = new Dictionary<SoundType, Sound>();

    protected override void Awake()
    {
        base.Awake();

        soundEffectAudioSrc = gameObject.AddComponent<AudioSource>();
        backgroundMusicAudioSrc = gameObject.AddComponent<AudioSource>();

        InitializeSoundEffects();
        InitializeAndPlayBackgroundMusic();
    }

    private void InitializeSoundEffects()
    {
        foreach (Sound sound in sounds)
        {
            if (soundDictionary.ContainsKey(sound.soundType))
            {
                Debug.LogError("Duplicate SoundType detected: " + sound.soundType);
                continue;
            }

            sound.Initialize(soundEffectAudioSrc);
            soundDictionary[sound.soundType] = sound;
        }
    }

    private void InitializeAndPlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Initialize(backgroundMusicAudioSrc);
            backgroundMusicAudioSrc.loop = true;
            PlayBackgroundMusic();
        }
        else
            Debug.LogWarning("Background music clip not set.");
    }

    public void PlaySound(SoundType soundType)
    {
        if (soundDictionary.TryGetValue(soundType, out var sound))
            sound.Play();
        else
            Debug.LogWarning("Sound not found: " + soundType);
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
            backgroundMusicAudioSrc.Play();
        else
            Debug.LogWarning("Background music not set.");
    }

    public void PauseBackgroundMusic()
    {
        if (backgroundMusic != null)
            backgroundMusicAudioSrc.Pause();
        else
            Debug.LogWarning("Background music not set.");
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusic != null)
            backgroundMusicAudioSrc.Stop();
        else
            Debug.LogWarning("Background music not set.");
    }
}
