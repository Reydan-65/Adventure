using CodeBase.Infrastructure.Services.LevelStates;
using CodeBase.Infrastructure.DependencyInjection;

namespace CodeBase.Infrastructure
{
    public class LevelBootStrapper : MonoBootStrapper
    {
        private ILevelStateSwitcher levelStateSwitcher;
        private LevelBootStrappState levelBootStrappState;

        [Inject]
        public void Construct(ILevelStateSwitcher levelStateSwitcher, 
                              LevelBootStrappState levelBootStrappState)
        {
            this.levelStateSwitcher = levelStateSwitcher;
            this.levelBootStrappState = levelBootStrappState;
        }

        public override void OnBindResolved()
        {
            InitLevelStateMachine();
        }

        private void InitLevelStateMachine()
        {
            levelStateSwitcher.AddState(levelBootStrappState);

            levelStateSwitcher.Enter<LevelBootStrappState>();
        }
    }
}
