using System.Runtime.CompilerServices;

namespace Digger
{
    public class Monster : ICreature
    {
        public CreatureCommand MoveTo(int x, int y, int moveX, int moveY)
        {
            if (x + moveX >= 0 && x + moveX <= Game.MapWidth 
                && y + moveY >= 0 && y + moveY <= Game.MapHeight
                && !(Game.Map[x + moveX, y + moveY] is Sack)
                && !(Game.Map[x + moveX, y + moveY] is Terrain)
                && !(Game.Map[x + moveX, y + moveY] is Monster))
                return new CreatureCommand() { DeltaX = moveX, DeltaY = moveY, TransformTo = null };

            return new CreatureCommand();
        }

        public CreatureCommand Act(int x, int y)
        {
            if (Player.IsPlayerAlive())
            {
                // Left
                if (Player.X - x <= -1)                    
                    return MoveTo(x, y, -1, 0);
                
                // Right
                if (Player.X - x >= 1)
                    return MoveTo(x, y, 1, 0);

                // top
                if (Player.Y - y <= -1)
                    return MoveTo(x, y, 0, -1);

                //down
                if (Player.Y - y >= 1)
                    return MoveTo(x, y, 0, 1);
            }
            
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Sack || conflictedObject is Monster;
        public int GetDrawingPriority() => 1;
        public string GetImageFileName() => "Monster.png";
    }
}
