using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaggerLibrary
{
    public class CardCategorizerChain
    {
        private CardCategorizerChain()
        {
            Head = new DoneCategorizer();
            Head.RegisterNext(new MustDoCategorizer())
                .RegisterNext(new SkippedCategorizer())
                .RegisterNext(new ShouldDoCategorizer())
                .RegisterNext(new CouldDoCategorizer())
                .RegisterNext(new NoneCategorizer());
        }

        public static ColumnType GetColumn(Card card)
        {
            return _instance.Head.Categorize(card);
        }

        private CardCategorizer Head { get; set; }

        private static readonly CardCategorizerChain _instance = new CardCategorizerChain();
    }
}
