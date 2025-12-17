using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace CustomizableHeroStats.Patches
{
    public class UI_InGame_HeroDetailWindow_Patches
    {
        //Patches the initialization of HeroDetailWindow to start with the right elements enabled/disabled.
        [HarmonyPatch(typeof(UI_InGame_HeroDetailWindow))]
        [HarmonyPatch("Awake")]
        public class UI_InGame_HeroDetailWindow_Patch_Awake
        {
            static void Postfix(UI_InGame_HeroDetailWindow __instance)
            {
                Debug.Log("[CustomizableHeroStats] Initializing HeroDetailWindow");
            
                CustomizableHeroStats.heroDetailMenu = __instance;
                CustomizableHeroStats.UpdateHeroDetailComponents();
            }
        }
        
        
        //Fixes replaces the LogicUpdate method so that controller input doesn't close the menu.
        [HarmonyPatch(typeof(UI_InGame_HeroDetailWindow))]
        [HarmonyPatch("LogicUpdate")]
        public class UI_InGame_HeroDetailWindow_Patch_Update
        {
            private static readonly MethodInfo updateTextMethod = typeof(UI_InGame_HeroDetailWindow).GetMethod("UpdateText", BindingFlags.Instance | BindingFlags.NonPublic);
            
            static bool Prefix(UI_InGame_HeroDetailWindow __instance)
            {
                //skip update text if player is missing or dead
                if (DewPlayer.local == null || DewPlayer.local.hero == null)
                {
                    return false;
                }
                
                //update text if menu is on
                if (__instance.alwaysShowToggle.isChecked)
                {
                    updateTextMethod?.Invoke(__instance, null);
                }
                
                //skip original method
                return false;
            }
        }
    }
}