using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser.Model
{
    internal abstract class GameCondition
    {
        //static map of id to method that takes an int data value (20 ids)
        //method to perform a given id with a given value, return bool
        //test method by making sure all actions are handled appropriately, affecting game state and consuming parameters
        delegate bool IsMet(int dataValue);
        private static Dictionary<int, IsMet> processors = new Dictionary<int, IsMet>();

        static GameCondition() {
            //processors.Add(0)
        }

        //how to access this? GameCondition.run(x, gameState)?
        //GameCondition.get(x).run(gameState, y)
        //gameState.checkCondition(x or x, y)
    }
}
