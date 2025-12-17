using UnityEngine;
using HarmonyLib;

namespace CustomizableHeroStats.Patches
{
    //Patches the initialization of HeroDetailWindow to start with the right elements enabled/disabled.
    [HarmonyPatch(typeof(UI_InGame_HeroDetailWindow))]
    [HarmonyPatch("Awake")]
    public class Patch_UI_InGame_HeroDetailWindow_Awake
    {
        static void Postfix(UI_InGame_HeroDetailWindow __instance)
        {
            Debug.Log("[CustomizableHeroStats] Initializing HeroDetailWindow");
            
            CustomizableHeroStats.heroDetailMenu = __instance;
            CustomizableHeroStats.UpdateComponents();
        }
    }
}