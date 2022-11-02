using System.Drawing;

namespace RisingJoker.PlatformsBuilder
{
    public class PlatformColorTheme
    {
        public Color MainColor;
        public Color EnemyColor;
        public Color CoinColor;

        public PlatformColorTheme(string mainColor, string enemyColor, string coinColor)
        {
            MainColor = ColorTranslator.FromHtml(mainColor);
            EnemyColor = ColorTranslator.FromHtml(enemyColor);
            CoinColor = ColorTranslator.FromHtml(coinColor);
        }
    }
}
