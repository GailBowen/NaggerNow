namespace NaggerLibrary.Cards
{
    public class Must : Card
    {
        public override bool ProcessTransition(string fromColumn, ICard penultimateAction)
        {

            if (penultimateAction.ColumnID != (int)ColumnType.colMust)
            {
                return false;
            }
            ColumnID = (int)ColumnType.colMust;
            
            Undo(fromColumn, penultimateAction);

            return true;
        }
    }
}
