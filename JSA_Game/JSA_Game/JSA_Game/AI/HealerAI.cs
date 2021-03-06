﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;
using JSA_Game.CharClasses;
using JSA_Game.Battle_Controller;

namespace JSA_Game.AI
{
    class HealerAI : iAI
    {
        private Level currLevel;
        private Boolean friendly;
        private const int FRIENDLY_CHANCE = 80;

        Character character;
        Vector2 targetPos;

        // AI knows what character it is and its surroundings.
        public HealerAI(Character c, Level currentLevel)
        {
            character = c;
            currLevel = currentLevel;
            targetPos = new Vector2(-1, -1);
            Console.WriteLine(character.ClassName);
            performFriendly();
        }

        public void move(GameTime gameTime)
        {
            switch (character.ClassName)
            {
                case "Cleric":
                    clericMove(gameTime);
                    break;
                default:
                    generalMove(gameTime);
                    break;
            }
        }

        private void generalMove(GameTime gameTime)
        {
            //Picks closest target
            int dist;
            int shortestDist = 64;
            targetPos = new Vector2(-1, -1);
            ArrayList targetList;
            if (friendly)
            {
                targetList = getFriendlyTargets();
            }
            else
            {
                targetList = getEnemyTargets();
            }
            foreach (Character t in targetList)
            {
                dist = Maps.AStar.calcDist(character.Pos, t.Pos);
                if (dist < shortestDist && dist <= character.Movement)
                {
                    shortestDist = dist;
                    targetPos = t.Pos;
                }
            }

            //Move towards target if found
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                currLevel.moveUnit(gameTime, character.Pos, targetPos, true, true);
            }
        }

        private void clericMove(GameTime gameTime)
        {
            int dist;
            int shortestDist = 128;
            targetPos = new Vector2(-1 - 1);
            ArrayList targetList = getFriendlyTargets();
            foreach (Character c in targetList)
            {
                if ((c.CurrHp / (float)c.MaxHP) * 100 < 50.0)
                {
                    dist = Maps.AStar.calcDist(character.Pos, c.Pos);
                    Console.WriteLine("Dist: " + dist);
                    if (dist < shortestDist && dist <= character.Movement + character.Actions[0].Range)
                    {
                        shortestDist = dist;
                        targetPos = c.Pos;
                    }
                }
            }


            //Move towards target if found
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                currLevel.moveUnit(gameTime, character.Pos, targetPos, true, false);
            }
        }

        public void action()
        {
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                if (Maps.AStar.calcDist(character.Pos, targetPos) <= character.Actions[0].Range)
                {
                    currLevel.attackTarget(character.Pos, targetPos, character.Actions[0]);
                    Console.WriteLine("Healed!");
                }
            }
        }

        public Level CurrLevel
        {
            get { return currLevel; }
            set { currLevel = value; }
        }

        private void performFriendly()
        {
            if (!hasFriendlyMove())
            {
                friendly = false;
                return;
            }

            Random rand = new Random();
            int value = rand.Next(0, 100);

            friendly = value <= FRIENDLY_CHANCE;
        }

        private ArrayList getFriendlyTargets()
        {
            if (character.IsEnemy)
            {
                return currLevel.EUnits;
            }
            else
            {
                return currLevel.PUnits;
            }
        }

        private ArrayList getEnemyTargets()
        {
            if (character.IsEnemy)
            {
                return currLevel.PUnits;
            }
            else
            {
                return currLevel.EUnits;
            }
        }

        private Boolean hasFriendlyMove()
        {
            foreach (Battle_Controller.Action action in character.Actions)
            {
                if (action.Friendly)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
