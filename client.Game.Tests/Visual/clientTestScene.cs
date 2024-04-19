using osu.Framework.Testing;

namespace client.Game.Tests.Visual
{
    public abstract partial class clientTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new clientTestSceneTestRunner();

        private partial class clientTestSceneTestRunner : clientGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
