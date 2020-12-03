using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboBlast.UI.Interfaces
{
    public interface IUILobbyPlayer
    {
        string Name { get; set; }
        bool Ready { get; set; }
    }
}
