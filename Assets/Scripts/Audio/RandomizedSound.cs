using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizedSound : MonoBehaviour
{
    public static string HURT = "Audio/Hurt";
    public static string ATTACK = "Audio/Attack";
    public static string MOVEMENT = "Audio/Movement";
    public static string SPOTTED = "Audio/Spotted";
    public WeightedAudioClip[] sounds;

    private static Dictionary<string, int> lastPlayedSlots = new Dictionary<string, int>();

    public static void TriggerUpdate()
    {
        foreach (var key in lastPlayedSlots.Keys.ToList())
        {
            lastPlayedSlots[key] = Mathf.Max(0, lastPlayedSlots[key] - 1);
        }
    }

    public void Play()
    {
        int totalWeight = 0;
        foreach (WeightedAudioClip randomizedSound in sounds)
        {
            totalWeight += randomizedSound.weight;
        }

        int chosenSound = Random.Range(0, totalWeight);
        totalWeight = 0;
        AudioSource source = GetComponent<AudioSource>();

        foreach (WeightedAudioClip randomizedSound in sounds)
        {
            if (totalWeight <= chosenSound && chosenSound < totalWeight + randomizedSound.weight)
            {
                source.Play();
                string name = randomizedSound.clip.name;
                if (!lastPlayedSlots.ContainsKey(name))
                {
                    lastPlayedSlots.Add(name, 0);
                }

                if (lastPlayedSlots[name] < 3)
                {
                    source.PlayOneShot(randomizedSound.clip);
                    lastPlayedSlots[name]++;
                }

                break;
            }

            totalWeight += randomizedSound.weight;
        }
    }

    public static void Play(Transform baseObject, string soundSet)
    {
        Transform hurt = baseObject.transform.Find(soundSet);
        if (hurt)
        {
            hurt.GetComponent<RandomizedSound>().Play();
        }
    }
}