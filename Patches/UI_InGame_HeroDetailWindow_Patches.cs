using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace CustomizableHeroStats.Patches
{
    [HarmonyPatch(typeof(UI_InGame_HeroDetailWindow))]
    public static class UI_InGame_HeroDetailWindow_Patches
    {
        private static readonly MethodInfo UpdateTextMethod = typeof(UI_InGame_HeroDetailWindow).GetMethod("UpdateText", BindingFlags.Instance | BindingFlags.NonPublic);
        
        //Patches the initialization of HeroDetailWindow to start with the right elements enabled/disabled.
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void Awake_Postfix(UI_InGame_HeroDetailWindow __instance)
        {
            Debug.Log("[CustomizableHeroStats] Initializing HeroDetailWindow");
        
            CustomizableHeroStats.heroDetailMenu = __instance;
            
            //Not included in the UpdateHeroDetailComponents because the menu should not appear/disapper when changing settings in the middle of the game
            CustomizableHeroStats.heroDetailMenu.alwaysShowToggle.isChecked = CustomizableHeroStats.Instance.config.startOpen;
            
            CustomizableHeroStats.Instance?.UpdateAllHeroDetailComponents();
        }
        
        
        //Fixes replaces the LogicUpdate method so that controller input doesn't close the menu.
        [HarmonyPatch("LogicUpdate")]
        [HarmonyPrefix]
        public static bool LogicUpdate_Prefix(UI_InGame_HeroDetailWindow __instance)
        {
            //skip update text if player is missing or dead
            if (DewPlayer.local == null || DewPlayer.local.hero == null)
            {
                return false;
            }
            
            //update text if menu is on
            if (__instance.alwaysShowToggle.isChecked)
            {
                UpdateTextMethod.Invoke(__instance, null);
            }
            
            //skip original method
            return false;
        }
    }
}