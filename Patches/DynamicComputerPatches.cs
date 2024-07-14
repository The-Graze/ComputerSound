using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ConputerSound.Patches
{
    [HarmonyPatch(typeof(GorillaComputerTerminal))]
    [HarmonyPatch("Init", MethodType.Normal)]
    internal class DynamicComputerPatch0
    {
        private static void Postfix(GorillaComputerTerminal __instance)
        {
           Plugin.ComputerSources.Add(__instance.gameObject.AddComponent<AudioSource>());

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
