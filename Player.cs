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
                    if (Game.Map[x, y] is Player)
                    {
                        X = x;
                        Y = y;
                        return true;
                    }
                
            return false;
        }
        public CreatureCommand MoveTo(int x, int y, int moveX, int moveY)
        {
            if (x + moveX >= 0 && x + moveX < Game.MapWidth
                && y + moveY >= 0 && y + moveY < Game.MapHeight
                && !(Game.Map[x + moveX, y + moveY] is Sack))
                return new CreatureCommand() { DeltaX = moveX, DeltaY = moveY, TransformTo = null };

            return new CreatureCommand();
        }

        public CreatureCommand Act(int x, int y)
        {
            if (Game.KeyPressed == Keys.Left)
                return MoveTo(x, y, -1, 0);

            if (Game.KeyPressed == Keys.Right)
                return MoveTo(x, y, 1, 0);

            if (Game.KeyPressed == Keys.Up)
                return MoveTo(x, y, 0, -1);

            if (Game.KeyPressed == Keys.Down)
                return MoveTo(x, y, 0, 1);

            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject) => 
            conflictedObject is Sack || conflictedObject is Monster;
       
        public int GetDrawingPriority() => -1;

        public string GetImageFileName() => "Digger.png";
    }
}
