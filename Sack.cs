namespace Digger
{
    public class Sack : ICreature
    {
        private int flySack = 0;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight -1 
                && (Game.Map[x,y + 1] == null 
                || (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster) 
                && flySack >= 1))
            {
                flySack++;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1, TransformTo = null};
            }

            if (flySack >= 2)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            
            flySack = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;
        public int GetDrawingPriority() => -2;
        public string GetImageFileName() => "Sack.png";
    }
}
