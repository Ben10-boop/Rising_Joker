namespace RisingJoker.RenderingAdapters
{
    public class MenuRenderer : IRenderer
    {
        Menu gameMenu;

        public MenuRenderer(Menu gameMenu)
        {
            this.gameMenu = gameMenu;
        }

        public void Render()
        {
            gameMenu.InitializeMenu();
            gameMenu.DisplayMessages();
        }
    }
}
