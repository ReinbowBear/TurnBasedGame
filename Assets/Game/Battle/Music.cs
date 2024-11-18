using System;
using UnityEngine;

public class Music : MonoBehaviour
{
    private System.Random random;

    [SerializeField] private AudioSource audioSource;
    [Space]
    [SerializeField] private AudioClip[] sounds;

    void Awake()
    {
        random = new System.Random(DateTime.Now.Millisecond);
    }

    void Start()
    {
        int randomID = random.Next(0, sounds.Length);

        audioSource.PlayOneShot(sounds[randomID]);
    }
}
