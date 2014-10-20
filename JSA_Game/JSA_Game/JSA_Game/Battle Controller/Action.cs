﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Battle_Controller
{
    class Action
    {
        private String name, description;
        private StatType[] targetStat, statCost;
        private ActionType type;
        private Boolean ignoreEnemyStats, friendly, aoe;
        private int cost, range;
        private double powerMultipler;

        /// <summary>
        /// Describes any action that can be taken during a battle.
        /// </summary>
        /// <param name="name">Name of the action.</param>
        /// <param name="description">Description of the action</param>
        /// <param name="targetStat">Stat the action targets (typically Hp).</param>
        /// <param name="statCost">Stat that is used to perform this action (typically Mp or nothing).</param>
        /// <param name="type">Type of action, physical or spell.</param>
        /// <param name="ignoreEnemyStats">Determines whether or not the action ignores enemy stats.</param>
        /// <param name="friendly">Determines whether or not the action is friendly.</param>
        /// <param name="aoe">Determines whether or not the action is an area of effect action</param>
        /// <param name="powerMultiplier">Determines how much power the action has.</param>
        /// <param name="cost">How much of the type statCost that this move takes to perform.</param>
        /// <param name="range">Range of the action.</param>
        public Action(String name, String description, StatType[] targetStat, StatType[] statCost, ActionType type,
            Boolean ignoreEnemyStats, Boolean friendly, Boolean aoe, double powerMultiplier, int cost, int range)
        {
            this.name = name;
            this.description = description;
            this.targetStat = targetStat;
            this.statCost = statCost;
            this.type = type;
            this.ignoreEnemyStats = ignoreEnemyStats;
            this.friendly = friendly;
            this.aoe = aoe;
            this.powerMultipler = powerMultiplier;
            this.cost = cost;
            this.range = range;
        }

        /// <summary>
        /// Sets up the Attack action, which is the default.
        /// </summary>
        public Action()
        {
            this.name = "Attack";
            this.description = "A standard attack.";
            this.targetStat = new StatType[] { StatType.Hp };
            this.statCost = new StatType[] { StatType.Mp };
            this.type = ActionType.Physical;
            this.ignoreEnemyStats = false;
            this.friendly = false;
            this.aoe = false;
            this.powerMultipler = 1.0;
            this.cost = 0;
            this.range = 1;
        }

        // GETTERS AND SETTERS

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public StatType[] TargetStat
        {
            get { return targetStat; }
            set { targetStat = value; }
        }

        public StatType[] StatCost
        {
            get { return statCost; }
            set { statCost = value; }
        }

        public ActionType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Boolean IgnoreEnemyStats
        {
            get { return ignoreEnemyStats; }
            set { ignoreEnemyStats = value; }
        }

        public Boolean Friendly
        {
            get { return friendly; }
            set { friendly = value; }
        }

        public Boolean Aoe
        {
            get { return aoe; }
            set { aoe = value; }
        }

        public double PowerMultiplier
        {
            get { return powerMultipler; }
            set { powerMultipler = value; }
        }

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public int Range
        {
            get { return range; }
            set { range = value; }
        }
    }
}