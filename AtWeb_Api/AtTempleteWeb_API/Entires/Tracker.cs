using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class Tracker
    {
        public Tracker()
        {
            Issue = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Issue> Issue { get; set; }
    }
}
