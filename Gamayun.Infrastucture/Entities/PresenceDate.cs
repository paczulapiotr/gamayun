using System;
using System.Collections.Generic;
using System.Text;

namespace Gamayun.Infrastucture.Entities
{
    public class PresenceDate
    {
        public DateTime Date { get; set; }
        public IEnumerable<Presence> Presences{ get; set; }
    }
}
