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
        public static int X { get; protected set; }
        public static int Y { get; protected set; }

        public static bool SetPlayerСoordinates()
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
            else if (conflictedObject is Monster)
                return true;

            return false;
        }

        public int GetDrawingPriority() => 2;

        public string GetImageFileName() => "Gold.png";
    }

    public class Sack : ICreature
    {
        private int isGold = 0;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight -1 && 
                (Game.Map[x,y + 1] == null || (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster) && isGold >= 1) )
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

        public int GetDrawingPriority() => -2;

        public string GetImageFileName() => "Sack.png";
    }

    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            if (Player.SetPlayerСoordinates())
            {
                

                // Left
                if (Player.X - x <= -1
                    && x > 0
                    && !(Game.Map[x - 1, y] is Sack)
                    && !(Game.Map[x - 1, y] is Terrain)
                    && !(Game.Map[x - 1, y] is Monster)
                    )
                    return new CreatureCommand() { DeltaX = -1, DeltaY = 0, TransformTo = null };

                // Right
                if (Player.X - x >= 1
                    && x < Game.MapWidth - 1
                    && !(Game.Map[x + 1, y] is Sack)
                    && !(Game.Map[x + 1, y] is Terrain)
                    && !(Game.Map[x + 1, y] is Monster)
                    )
                    return new CreatureCommand() { DeltaX = 1, DeltaY = 0, TransformTo = null };
                
                // top
                if (Player.Y - y <= -1
                    && y > 0
                    && !(Game.Map[x, y - 1] is Sack)
                    && !(Game.Map[x, y - 1] is Terrain)
                    && !(Game.Map[x, y - 1] is Monster)
                    )
                    return new CreatureCommand() { DeltaX = 0, DeltaY = -1, TransformTo = null };
                
                //down
                if (Player.Y - y >= 1
                    && y < Game.MapHeight - 1
                    && !(Game.Map[x, y + 1] is Sack)
                    && !(Game.Map[x, y + 1] is Terrain)
                    && !(Game.Map[x, y + 1] is Monster)
                    )
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1, TransformTo = null };
            }
            
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack || conflictedObject is Monster)
            {
                return true;
            }

            return false;
        }

        public int GetDrawingPriority() => 1;

        public string GetImageFileName() => "Monster.png";
    }
}
