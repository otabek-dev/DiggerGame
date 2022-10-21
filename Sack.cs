namespace Digger
{
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
}
