using System.Collections.Generic;
using System.Windows.Forms;

namespace RisingJoker
{
    public class Menu
    {
        Stack<string> MenuMessages = new Stack<string>();
        Label ConsoleBoard;
        Control.ControlCollection Controls;
        bool hasNewMessages = false;
        bool hasInitialized = false;

        public Menu(Label consoleBoard, Control.ControlCollection controls)
        {
            ConsoleBoard = consoleBoard;
            Controls = controls;
        }


        public void InitializeMenu()
        {
            if (hasInitialized)
            {
                return;
            }


            foreach (Control x in Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "pBottom" || (string)x.Tag == "platform")
                    {
                        x.Visible = false;
                    }
                }
            }
            hasInitialized = true;
        }

        public void DisplayMessages()
        {
            if (!hasNewMessages)
            {
                return;
            }

            string menuBoardText = "";
            int i = 0;

            foreach (string message in MenuMessages)
            {
                if (i > 8) break;
                menuBoardText = menuBoardText + "\n" + message;
                i++;
            }
            ConsoleBoard.Text = menuBoardText;
            hasNewMessages = false;
        }

        public void AddNewMessage(string message)
        {
            MenuMessages.Push(message);
            hasNewMessages = true;
        }
    }
}
