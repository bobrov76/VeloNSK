using System;
using System.Collections.Generic;

namespace API
{
    public partial class Participation
    {
        public int IdParticipation { get; set; }
        public int IdUser { get; set; }
        public int IdCompetentions { get; set; }
        public bool IdStatusVerification { get; set; }
    }
}
