using Rest_deel2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_deel2.Data
{
    public class NewsMessagesRepository
    {
        public Dictionary<int, NewsMessage> messages { get; }

        public NewsMessagesRepository()
        {
            this.messages = new Dictionary<int, NewsMessage>();
        }


        public int getUnusedId()
        {
            var keys = this.messages.Keys.ToArray();
            int id = 0;

            while (keys.Contains(id))
            {
                id++;
            }
            return id;
        }
    }
}
