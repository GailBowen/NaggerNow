namespace NaggerLibrary.Cards
{
    public class Could: Card
    {
       
        public override bool ProcessTransition(string fromColumn, ICard penultimateAction)
        {
            if (penultimateAction.ColumnID != (int)ColumnType.colCould)
            {
                return false;
            }

            ColumnID = (int)ColumnType.colCould;

            Undo(fromColumn, penultimateAction);

            return true;
        }
    }
}
