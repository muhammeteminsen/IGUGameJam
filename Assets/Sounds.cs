using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
  public List<AudioSource> sounds;
  public void PlaySound(string soundName)
{
  foreach (var sound in sounds)
  {
    if (sound.name == soundName)
    {
      sound.Play();
      return;
    }
  }
}
}

