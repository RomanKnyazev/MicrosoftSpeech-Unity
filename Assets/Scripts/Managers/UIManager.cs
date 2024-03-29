﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CognitiveServicesTTS;
using System;

namespace Assets
{
public sealed class UIManager : MonoBehaviour {

    public SpeechManager speech;
    public InputField input;
    public InputField pitch;
    public Toggle useSDK;
    public Dropdown voicelist;

    private void Start()
    {
        pitch.text = "0";

        List<string> voices = new List<string>();
        foreach (VoiceName voice in Enum.GetValues(typeof(VoiceName)))
        {
            voices.Add(voice.ToString());
        }
        voicelist.AddOptions(voices);
        voicelist.value = (int)VoiceName.enUSJessaRUS;
    }

    public void SpeechPlayback()
    {
        if (speech.isReady)
        {
            string msg = input.text;
            speech.VoiceName = (VoiceName)voicelist.value;
            speech.VoicePitch = int.Parse(pitch.text);
            if (useSDK.isOn)
            {
                speech.SpeakWithSDKPlugin(msg);
            }
            else
            {
                speech.SpeakWithRESTAPI(msg);
            }
        } else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

    public void ClearText()
    {
        input.text = "";
    }
}
}