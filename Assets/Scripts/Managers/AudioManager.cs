using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        LEVEL_CLEARED,
        ROLL,
        BOUNCE,
        SQUISH,
        MENU_CLICK,
        CLOCK,
        KILL
    }

    public enum TrackType
    {
        LOST,
        WIN,
        MENU,
        GAMEPLAY
    }

    // Sound effects
    public GameObject LevelCleared;

    public GameObject Rolling;
    public GameObject Bounces;
    public GameObject Squishes;
    public GameObject Clock;
    public GameObject Kill;
    private AudioSource ALevelCleared;
    private AudioSource ARoll;
    private AudioSource ABounce;
    private AudioSource ASquishes;
    private AudioSource AClock;
    private AudioSource AKill;

    // Tracks
    public GameObject Lost;

    public GameObject Win;
    public GameObject Menu;
    public GameObject Gameplay;
    private AudioSource ALost;
    private AudioSource AWin;
    private AudioSource AMenu;
    private AudioSource AGameplay;

    public static AudioManager Find()
    {
        if (Instance == null)
        {
            Instance = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
        return Instance;
    }

    private static AudioManager Instance;

    // Start is called before the first frame update
    private void Start()
    {
        ALevelCleared = LevelCleared.GetComponent<AudioSource>();
        ARoll = Rolling.GetComponent<AudioSource>();
        ABounce = Bounces.GetComponent<AudioSource>();
        ASquishes = Squishes.GetComponent<AudioSource>();
        AClock = Clock.GetComponent<AudioSource>();
        AKill = Kill.GetComponent<AudioSource>();

        ALost = Lost.GetComponent<AudioSource>();
        AWin = Win.GetComponent<AudioSource>();
        AMenu = Menu.GetComponent<AudioSource>();
        AGameplay = Gameplay.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PlayTrack(TrackType soundType)
    {
        // Stop any playing tracks
        List<AudioSource> tracks = new List<AudioSource>();
        tracks.Add(ALost);
        tracks.Add(AWin);
        tracks.Add(AMenu);
        tracks.Add(AGameplay);
        foreach (AudioSource track in tracks) {
            track.Stop();
        }

        // Play the new track
        AudioSource trackAudioSource = null;
        switch (soundType)
        {
            case TrackType.LOST:
                trackAudioSource = ALost;
                break;

            case TrackType.WIN:
                trackAudioSource = AWin;
                break;

            case TrackType.MENU:
                trackAudioSource = AMenu;
                break;

            case TrackType.GAMEPLAY:
                trackAudioSource = AGameplay;
                break;
        }
        trackAudioSource.Play();
    }

    public void PlaySound(SoundType soundType)
    {
        AudioSource audioSource = getAudioSource(soundType);
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void PlayOneShotSound(SoundType soundType)
    {
        AudioSource audioSource = getAudioSource(soundType);
        audioSource.Play();
    }

    public void StopSound(SoundType soundType)
    {
        AudioSource audioSource = getAudioSource(soundType);
        audioSource.Stop();
    }

    private AudioSource getAudioSource(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.LEVEL_CLEARED:
                return ALevelCleared;

            case SoundType.BOUNCE:
                return ABounce;

            case SoundType.ROLL:
                return ARoll;

            case SoundType.SQUISH:
                return ASquishes;

            case SoundType.CLOCK:
                return AClock;

            case SoundType.KILL:
                return AKill;
        }
        return null;
    }

    private AudioClip randomAudioClip(List<AudioClip> list)
    {
        return list[Random.Range(0, list.Count-1)];
    }
}
