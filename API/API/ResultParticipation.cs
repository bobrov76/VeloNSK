using System;
using System.Collections.Generic;

namespace API
{
    public partial class ResultParticipation
    {
        public int IdResultParticipation { get; set; }
        public int IdParticipation { get; set; }
        public DateTime ResultTime { get; set; }
        public int Mesto { get; set; }
    }
}
