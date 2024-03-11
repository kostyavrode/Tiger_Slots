using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSoundChecker : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        if (PlayerPrefs.GetString("Sound")=="true")
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
