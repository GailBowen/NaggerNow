using NaggerLibrary.Mock;

namespace NaggerLibrary.Cards
{
    public class Done: Card
    {

        public override bool ProcessTransition(string fromColumn, ICard penultimateAction, ICard mostRecentAction)
        {
            if (mostRecentAction.ColumnID == (int)ColumnType.colSkip)
            {
                return false;
            }
            
            ColumnID = (int)ColumnType.colDone;
            LastDone = SystemTime.Now.Invoke().Date;
            DueDate = LastDone.AddDays(FrequencyID);

            return true;
        }
    }
}
