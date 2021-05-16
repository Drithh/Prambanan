using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(VolumeIncrease());
    }
    
    IEnumerator VolumeIncrease()
    {
        AudioSource audio = transform.GetComponent<AudioSource>();
        while (audio.volume < 0.1f)
        {
            audio.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
