using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamNG.IDE.Core
{
    class recentPosts
    {

    }

    public class recentPost 
    {
        public string threadName { get; set; }
        public DateTime Date { get; set; }
        public string status { get; set; }
        public string author { get; set; }
        public recentPost(string thrName, DateTime date, string state, string threadAuthor)
        {
            threadName = thrName;
            Date = date;
            status = state;
            author = threadAuthor;
        }
    }
}
