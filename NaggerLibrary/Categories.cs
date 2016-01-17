using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary
{
    class MustDoCategorizer: CardCategorizer
    {
        public override ColumnType Categorize(Card card)
        { 
            if (MustBeDoneToday(card))
            {
                return ColumnType.colMust;
            }

            return Next.Categorize(card);
        }

    }

    class ShouldDoCategorizer : CardCategorizer
    {
        public override ColumnType Categorize(Card card)
        {
            if (ShouldDoToday(card))
            {
                return ColumnType.colShould;
            }

            return Next.Categorize(card);
        }

    }

    class CouldDoCategorizer : CardCategorizer
    {
        public override ColumnType Categorize(Card card)
        {
            if (CouldDoToday(card))
            {
                return ColumnType.colCould;
            }

            return Next.Categorize(card);
        }

    }

    class DoneCategorizer : CardCategorizer
    {
        public override ColumnType Categorize(Card card)
        {
            if (DoneToday(card))
            {
                return ColumnType.colDone;
            }

            return Next.Categorize(card);
        }

    }

    class SkippedCategorizer : CardCategorizer
    {
        public override ColumnType Categorize(Card card)
        {
            if (SkippedToday(card))
            {
                return ColumnType.colSkip;
            }

            return Next.Categorize(card);
        }

    }


    class NoneCategorizer : CardCategorizer
    {
        public override ColumnType Categorize(Card card)
        {
            return ColumnType.colNone;   
        }

    }
}
