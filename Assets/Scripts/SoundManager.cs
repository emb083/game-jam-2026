using System.Collections.Generic;
using UnityEngine;

public enum SoundType {
    CUSTOMER_ENTER,
    CUSTOMER_ORDER,
    GRAB,
    ORDER_RIGHT,
    ORDER_WRONG,
    ORDER_MISSED_HUMAN,
    ORDER_MISSED_ALIEN,
    NO_ITEM_HUMAN,
    NO_ITEM_ALIEN,
    TIME_LOW,
    BUTTON
}

public class SoundCollection {
  private AudioClip[] clips;
  private int lastClipIndex;

  public SoundCollection(params string[] clipNames) {
    this.clips = new AudioClip[clipNames.Length];
    for (int i = 0; i < clipNames.Length; i++) {
      clips[i] = Resources.Load<AudioClip>(clipNames[i]);
    }
    lastClipIndex = -1;
  }

  public AudioClip GetRandClip() {
    if (clips.Length == 1) {
      return clips[0];
    }
    else {
      int index = lastClipIndex;
      while (index == lastClipIndex) {
        index = Random.Range(0, clips.Length);
      }
      lastClipIndex = index;
      return clips[index];
    }
  }
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
  public float mainVolume = 1.0f;
  private Dictionary<SoundType, SoundCollection> sounds;
  private AudioSource audioSrc;

  public static SoundManager Instance { get; private set; }

  private void Awake() {
    Instance = this;
    audioSrc = GetComponent<AudioSource>();
    sounds = new() {
        {SoundType.CUSTOMER_ENTER, new SoundCollection("doorbell")},
        {SoundType.CUSTOMER_ORDER, new SoundCollection("typing")},
        {SoundType.GRAB, new SoundCollection("pickup")},
        {SoundType.ORDER_RIGHT, new SoundCollection("rattle")},
        {SoundType.ORDER_WRONG, new SoundCollection("drop")},
        {SoundType.ORDER_MISSED_HUMAN, new SoundCollection("missed-human")},
        {SoundType.ORDER_MISSED_ALIEN, new SoundCollection("missed-alien")},
        {SoundType.NO_ITEM_HUMAN, new SoundCollection("hm-human")},
        {SoundType.NO_ITEM_ALIEN, new SoundCollection("hm-alien")},
        {SoundType.TIME_LOW, new SoundCollection("ticking")},
        {SoundType.BUTTON, new SoundCollection("button")}
    };
  }

  public static void Play(SoundType type, AudioSource audioSrc = null, float pitch = -1) {
    if (Instance.sounds.ContainsKey(type)) {
      audioSrc ??= Instance.audioSrc;
      audioSrc.volume = Random.Range(0.7f, 1.0f) * Instance.mainVolume;
      audioSrc.pitch = pitch >= 0 ? pitch : Random.Range(0.75f, 1.25f);
      audioSrc.clip = Instance.sounds[type].GetRandClip();
      audioSrc.PlayOneShot(audioSrc.clip);
    }
  }
}