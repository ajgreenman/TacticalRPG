﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JSA_Game.Battle_Controller;
using JSA_Game.Maps;

namespace JSA_Game.Maps.State
{
    class ActionState
    {
        public static void update(Level level, GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);
            Vector2 oldPos = new Vector2(level.Cursor.CursorPos.X, level.Cursor.CursorPos.Y);

            if (level.MoveTimeElapsed >= level.MoveDelay)   // A unit is selected
            {
                //Scroll board if necessary
                if (keyboard.IsKeyDown(Keys.Left) && level.Cursor.CursorPos.X == 1 && level.ShowStartX != 0)
                {
                    level.ShowStartX--;
                }
                else if (keyboard.IsKeyDown(Keys.Right) && level.Cursor.CursorPos.X == level.NumTilesShowing - 2 && level.ShowStartX + level.NumTilesShowing != level.BoardWidth)
                {
                    level.ShowStartX++;
                }
                else if (keyboard.IsKeyDown(Keys.Up) && level.Cursor.CursorPos.Y == 1 && level.ShowStartY != 0)
                {
                    level.ShowStartY--;
                }
                else if (keyboard.IsKeyDown(Keys.Down) && level.Cursor.CursorPos.Y == level.NumTilesShowing - 2 && level.ShowStartY + level.NumTilesShowing != level.BoardHeight)
                {
                    level.ShowStartY++;
                }
                else if ((keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
                    && level.Cursor.CursorPos.X != level.ShowStartX + level.NumTilesShowing && level.Cursor.CursorPos.Y != level.ShowStartY + level.NumTilesShowing)
                {
                    //Listen for input to move cursor.  If it moved, update aoe range if present
                   
                }
                if (level.Cursor.moveCursor(gameTime) && level.SelectedAction != null)
                {

                    if (level.SelectedAction.Aoe) //If the action is an aoe action
                    {
                        //Clear old aoe range
                        level.scanForTargets(false, oldPos, level.SelectedAction.AoeRange, true);

                        level.scanForTargets(true, level.SelectedPos, level.SelectedAction.Range);
                        //Show new aoe range
                        //System.Diagnostics.Debug.Print("Showing aoe range");
                         level.scanForTargets(true, level.Cursor.CursorPos, level.SelectedAction.AoeRange, true);
                    }
                }
                level.MoveTimeElapsed = 0;
            }

            //Confirm attack
            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed)
            {
                int x = (int)level.Cursor.CursorPos.X + level.ShowStartX;
                int y = (int)level.Cursor.CursorPos.Y + level.ShowStartY;

                

                if (level.Board[x, y].Occupant != null)
                {
                    if (level.Board[x, y].Occupant.IsEnemy && level.Board[x, y].IsSelected)
                    {
                        level.TargetList.Add(level.Board[x, y].Occupant);
                    }

                }

                if (level.TargetList.Count > 0)
                {
                    System.Diagnostics.Debug.Print("Num targets: " + level.TargetList.Count);
                    //Here apply attack to all aoe targets
                    Character[] targetList = new Character[level.TargetList.Count];

                    for (int i = 0; i < level.TargetList.Count; i++)
                    {
                        targetList[i] = (Character) level.TargetList[i];
                    }
                    for (int i = 0; i < level.TargetList.Count; i++)
                    {
                        Character c = targetList[i];
                    
                        level.attackTarget(level.SelectedPos, c.Pos, level.SelectedAction);
                    }

                    //if (level.Board[x, y].Occupant.IsEnemy && level.Board[x, y].IsSelected)
                    //{


                    //    //JSA_Game.Battle_Controller.Action action = level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Attack;
                    //    level.attackTarget(level.SelectedPos, new Vector2(x, y), level.SelectedAction);

                        
                   // }
                    level.State = LevelState.CursorSelection;
                    level.scanForTargets(false, level.Cursor.CursorPos, level.SelectedAction.AoeRange);
                }
            }
            else if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.scanForTargets(false, level.SelectedPos, level.SelectedAction.Range);
                if (level.SelectedAction != null)
                {
                    level.scanForTargets(false, level.Cursor.CursorPos, level.SelectedAction.AoeRange, true);
                }
            }
        }
    }
}
