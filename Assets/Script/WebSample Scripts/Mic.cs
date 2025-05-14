using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mic : MonoBehaviour
{
    AudioClip record;
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        // RecSnd();
        // Invoke("PlaySnd", 6f);
    }

    public void PlaySnd()
    {
        aud.Play();
        SavWav.Save("/Users/yeongmu/Documents/GitHub/Midnight-Team-1/Assets/Resources/sample", aud.clip); // 저장 기능, Test라는 이름으로 저장된다
    }

    public void RecSnd()
    {
    	// 디바이스 확인용 코드로, 생략해도 된다
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        record = Microphone.Start(Microphone.devices[0].ToString(), false, 3, 44100); // 3초 녹음
        aud.clip = record;

    }
}