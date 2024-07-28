using BepInEx;
using BepInEx.Configuration;
using Photon.Voice.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Utilla;

namespace ComputerSound
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        Plugin() => StartUp();

        public static ConfigEntry<float> FanVol;

        public static AudioClip FanSound;
        void StartUp()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            FanVol = Config.Bind("Settings", "Volume", 0.02f, "Computer fan Volume");
            StartCoroutine(GetAudioClip());
        }
        IEnumerator GetAudioClip()
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://github.com/The-Graze/ComputerSound/raw/master/computer.ogg", AudioType.OGGVORBIS))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    FanSound = DownloadHandlerAudioClip.GetContent(www);
                }
            }
        }
    }
}
