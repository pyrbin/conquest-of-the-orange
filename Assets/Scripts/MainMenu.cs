using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        AudioManager.Find().PlayTrack(AudioManager.TrackType.MENU);
    }
}
