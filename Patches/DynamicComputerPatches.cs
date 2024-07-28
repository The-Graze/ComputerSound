using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ComputerSound.Patches
{
    [HarmonyPatch(typeof(GorillaComputerTerminal))]
    [HarmonyPatch("Init", MethodType.Normal)]
    internal class DynamicComputerPatch0
    {
        private static void Postfix(GorillaComputerTerminal __instance)
        {
            AudioSource auds = __instance.gameObject.AddComponent<AudioSource>();
            auds.volume = Plugin.FanVol.Value;
            auds.maxDistance = 0.2f;
            auds.clip = Plugin.FanSound;
            auds.loop = true;
            auds.Play();
        }
    }
    [HarmonyPatch(typeof(GorillaComputerTerminal))]
    [HarmonyPatch("OnDisable", MethodType.Normal)]
    internal class DynamicComputerPatch1
    {
        private static void Postfix(GorillaComputerTerminal __instance)
        {
            Plugin.Destroy(__instance.gameObject.GetComponent<AudioSource>());
        }
    }
}
