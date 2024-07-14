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

namespace ConputerSound
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        Plugin() => StartUp();

        ConfigEntry<float> FanVol;

        public static AudioClip FanSound;

        string audioPath;
        void StartUp()
        {
            
            HarmonyPatches.ApplyHarmonyPatches();
            FanVol = Config.Bind("Settings", "Volume", 0.2f, "Computer fan Volume");
            audioPath = Paths.PluginPath + "/computer.ogg";
            if (File.Exists(audioPath)) 
            {
                FanSound = AudioClip.Create(audioPath);
            }
            else
            {

            }
        }
        IEnumerator GetAudioClip()
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://www.my-server.com/audio.ogg", AudioType.OGGVORBIS))
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
