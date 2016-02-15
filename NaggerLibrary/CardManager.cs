using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;


namespace NaggerLibrary
{
    public class CardManager
    {
        public ICard ProcessCard(string Nag, string fromColumn, string toColumn)
        {
            CardFactory factory = new CardFactory();
            ICard card = factory.CreateInstance(toColumn.ToLower());

            card = (ICard)JsonConvert.DeserializeObject(Nag, card.GetType());

            card.Description = HttpUtility.HtmlDecode(card.Description);

            card.ProcessTransition();

            return card;
        }
    }
}
