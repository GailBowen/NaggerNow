using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NaggerLibrary
{
    public interface INaggerDataLinker
    {
        Card Fetch(IDataReader rdr);
        
        IDataReader FetchAll();

        List<Card> GetCardCollection();

        void Update(Card card);
    }
}
