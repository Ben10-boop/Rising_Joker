namespace RisingJoker.RenderingAdapters
{
    public class RunningGameRenderer : IRenderer
    {

        RunningGame GameRunner;

        public RunningGameRenderer(RunningGame gameRunner)
        {
            GameRunner = gameRunner;
        }

        public void Render()
        {
            GameRunner.BeforePhysics();
            GameRunner.RenderPhysics();
        }
    }
}
