using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject) => true;

        public int GetDrawingPriority() => 1;

        public string GetImageFileName() => "Terrain.png";
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
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
            if (conflictedObject is Sack)
            {
                Game.IsOver = true;
                
                return true;
            }
            
            return false;
        }

        public int GetDrawingPriority() => -1;

        public string GetImageFileName() => "Digger.png";
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }

            return false;
        }

        public int GetDrawingPriority() => 1;

        public string GetImageFileName() => "Gold.png";
    }

    public class Sack : ICreature
    {
        private int isGold = 0;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight -1 && 
                (Game.Map[x,y + 1] == null || (Game.Map[x, y + 1] is Player && isGold >= 1) ) )
            {
                isGold++;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1, TransformTo = null};
            }

            if (isGold >= 2)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            
            isGold = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;

        public int GetDrawingPriority() => 1;

        public string GetImageFileName() => "Sack.png";
    }
}
