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

            //Listen for input to move cursor
            level.Cursor.moveCursor(gameTime);

            //Confirm attack
            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed)
            {
                System.Diagnostics.Debug.Print("Confirmed attack.");
                if (level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant != null)
                {
                    if (level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant.IsEnemy && level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].IsSelected)
                    {
                        Character c = level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant;
                        Character e = level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant;

                        if (BattleController.isValidAction(c.Attack, c, level.SelectedPos, level.Cursor.CursorPos))
                        {
                            System.Diagnostics.Debug.Print("Enemy HP is " + e.CurrHp);
                            BattleController.performAction(c.Attack, c, e);
                            System.Diagnostics.Debug.Print("Enemy HP now is " + e.CurrHp);
                        }

                        if (e.CurrHp < 1)
                        {
                            c.CurrExp += e.yieldExp();
                            level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].IsOccupied = false;
                            level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant = null;
                            level.EUnits.Remove(e);
                        }
                        c.ActionDisabled = true;
                        level.scanForTargets(false, level.SelectedPos, c.Attack.Range);
                        level.State = LevelState.CursorSelection;

                        //Check for win
                        if (level.EUnits.Count <= 0)
                        {
                            System.Diagnostics.Debug.Print("Player Won!");
                            level.WinState = WinLossState.Win;
                        }
                    }
                }
            }
            else if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.scanForTargets(false, level.SelectedPos, level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Attack.Range);
            }

        }
    }
}
