using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaggerLibrary.Cards;
using System.Reflection;

namespace NaggerLibrary
{
    public class CardFactory
    {
        Dictionary<string, Type> cards;

        public CardFactory()
        {
            LoadTypesICanReturn();
        }

        public ICard CreateInstance(string cardName)
        {
            Type t = GetTypeToCreate(cardName);

            if (t == null)
                return new Null();

            return Activator.CreateInstance(t) as ICard;
        }


        Type GetTypeToCreate(string cardName)
        {
            foreach (var card in cards)
            {
                if (card.Key.Contains(cardName))
                {
                    return cards[card.Key];
                }
            }

            return null;
        }


        void LoadTypesICanReturn()
        {
            cards = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(ICard).ToString()) != null)
                {
                    cards.Add(type.Name.ToLower(), type);
                }
            }
        }
    }
}
