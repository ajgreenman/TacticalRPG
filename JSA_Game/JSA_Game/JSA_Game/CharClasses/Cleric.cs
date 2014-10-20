﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;

namespace JSA_Game.CharClasses
{
    class Cleric : Character
    {
        public Cleric(Level level)
        {
            AI = new AggressiveAI(this, level);
            Texture = "player";

            MaxHP = STANDARD_STAT;
            MaxMP = STRONG_STAT;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = STANDARD_STAT;
            Armor = WEAK_STAT;
            Accuracy = STANDARD_STAT;
            Dodge = STANDARD_STAT;
            Magic = STRONG_STAT;
            Resist = WEAK_STAT;

            Battle_Controller.Action actionHeal = new Battle_Controller.Action("Heal", "Heal a friendly unit.",
                new StatType[] { StatType.Hp },
                new StatType[] { StatType.Mp }, ActionType.Spell, false, true, false, 1.0, 3, 5);
            Actions[0] = actionHeal;

            Battle_Controller.Action actionShield = new Battle_Controller.Action("Shield", "Shield a friendly unit.",
                new StatType[] { StatType.Armor, StatType.Resist },
                new StatType[] { StatType.Mp }, ActionType.Spell, false, true, false, 1.0, 2, 5);
            Actions[1] = actionShield;

            Battle_Controller.Action actionJudgment = new Battle_Controller.Action("Judgment", "Smite the enemy.",
                new StatType[] { StatType.Hp },
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.0, 3, 5);
            Actions[2] = actionJudgment;
        }
    }
}
