using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD30
{
    public class StateManager : LDComponent
    {
        private List<GameState> states;
        private List<GameState> statesToRemove;
        private List<GameState> statesToAdd;

        public StateManager(Game1 game)
            : base(game)
        {
            states = new List<GameState>();
            statesToAdd = new List<GameState>();
            statesToRemove = new List<GameState>();
        }

        public override void update()
        {
            base.update();

            foreach (GameState state in states)
            {
                if (state.enabled)
                    state.update();
            }

            foreach (GameState state in statesToAdd)
                states.Add(state);

            foreach (GameState state in statesToRemove)
                states.Remove(state);

            statesToAdd.Clear();
            statesToRemove.Clear();
        }

        public override void draw()
        {
            base.draw();

            foreach (GameState state in states)
            {
               if (state.visible)
                    state.draw();
            }
        }

        public void removeState(GameState state)
        {
            statesToRemove.Add(state);
        }

        public void addState(GameState newState)
        {
            statesToAdd.Add(newState);
        }
        
    }
}
