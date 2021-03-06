﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JSA_Game.Screens
{
    class MainMenuScreen : MenuScreen
    {


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("JSA")
        {
            // Create our menu entries.
            MenuEntry newGameMenuEntry = new MenuEntry("New Game");
            MenuEntry loadGameMenuEntry = new MenuEntry("Load Game");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");
            MenuEntry testMenuEntry = new MenuEntry("Testing...");

            // Hook up menu event handlers.
            newGameMenuEntry.Selected += NewGameMenuEntrySelected;
            loadGameMenuEntry.Selected += loadGameMenuEntrySelected;
            exitMenuEntry.Selected += exitGameMenuEntrySelected;

            //Literally adding the function for when the item is selected.
            testMenuEntry.Selected += TestMenuEntrySelected;


            // Add entries to the menu.
            MenuEntries.Add(newGameMenuEntry);
            MenuEntries.Add(loadGameMenuEntry);
            MenuEntries.Add(exitMenuEntry);
            //MenuEntries.Add(testMenuEntry);

            // Sound
            Sound.PlaySound("title");
        }





        /// <summary>
        /// Event handler for when the New Game menu entry is selected.
        /// </summary>
        void NewGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            //First Level
            ScreenManager.AddScreen(new LevelScreen(Game1.getNextLevelName(), ScreenManager), e.PlayerIndex);

            //With loading screen
            //LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                //new LevelScreen(Game1.getNextLevelName()));


            //ScreenManager.AddScreen(new TownBackgroundScreen(), null);
           // ScreenManager.AddScreen(new TownScreen(), null);
        }


        /// <summary>
        /// Event handler for when the Load Game menu entry is selected.
        /// </summary>
        void loadGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new LoadGameScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Exit Game menu entry is selected.
        /// </summary>
        void exitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message, true);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, e.PlayerIndex);

            //ScreenManager.Game.Exit();
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            

            //ScreenManager.Game.Exit();
        }

        /// <summary>
        /// Event handler for when the Testing... menu entry is selected.
        /// </summary>
        void TestMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //const string message = "It's a test message box!";

            //MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message, false);

            //No extra events - just closes

            //ScreenManager.AddScreen(confirmExitMessageBox, null);
            /*
            List<JSA_Game.Character> testCharList = new List<JSA_Game.Character>();
            Character c = new JSA_Game.CharClasses.Warrior(null);
            c.Name = "Jimmy";
            testCharList.Add(c);

            testCharList.Add(new JSA_Game.CharClasses.Mage(null));
            testCharList.Add(new JSA_Game.CharClasses.Mage(null, 2));
            testCharList.Add(new JSA_Game.CharClasses.Warrior(null,3));
            testCharList.Add(new JSA_Game.CharClasses.Warrior(null));
            testCharList.Add(new JSA_Game.CharClasses.Mage(null));
            testCharList.Add(new JSA_Game.CharClasses.Mage(null, 2));
            testCharList.Add(new JSA_Game.CharClasses.Warrior(null, 3));
            testCharList.Add(new JSA_Game.CharClasses.Warrior(null));
            testCharList.Add(new JSA_Game.CharClasses.Mage(null));
            testCharList.Add(new JSA_Game.CharClasses.Mage(null, 2));
            testCharList.Add(new JSA_Game.CharClasses.Warrior(null, 3));

            ScreenManager.AddScreen(new CharacterListScreen(ScreenManager.GraphicsDevice, testCharList, "right", null, new Vector2(0,0)), e.PlayerIndex);
            */
            ScreenManager.AddScreen(new TransitionScreen("Player Turn", Color.White), e.PlayerIndex);
           
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }





    }
}
