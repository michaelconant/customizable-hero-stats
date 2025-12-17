using HarmonyLib;
using UnityEngine;

namespace CustomizableHeroStats.Patches
{
    public class UI_InGame_StardustGroup_Patches
    {
        //Used to save reference to Stardust CanvasGroup in main class so that the OnConfigChanged function can update the display immediately instead of slowly fading away
        [HarmonyPatch(typeof(UI_InGame_StardustGroup))]
        [HarmonyPatch("Start")]
        public class UI_InGame_StardustGroup_Patch_Start
        {
            static void Postfix(UI_InGame_StardustGroup __instance, ref CanvasGroup ____cg)
            {
                CustomizableHeroStats.stardustCanvasGroup =  ____cg;
            }
        }
        
        
        //Manually changes the alpha to be fully opaque if the user chose to always show it
        [HarmonyPatch(typeof(UI_InGame_StardustGroup))]
        [HarmonyPatch("FrameUpdate")]
        public class UI_InGame_StardustGroup_Patch_FrameUpdate
        {
            static bool Prefix(UI_InGame_StardustGroup __instance, ref CanvasGroup ____cg)
            {
                //if the setting to always show stardust is on manually set alpha to 1 and skip original method
                if (CustomizableHeroStats.configStatic.alwaysShowStardust)
                {
                    ____cg.alpha = 1f;
                    return false;
                }

                return true;
            }
        }
    }
}