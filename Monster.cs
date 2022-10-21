namespace Digger
{
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
