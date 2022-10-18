using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            if (Game.KeyPressed == Keys.Left && x > 0)
                return new CreatureCommand() { DeltaX = -1, DeltaY = 0, TransformTo = null };

            if (Game.KeyPressed == Keys.Right && x < Game.MapWidth - 1)
                return new CreatureCommand() { DeltaX = 1, DeltaY = 0, TransformTo = null };

            if (Game.KeyPressed == Keys.Up && y > 0)
                return new CreatureCommand() { DeltaX = 0, DeltaY = -1, TransformTo = null };

            if (Game.KeyPressed == Keys.Down && y < Game.MapHeight - 1)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1, TransformTo = null };

            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return -1;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = null };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }

            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Sack : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            
            if (y < Game.MapHeight -1 && Game.Map[x,y + 1] == null)
            {
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1, TransformTo = null};
            }

            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
}
