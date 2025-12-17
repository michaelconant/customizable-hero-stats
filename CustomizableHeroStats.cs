using System;
using UnityEngine;

namespace CustomizableHeroStats
{
    // ModBehaviour will be instantiated and attached as a component in a container game object named <Your Mod Id>
    // Multiple ModBehaviours in your mod will share the same container.
    public class CustomizableHeroStats : ModBehaviour
    {
        public CustomizableHeroStatsConfig config = new CustomizableHeroStatsConfig();
        //"public static" prevents the config from showing up in game, so I use this static duplicate to access config settings within patches
        public static CustomizableHeroStatsConfig configStatic;

        //references that are updated each time a new run is started
        //used for updating UI components when settings are changed mid run
        public static UI_InGame_HeroDetailWindow heroDetailMenu;
        public static CanvasGroup stardustCanvasGroup;
        
        private void Awake()
        {
            Debug.Log("[" + mod.metadata.name + "] Hello! I'm loaded!");
            configStatic = config;
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
            UpdateHeroDetailComponents();

            try
            {
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Stardust) Attempting to update stardust visibility");
                stardustCanvasGroup.alpha = CustomizableHeroStats.configStatic.alwaysShowStardust ? 1f : 0f;
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Stardust) SUCCESSFULLY updated stardust visibility!");
            }
            catch
            {
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Stardust) FAILED to update stardust visibility. Ignore this if you not currently in a run (the components don't exist yet).");
            }
        }

        public static void UpdateHeroDetailComponents()
        {
            Debug.Log("[CustomizableHeroStats] (UpdateHeroDetailComponents) Updating menus to fit new settings");
            
            //try to change
            try
            {
                //lock button state and visibility
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.LockButton) Attempting to update lock button state and visibility!");
                heroDetailMenu.alwaysShowToggle.isChecked = CustomizableHeroStats.configStatic.startOpen;
                heroDetailMenu.alwaysShowToggle.gameObject.SetActive(CustomizableHeroStats.configStatic.showToggle);
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.LockButton) SUCCESSFULLY updated lock button state and visibility!");
                
                
                //Background shadow and box
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Background) Attempting to update background shadow and box visibility!");
                heroDetailMenu.transform.GetChild(0).gameObject.SetActive(CustomizableHeroStats.configStatic.showShadow);
                heroDetailMenu.transform.GetChild(1).gameObject.SetActive(CustomizableHeroStats.configStatic.showBackground);
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Background) SUCCESSFULLY updated background shadow and box visibility!");
                
                
                //border
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Cosmetics) Attempting to update border");
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
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Cosmetics) SUCCESSFULLY updated border!");
                
                
                //stats
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.IndividualStats) Attempting to update individual stats!");
                heroDetailMenu.transform.GetChild(4).gameObject.SetActive(CustomizableHeroStats.configStatic.showHealth);
                heroDetailMenu.transform.GetChild(5).gameObject.SetActive(CustomizableHeroStats.configStatic.showArmor);
                heroDetailMenu.transform.GetChild(6).gameObject.SetActive(CustomizableHeroStats.configStatic.showAttackDamage);
                heroDetailMenu.transform.GetChild(7).gameObject.SetActive(CustomizableHeroStats.configStatic.showAbilityPower);
                heroDetailMenu.transform.GetChild(8).gameObject.SetActive(CustomizableHeroStats.configStatic.showAttackSpeed);
                heroDetailMenu.transform.GetChild(9).gameObject.SetActive(CustomizableHeroStats.configStatic.showSkillHaste);
                heroDetailMenu.transform.GetChild(10).gameObject.SetActive(CustomizableHeroStats.configStatic.showCriticalStrikeChance);
                heroDetailMenu.transform.GetChild(11).gameObject.SetActive(CustomizableHeroStats.configStatic.showFireAmp);
                heroDetailMenu.transform.GetChild(12).gameObject.SetActive(CustomizableHeroStats.configStatic.showMovementSpeed);
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.IndividualStats) SUCCESSFULLY updated individual stats!");
            }
            catch (Exception ex)
            {
                Debug.Log("[CustomizableHeroStats] (UpdateComponents) FAILED to update all components. Ignore this if you not currently in a run (the components don't exist yet).");
            }
            
            Debug.Log("[CustomizableHeroStats] (UpdateComponents) SUCCESSFULLY updated all components.");
        }
    }
}