using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Game
{
    /// <summary>
    /// class hold information about each menu
    /// </summary>
    public class Menu
    {
        public string MenuName { get; set; }
        public string MenuTitle { get; set; }
        public Dictionary<char, MenuOption> MenuChoices { get; set; }


        /// <summary>
        /// the main menu
        /// </summary>
        public Menu()
        {
            MenuName = "MainMenu";
            MenuTitle = "MainMenu";
            MenuChoices = new Dictionary<char, MenuOption>()
                {
                    { 'N', MenuOption.PlayNewRound },
                    { 'R', MenuOption.ViewGameRules },
                    { 'C', MenuOption.ViewCurrentGameResults },
                   // { 'P', MenuOption.ViewPastGameResultsScores },
                   // { 'S', MenuOption.SaveGameResults },
                    { 'Q', MenuOption.Quit }
                };
        }
    }

}