using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace CustomizableHeroStats
{
    // ModBehaviour will be instantiated and attached as a component in a container game object named <Your Mod Id>
    // Multiple ModBehaviours in your mod will share the same container.
    public class CustomizableHeroStats : ModBehaviour
    {
        public CustomizableHeroStatsConfig config = new CustomizableHeroStatsConfig();
        public static CustomizableHeroStatsConfig configStatic;

        public static UI_InGame_HeroDetailWindow heroDetailMenu;
        
        private void Awake()
        {
            // Metadata of your mod is stored in this.about
            Debug.Log("[" + mod.metadata.name + "] Hello! I'm loaded!");
            
            configStatic = config;
            
            // If you need to patch with Harmony, you can use this.harmony to access the Harmony instance for your mod.
            // It will be created with your mod's id automatically, the first time you access the property.
            harmony.PatchAll();
        }

        private void OnDestroy()
        {
            // Make sure you clean up properly to support Live Reload.
            Debug.Log("[" + mod.metadata.name + "] Good bye!");
            harmony.UnpatchAll();
        }

        public override void OnConfigChanged()
        {
            Debug.Log("[" + mod.metadata.name + "] Config changed!");
            configStatic = config;
            UpdateComponents();
        }

        public static void UpdateComponents()
        {
            heroDetailMenu.alwaysShowToggle.isChecked = CustomizableHeroStats.configStatic.startOpen;
            heroDetailMenu.alwaysShowToggle.gameObject.SetActive(CustomizableHeroStats.configStatic.showToggle);
            
            try
            {
                Debug.Log("[CustomizableHeroStats] Attempting to customize menu on awake!");
                
                //shadow
                heroDetailMenu.transform.GetChild(0).gameObject.SetActive(CustomizableHeroStats.configStatic.showShadow);
                
                //background
                heroDetailMenu.transform.GetChild(1).gameObject.SetActive(CustomizableHeroStats.configStatic.showBackground);
                
                //border
                switch (CustomizableHeroStats.configStatic.borderType)
                {
                    case Border.None:
                        heroDetailMenu.transform.GetChild(2).gameObject.SetActive(false);
                        heroDetailMenu.transform.GetChild(3).gameObject.SetActive(false);
                        break;
                    case Border.Small:
                        heroDetailMenu.transform.GetChild(2).gameObject.SetActive(false);
                        heroDetailMenu.transform.GetChild(3).gameObject.SetActive(true);
                        break;
                    case Border.Large:
                        heroDetailMenu.transform.GetChild(2).gameObject.SetActive(true);
                        heroDetailMenu.transform.GetChild(3).gameObject.SetActive(false);
                        break;
                }
                
                //stats
                heroDetailMenu.transform.GetChild(4).gameObject.SetActive(CustomizableHeroStats.configStatic.showHealth);
                heroDetailMenu.transform.GetChild(5).gameObject.SetActive(CustomizableHeroStats.configStatic.showArmor);
                heroDetailMenu.transform.GetChild(6).gameObject.SetActive(CustomizableHeroStats.configStatic.showAttackDamage);
                heroDetailMenu.transform.GetChild(7).gameObject.SetActive(CustomizableHeroStats.configStatic.showAbilityPower);
                heroDetailMenu.transform.GetChild(8).gameObject.SetActive(CustomizableHeroStats.configStatic.showAttackSpeed);
                heroDetailMenu.transform.GetChild(9).gameObject.SetActive(CustomizableHeroStats.configStatic.showSkillHaste);
                heroDetailMenu.transform.GetChild(10).gameObject.SetActive(CustomizableHeroStats.configStatic.showCriticalStrikeChance);
                heroDetailMenu.transform.GetChild(11).gameObject.SetActive(CustomizableHeroStats.configStatic.showFireAmp);
                heroDetailMenu.transform.GetChild(12).gameObject.SetActive(CustomizableHeroStats.configStatic.showMovementSpeed);
                
                Debug.Log("[CustomizableHeroStats] SUCCESSFULLY customize menu on awake!");
            }
            catch
            {
                Debug.Log("[CustomizableHeroStats] FAILED to customize menu on awake.");
            }
        }
    }
}