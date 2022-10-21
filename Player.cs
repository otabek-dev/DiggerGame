using System.Windows.Forms;

namespace Digger
{
    public class Player : ICreature
    {
        public static int X { get; protected set; }
        public static int Y { get; protected set; }

        public static bool IsPlayerAlive()
        {
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                {
                    var creature = Game.Map[x, y];
                    if (creature is Player)
                    {
                        X = x;
                        Y = y;
                        return true;
                    }
                }

            return false;
        }

        public CreatureCommand Act(int x, int y)
        {
            X = x;
            Y = y;

            if (Game.KeyPressed == Keys.Left 
                && x > 0 
                && !(Game.Map[x-1,y] is Sack))
                return new CreatureCommand() { DeltaX = -1, DeltaY = 0, TransformTo = null };

            if (Game.KeyPressed == Keys.Right 
                && x < Game.MapWidth - 1
                && !(Game.Map[x + 1, y] is Sack))
                return new CreatureCommand() { DeltaX = 1, DeltaY = 0, TransformTo = null };

            if (Game.KeyPressed == Keys.Up 
                && y > 0
                && !(Game.Map[x, y - 1] is Sack))
                return new CreatureCommand() { DeltaX = 0, DeltaY = -1, TransformTo = null };

            if (Game.KeyPressed == Keys.Down 
                && y < Game.MapHeight - 1
                && !(Game.Map[x, y + 1] is Sack))
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1, TransformTo = null };

            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack || conflictedObject is Monster)
            {
                Game.IsOver = true;
                return true;
            }
            
            return false;
        }

        public int GetDrawingPriority() => -1;

        public string GetImageFileName() => "Digger.png";
    }
}
