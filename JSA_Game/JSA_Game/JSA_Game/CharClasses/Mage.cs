﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;

namespace JSA_Game.CharClasses
{
    class Mage : Character
    {
        public Mage(Level level, int startingLevel = 1)
        {
            charLevel = startingLevel;
            Texture = "Mage";
            name = "Mage";
            className = "Mage";
            AI = new AggressiveAI(this, level);

            MaxHP = STANDARD_HPMP;
            MaxMP = STRONG_HPMP;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = WEAK_STAT;
            Armor = WEAK_STAT;
            Accuracy = STANDARD_STAT;
            Dodge = STANDARD_STAT;
            Magic = STRONG_STAT;
            Resist = STANDARD_STAT;

            Battle_Controller.Action actionFireball = new Battle_Controller.Action("Fireball", "Blast the enemy with a raging fireball.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.0, 3, 5);
            Actions[0] = actionFireball;

            Battle_Controller.Action actionShock = new Battle_Controller.Action("Shock",
                "Call down a lightning storm, affecting all enemies in range.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, true, 1.0, 4, 3);
            Actions[1] = actionShock;

            Battle_Controller.Action actionSlow = new Battle_Controller.Action("Slow", "Slows the enemy, lowering dodge and movement.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.0, 3, 5);
            Actions[2] = actionSlow;
        }
    }
}