using System;
using UnityEngine;

namespace CustomizableHeroStats
{
    // ModBehaviour will be instantiated and attached as a component in a container game object named <Your Mod Id>
    // Multiple ModBehaviours in your mod will share the same container.
    public class CustomizableHeroStats : ModBehaviour
    {
        //Singleton, used for accessing config settings from patches
        public static CustomizableHeroStats Instance { get; private set; }
        
        public CustomizableHeroStatsConfig config = new CustomizableHeroStatsConfig();

        //references that are updated each time a new run is started
        //used for updating UI components when settings are changed mid run
        public static UI_InGame_HeroDetailWindow heroDetailMenu;
        public static CanvasGroup stardustCanvasGroup;
        
        private void Awake()
        {
            Instance = this;
            harmony.PatchAll();
            Debug.Log("[" + mod.metadata.name + "] Hello! I'm loaded!");
        }

        private void OnDestroy()
        {
            harmony.UnpatchAll();
            if (Instance == this)
            {
                Instance = null;
            }
            Debug.Log("[" + mod.metadata.name + "] Good bye!");
        }

        public override void OnConfigChanged()
        {
            Debug.Log("[" + mod.metadata.name + "] Config changed!");
            UpdateAllHeroDetailComponents();

            //immediately changes visibility of stardust counter so that the player can see the change within the settings menu instead of waiting for it slowly appear/disappear
            if (stardustCanvasGroup != null)
            {
                Debug.Log("[CustomizableHeroStats] (UpdateComponents.Stardust) Updating stardust visibility");
                stardustCanvasGroup.alpha = config.alwaysShowStardust ? 1f : 0f;
            }
        }

        public void UpdateAllHeroDetailComponents()
        {
            Debug.Log(heroDetailMenu);
            if (heroDetailMenu != null)
            {
                Debug.Log("[CustomizableHeroStats] (UpdateHeroDetailComponents) Updating menus to fit new settings");
                
                //lock button state and visibility
                heroDetailMenu.alwaysShowToggle.gameObject.SetActive(config.showToggle);
                
                //Background shadow and box
                UpdateComponentActiveState("Shadow", config.showShadow);
                UpdateComponentActiveState("Background", config.showBackground);
                
                //border
                UpdateComponentActiveState("Window Deco - Top Half", config.borderType == Border.Small);
                UpdateComponentActiveState("Window Deco - Big Generic", config.borderType == Border.Large);
                
                //stats
                UpdateComponentActiveState("Health", config.showHealth);
                UpdateComponentActiveState("Armor", config.showArmor);
                UpdateComponentActiveState("AD", config.showAttackDamage);
                UpdateComponentActiveState("AP", config.showAbilityPower);
                UpdateComponentActiveState("Attack Speed", config.showAttackSpeed);
                UpdateComponentActiveState("Skill Haste", config.showSkillHaste);
                UpdateComponentActiveState("Critical Strike Chance", config.showCriticalStrikeChance);
                UpdateComponentActiveState("Fire Amp", config.showFireAmp);
                UpdateComponentActiveState("Movement Speed", config.showMovementSpeed);
            }
        }
        
        public void UpdateComponentActiveState(string childName, bool isActive)
        {
            Transform childComponent = heroDetailMenu.transform.Find(childName);

            if (childComponent != null)
            {
                childComponent.gameObject.SetActive(isActive);
            }
        }
    }
}