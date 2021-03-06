﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using JSA_Game.Maps;

namespace JSA_Game.Screens
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class PauseMenuScreen : MenuScreen
    {

        InputAction escape;
        Level level;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PauseMenuScreen(Level level)
            : base("Paused")
        {
            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry toTownMenuEntry = new MenuEntry("To Town (Cheat)");
            MenuEntry nextLevelMenuEntry = new MenuEntry("Next Level (Cheat)");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            toTownMenuEntry.Selected += toTownMenuEntrySelected;
            nextLevelMenuEntry.Selected += nextLevelMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(toTownMenuEntry);
            MenuEntries.Add(nextLevelMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);

            escape = new InputAction(null, new Keys[] { Keys.Escape }, true);
            this.level = level;
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
            PlayerIndex playerIndex;
            if (escape.Evaluate(input, ControllingPlayer, out playerIndex))
            {
                ExitScreen();
            }


        }

        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Quit game?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        void toTownMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Game1.resetPlayerChars(level.PUnits);
            level.WinState = WinLossState.Win;
            ExitScreen();
            //ScreenManager.AddScreen(new TownBackgroundScreen(), e.PlayerIndex);
            //ScreenManager.AddScreen(new TownScreen(), e.PlayerIndex);
        }
        


        void nextLevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            Game1.resetPlayerChars(level.PUnits);
            string levelName = Game1.getNextLevelName();
            if (levelName.Equals(""))
            {
                //No more levels left
                const string message = "No more levels left";
                MessageBoxScreen noMoreLevelsMessageBox = new MessageBoxScreen(message, false);
                ScreenManager.AddScreen(noMoreLevelsMessageBox, null);
            }
            else
            {
                ScreenManager.AddScreen(new LevelScreen(levelName, ScreenManager), e.PlayerIndex);
            }

        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            foreach (GameScreen screen in ScreenManager.GetScreens())
                screen.ExitScreen();
            Game1.resetLevels();
            ScreenManager.AddScreen(new MainMenuBackgroundScreen(), null);
            ScreenManager.AddScreen(new MainMenuScreen(), null);
        }

    }
}
