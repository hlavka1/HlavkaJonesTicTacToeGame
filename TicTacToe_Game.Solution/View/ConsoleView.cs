using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace TicTacToe_Game
{
    public class ConsoleView
    {
        #region ENUMS

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitScreen()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for playing our Tic Tac Toe game.");

        }



        public enum ViewState
        {
            Active,
            PlayerTimedOut, // TODO Track player time on task
            PlayerUsedMaxAttempts
        }

        #endregion

        #region FIELDS

        private const int GAMEBOARD_VERTICAL_LOCATION = 4;

        private const int POSITIONPROMPT_VERTICAL_LOCATION = 14;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 3;

        private const int MESSAGEBOX_VERTICAL_LOCATION = 17;

        private const int TOP_LEFT_ROW = 3;
        private const int TOP_LEFT_COLUMN = 6;

        private Gameboard _gameboard;
        private ViewState _currentViewStat;

        #endregion

        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewStat; }
            set { _currentViewStat = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;

            InitializeView();

        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the console view
        /// </summary>
        public void InitializeView()
        {
            _currentViewStat = ViewState.Active;

            InitializeConsole();
        }

        /// <summary>
        /// Prompt the first player to choose their game token
        /// </summary>
        /// <returns>char</returns>
        public char GetPlayerOne()
        {
            char playerChoice;

            Console.Clear();

            Dictionary<char, string> menu = new Dictionary<char, string>
                {
                    { 'O', "Player O" },
                    { 'X', "Player X" },
                    { 'R', "Random player selection" }
                };
            ConsoleUtil.HeaderText = "Player Selection";
            Console.WriteLine("Choose which player will take the first turn. \n\n");
            foreach (KeyValuePair<char, string> menuChoice in menu)
            {
                string formattedMenuChoice = ConsoleUtil.ToLabelFormat(menuChoice.Value.ToString()) + "\n";
                //  Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 3, topRow++);
                Console.Write($"{menuChoice.Key}. {formattedMenuChoice}");
            }
            ConsoleKeyInfo keyPressedInfo = Console.ReadKey();

            playerChoice = Convert.ToChar(keyPressedInfo.KeyChar.ToString().ToUpper());

            return playerChoice;
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyBackgroundColor;

            
            ConsoleUtil.WindowTitle = "The Tic-tac-toe Game";

        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        /// <param name="allowQuit"></param>
        /// <returns>bool</returns>
        public bool DisplayContinuePrompt(bool allowQuit)
        {
            bool userChoice = true;

            //Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            if (allowQuit)
            {
               if (Convert.ToChar(response.KeyChar.ToString().ToUpper()) == 'Q')
                {
                    userChoice = false;
                }
            }

            Console.WriteLine();

            Console.CursorVisible = true;

            return userChoice;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("We hope you had fun playing on our 4x4 Tic Tac Toe. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the session timed out screen
        /// </summary>
        public void DisplayTimedOutScreen()
        {
            ConsoleUtil.HeaderText = "You have run out of time!";
            ConsoleUtil.DisplayReset();

            DisplayMessageBox("It appears your session has timed out.");

            DisplayContinuePrompt(false);
        }

        /// <summary>
        /// display the maximum attempts reached screen
        /// </summary>
        public void DisplayMaxAttemptsReachedScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Maximum Attempts Reached!";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you are having difficulty entering your choice");
            sb.Append(" Really?!  Tic Tac Toe isn't this hard!   Try again.");
            sb.Append("Please refer to the instructions and play.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt(false);
        }



        /// <summary>
        /// Inform the player that their position choice is not available
        /// </summary>
        public void DisplayGamePositionChoiceNotAvailableScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Position Choice Unavailable";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you have chosen a position that is all ready");
            sb.Append(" taken. Please try again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt(false);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        /// <returns>bool</returns>
        public bool DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Written by Justina Hlavka & Shayne Jones");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College");
            ConsoleUtil.DisplayMessage("CIT 255 - Fall 2018");
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("This application is designed to allow two players to play ");
            sb.AppendFormat("a game of tic-tac-toe. The rules are standard, but the board is 4x4. To win, you have to get 4 in a row. ");
            sb.AppendFormat("Each player gets to take a turn.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press Q to quit at any time, if you get bored and don't want to play the game anymore.");
            Console.WriteLine();

             return DisplayContinuePrompt(true);
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Tic-tac-toe Game.");

            DisplayContinuePrompt(false);
        }

        public void DisplayGameArea()
        {
            ConsoleUtil.HeaderText = "Current Game Board";
            ConsoleUtil.DisplayReset();

            DisplayGameboard();
            DisplayGameStatus();
        }

        public void DisplayCurrentGameStatus(int roundsPlayed, int playerXWins, int playerOWins, int catsGames)
        {
            ConsoleUtil.HeaderText = "Current Game Status";
            ConsoleUtil.DisplayReset();

            double playerXPercentageWins = (roundsPlayed > 0) ? (double)playerXWins / roundsPlayed : 0;
            double playerOPercentageWins = (roundsPlayed > 0) ? (double)playerOWins / roundsPlayed : 0;
            double percentageOfCatsGames = (roundsPlayed > 0) ? (double)catsGames / roundsPlayed : 0; 
            

            ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            ConsoleUtil.DisplayMessage("Rounds for Player X: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            ConsoleUtil.DisplayMessage("Rounds for Player O: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            DisplayContinuePrompt(false);

            Console.Clear();
        }

        public bool DisplayNewRoundPrompt()
        {
            ConsoleUtil.HeaderText = "Continue or Quit";
            ConsoleUtil.DisplayReset();

            return DisplayGetYesNoPrompt("Would you like to play another round?");
        }

        public void DisplayGameStatus()
        {
            StringBuilder sb = new StringBuilder();

            switch (_gameboard.CurrentRoundState)
            {
                case Gameboard.GameboardState.NewRound:
                    //
                    // The new game status should not be an necessary option here
                    //
                    break;
                case Gameboard.GameboardState.PlayerXTurn:
                    DisplayMessageBox("It is currently Player X's turn.");
                    break;
                case Gameboard.GameboardState.PlayerOTurn:
                    DisplayMessageBox("It is currently Player O's turn.");
                    break;
                case Gameboard.GameboardState.PlayerXWin:
                    DisplayMessageBox("Player X Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.PlayerOWin:
                    DisplayMessageBox("Player O Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.CatsGame:
                    DisplayMessageBox("Cat's Game! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                default:
                    break;
            }
        }

        public void DisplayMessageBox(string message)
        {
            string leftMargin = new String(' ', ConsoleConfig.displayHorizontalMargin);
            string topBottom = new String('*', ConsoleConfig.windowWidth - 2 * ConsoleConfig.displayHorizontalMargin);

            StringBuilder sb = new StringBuilder();

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);
            Console.WriteLine(leftMargin + topBottom);

            Console.WriteLine(ConsoleUtil.Center("Game Status"));

            ConsoleUtil.DisplayMessage(message);

            Console.WriteLine(Environment.NewLine + leftMargin + topBottom);
        }

        /// <summary>
        /// display the current game board
        /// </summary>
        private void DisplayGameboard()
        {
            //
            // move cursor below header
            //


            Console.SetCursorPosition(0, GAMEBOARD_VERTICAL_LOCATION);


            Console.OutputEncoding = System.Text.Encoding.Unicode;

            char ulCorner = '\u2554';
            char llCorner = '\u255A';
            char urCorner = '\u2557';
            char lrCorner = '\u255D';
            char vertical = '\u2551';
            char horizontal = '\u2550';
            char uTee = '\u2566';
            char dTee = '\u2569';
            char lTee = '\u2560';
            char rTee = '\u2563';
            char cTee = '\u256C';

            Console.Write("\t\t\t        " + ulCorner+horizontal+horizontal+horizontal+uTee+horizontal+horizontal+horizontal+uTee+horizontal+horizontal+horizontal+uTee+horizontal+horizontal+horizontal+urCorner+"\n");

            for (int i = 0; i < 4; i++)
            {
                Console.Write("\t\t\t        " + vertical + " ");

                for (int j = 0; j < 4; j++)
                {
                    if (_gameboard.PositionState[i, j] == Gameboard.PlayerPiece.None)
                    {
                        Console.Write(" " + " " + vertical + " ");
                    }
                    else
                    {
                        Console.Write(_gameboard.PositionState[i, j] + " " + vertical + " ");
                    }

                }

                if (i < 3)
                {
                    Console.Write("\n\t\t\t        " + lTee + horizontal + horizontal + horizontal + cTee + horizontal + horizontal + horizontal + cTee + horizontal + horizontal + horizontal + cTee + horizontal + horizontal + horizontal + rTee + "\n");
                }
                else
                {
                    Console.Write("\n\t\t\t        " + llCorner + horizontal + horizontal + horizontal + dTee + horizontal + horizontal + horizontal + dTee + horizontal + horizontal + horizontal + dTee + horizontal + horizontal + horizontal + lrCorner + "\n");
                }

            }

        }

        /// <summary>
        /// display prompt for a player's next move
        /// </summary>
        /// <param name="coordinateType"></param>
        private void DisplayPositionPrompt(string coordinateType)
        {
            //
            // Clear line by overwriting with spaces
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write(new String(' ', ConsoleConfig.windowWidth));
            //
            // Write new prompt
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write("Enter " + coordinateType + " number: ");
        }

        /// <summary>
        /// Display a Yes or No prompt with a message
        /// </summary>
        /// <param name="promptMessage">prompt message</param>
        /// <returns>bool where true = yes</returns>
        private bool DisplayGetYesNoPrompt(string promptMessage)
        {
            bool yesNoChoice = false;
            bool validResponse = false;
            string userResponse;

            while (!validResponse)
            {
                ConsoleUtil.DisplayReset();

                ConsoleUtil.DisplayPromptMessage(promptMessage + "(yes/no)");
                userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "YES")
                {
                    validResponse = true;
                    yesNoChoice = true;
                }
                else if (userResponse.ToUpper() == "NO")
                {
                    validResponse = true;
                    yesNoChoice = false;
                }
                else
                {
                    ConsoleUtil.DisplayMessage(
                        "It appears that you have entered an incorrect response." +
                        " Please enter either \"yes\" or \"no\"."
                        );
                    DisplayContinuePrompt(false);
                }
            }

            return yesNoChoice;
        }

        /// <summary>
        /// Get a player's position choice within the correct range of the array
        /// Note: The ConsoleView is allowed access to the GameboardPosition struct.
        /// </summary>
        /// <returns>GameboardPosition</returns>
        public GameboardPosition GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1);

            //
            // Get row number from player.
            //
            gameboardPosition.Row = PlayerCoordinateChoice("Row");

            //
            // Get column number.
            //
            if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            {
                gameboardPosition.Column = PlayerCoordinateChoice("Column");
            }

            return gameboardPosition;

        }

        /// <summary>
        /// Validate the player's coordinate response for integer and range
        /// </summary>
        /// <param name="coordinateType">an integer value within proper range or -1</param>
        /// <returns></returns>
        private int PlayerCoordinateChoice(string coordinateType)
        {
            int tempCoordinate = -1;
            int numOfPlayerAttempts = 1;
            int maxNumOfPlayerAttempts = 4;

            while ((numOfPlayerAttempts <= maxNumOfPlayerAttempts))
            {
                DisplayPositionPrompt(coordinateType);

                if (int.TryParse(Console.ReadLine(), out tempCoordinate))
                {
                    //
                    // Player response within range
                    //
                    if (tempCoordinate >= 1 && tempCoordinate <= _gameboard.MaxNumOfRowsColumns)
                    {
                        return tempCoordinate;
                    }
                    //
                    // Player response out of range
                    //
                    else
                    {
                        DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3,4)");
                    }
                }
                //
                // Player response cannot be parsed as integer
                //
                else
                {
                    DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3,4), sorry");
                }

                //
                // Increment the number of player attempts
                //
                numOfPlayerAttempts++;
            }

            //
            // Player used maximum number of attempts, set view state and return
            //
            CurrentViewState = ViewState.PlayerUsedMaxAttempts;
            return tempCoordinate;
        }

        /// <summary>
        /// get a menu choice from the user
        /// </summary>
        /// <returns>MenuOption</returns>
        public MenuOption GetMenuChoice(Menu menu)
        {
            MenuOption chooseOption = MenuOption.None;

            Console.CursorVisible = true;

            //
            // create an array of valid menu keys from menu dictionary
            //
            char[] validKeys = menu.MenuChoices.Keys.ToArray();

            ///
            /// validate key pressed as in MenuOption dictionary
            ///
            char keyPressed;

            do
            {
                DisplayMenu(menu);

                ConsoleKeyInfo keyPressedInfo = Console.ReadKey(true);
                keyPressed = Convert.ToChar(keyPressedInfo.KeyChar.ToString().ToUpper());
                    
                if (!validKeys.Contains(keyPressed))
                {
                    Console.WriteLine("\n\n");
                    Console.WriteLine("   " + keyPressed.ToString() + " is not a valid menu choice.");

                    DisplayContinuePrompt(false);
                }


            } while (!validKeys.Contains(keyPressed));

            chooseOption = menu.MenuChoices[keyPressed];
            Console.CursorVisible = true;


            return chooseOption;
        }

        public void DisplayMenu(Menu menu)
        {
            Console.Clear(); 

            //MenuOption chooseOption = MenuOption.None;

            ConsoleUtil.HeaderText = "The Tic-tac-toe Main Menu";

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);

            //
            // display menu choices
            //
            int topRow = 3;
            foreach (KeyValuePair<char, MenuOption> menuChoice in menu.MenuChoices)
            {
                if (menuChoice.Value != MenuOption.None)
                {
                    string formatedMenuChoice = ConsoleUtil.ToLabelFormat(menuChoice.Value.ToString());
                    Console.SetCursorPosition(MESSAGEBOX_VERTICAL_LOCATION + 3, topRow++);
                    Console.Write($"{menuChoice.Key}. {formatedMenuChoice}");
                }
            }
            Console.WriteLine();
            //chooseOption = GetMenuChoice(menu);

            //return chooseOption;
        }

        /// <summary>
        /// display the game rules
        /// </summary>
        public void DisplayGameRules()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "The Tic-tac-toe Game Rules";
            ConsoleUtil.DisplayReset();
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("Tic-tac-toe is a two-player game.  ");
            sb.AppendFormat("This version of tic-tac-toe uses a 4-row, 4-column game board.  ");
            sb.AppendFormat("Players take turns placing their game pieces on the board.  ");
            sb.AppendFormat("The first player to place 4 pieces in a row, column, or diagonal, wins.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            DisplayContinuePrompt(false);

            Console.Clear();
        }

        /// <summary>
        /// display the save game screen
        /// this is not yet implemented
        /// </summary>
        public void DisplaySaveGameScreen()
        {
            
        }

        #endregion
    }
}
