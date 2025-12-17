using UnityEngine;

namespace CustomizableHeroStats
{
    public enum Border
    {
        None = 0,
        Small = 1,
        Large = 2
    }
    
    public class CustomizableHeroStatsConfig : ModConfig
    {
        [Header("General Settings")]
        [LabelText("Show Hero Stats at Start")] [Description("Hero stats will start open at the start of each run. You can still toggle them off mid run.")]
        public bool startOpen = true;
        
        [LabelText("Always Show Stardust")] [Description("Always keep the amount stardust visible under the player's health bar.")]
        public bool alwaysShowStardust = true;
        
        [Space(40)]
        
        [Header("Hero Stats Cosmetics")]
        [LabelText("Shadow")] [Description("Show a shadow behind the hero stats")]
        public bool showShadow = true;
        
        [LabelText("Background")] [Description("Show a background box image behind the hero stats")]
        public bool showBackground = true;
        
        [LabelText("Border")] [Description("Show a border around the hero stats")]
        public Border borderType = Border.Small;
        
        [LabelText("Lock Button")] [Description("Show toggle lock icon on the top right of hero stats.")]
        public bool showToggle = true;
        
        [Space(40)]

        [Header("Individual Stats")]
        [LabelText("Health")] [Description("Show health stat.")]
        public bool showHealth = true;
        
        [LabelText("Armor")] [Description("Show armor stat.")]
        public bool showArmor = true;
        
        [LabelText("Attack Damage")] [Description("Show attack damage stat.")]
        public bool showAttackDamage = true;
        
        [LabelText("Ability Power")] [Description("Show ability power stat.")]
        public bool showAbilityPower = true;
        
        [LabelText("Attack Speed")] [Description("Show attack speed stat.")]
        public bool showAttackSpeed = true;
        
        [LabelText("Skill Haste")] [Description("Show skill haste stat.")]
        public bool showSkillHaste = true;
        
        [LabelText("Critical Strike Chance")] [Description("Show critical strike chance stat.")]
        public bool showCriticalStrikeChance = true;
        
        [LabelText("Fire Amp")] [Description("Show fire amp stat.")]
        public bool showFireAmp = true;
        
        [LabelText("Movement Speed")] [Description("Show movement speed stat.")]
        public bool showMovementSpeed = true;
    }
}