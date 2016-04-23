using NaggerLibrary.Mock;

namespace NaggerLibrary.Cards
{
    public class Skip: Card
    {
        public override bool ProcessTransition(string fromColumn, ICard penultimateAction, ICard mostRecentAction)
        {
            LastSkip = SystemTime.Now.Invoke().Date;
            ColumnID = (int)ColumnType.colSkip;
            DueDate = DueDate.AddDays(FrequencyID);
            SkipCount += 1;

            return true;
        }
    }
}

