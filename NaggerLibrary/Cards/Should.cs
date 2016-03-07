namespace NaggerLibrary.Cards
{
    public class Should : Card
    { 
        public override bool ProcessTransition(string fromColumn, ICard penultimateAction)
        {
            if (penultimateAction.ColumnID != (int)ColumnType.colShould)
            {
                return false;
            }
            
            ColumnID = (int)ColumnType.colShould;

            Undo(fromColumn, penultimateAction);

            return true;
        }
    }
}
