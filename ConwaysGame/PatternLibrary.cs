using ConwaysGame.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGame
{
    public static class PatternLibrary
    {
        // TODO: I would add the ability to apply an offset for adding multiple
        // patterns to a board.  I may be finished with it before taking it
        // that far.
        public static List<GridLocation> Rpentomino = new List<GridLocation>()
        {
            new GridLocation(2,2),
            new GridLocation(2,3),
            new GridLocation(3,1),
            new GridLocation(3,2),
            new GridLocation(4,2)
        };


        public static List<GridLocation> Blinker = new List<GridLocation>()
        {
            new GridLocation(2,4),
            new GridLocation(2,4),
            new GridLocation(4,4),
        };


        public static List<GridLocation> Toad = new List<GridLocation>()
        {
            new GridLocation(2,3),
            new GridLocation(2,4),
            new GridLocation(2,5),
            new GridLocation(3,2),
            new GridLocation(3,3),
            new GridLocation(3,4),
        };
    }
}
