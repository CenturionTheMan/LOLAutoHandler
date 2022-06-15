using LOL_AutoHandler_v2.Utilty;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOL_AutoHandler_v2.Models
{
    public class ClientDependableSizer
    {
        public Vector2 PickBanOffset;
        public Vector2 SearchBarOffset;

        public Vector2 ClientSize;

        public string AcceptButtonPath = "";
        public string BanButtonPath = "";

        public string PressedAceptButton = "";

        public string PickButtonPath = "";
        public string SearchBarPath = "";

        public string PickPhaseShower = "";
        public string BanPhaseShower = "";
        public string QueuePhaseShower = "";

        public string TopLaneShower = "";
        public string JungleLaneShower = "";
        public string MidLaneShower = "";
        public string AdcLaneShower = "";
        public string SupportLaneShower = "";

        public ClientDependableSizer(WindowSize size)
        {
            switch (size)
            {
                case WindowSize.MEDIUM:
                    PressedAceptButton = @"Python\AcceptButtonImages\AcceptButtonPressed.png";
                    AcceptButtonPath = @"Python\AcceptButtonImages\AcceptButton.png";
                    PickPhaseShower = @"Python\PhaseShowerImages\PickPhaseShower.png";
                    BanPhaseShower = @"Python\PhaseShowerImages\BanPhaseShower.png";
                    QueuePhaseShower = @"Python\PhaseShowerImages\QueuePhaseShower.png";
                    SearchBarPath = @"Python\SearchBar\SearchBar.png";
                    PickButtonPath = @"Python\PickButton\PickButton.png";
                    BanButtonPath = @"Python\BanButton\BanButton.png";
                    PickBanOffset = new Vector2(390, 170);
                    SearchBarOffset = new Vector2(817, 100);
                    ClientSize = new Vector2(1280,720);
                    TopLaneShower = @"Python\RoleImages\Top.png";
                    JungleLaneShower = @"Python\RoleImages\Jg.png";
                    MidLaneShower = @"Python\RoleImages\Mid.png";
                    AdcLaneShower = @"Python\RoleImages\Adc.png";
                    SupportLaneShower = @"Python\RoleImages\Sup.png";
                    break;
                case WindowSize.LARGE:
                    PressedAceptButton = @"Python\AcceptButtonImages\AcceptButtonPressedLarge.png";
                    AcceptButtonPath = @"Python\AcceptButtonImages\AcceptButtonLarge.png";
                    PickPhaseShower = @"Python\PhaseShowerImages\PickPhaseShowerLarge.png";
                    BanPhaseShower = @"Python\PhaseShowerImages\BanPhaseShowerLarge.png";
                    QueuePhaseShower = @"Python\PhaseShowerImages\QueuePhaseShower.png";
                    SearchBarPath = @"Python\SearchBar\SearchBarLarge.png";
                    PickButtonPath = @"Python\PickButton\PickButtonLarge.png";
                    BanButtonPath = @"Python\BanButton\BanButtonLarge.png";
                    PickBanOffset = new Vector2(0, 0);
                    SearchBarOffset = new Vector2(0, 0);
                    ClientSize = new Vector2(1600,900);
                    TopLaneShower = @"Python\RoleImages\TopLarge.png";
                    JungleLaneShower = @"Python\RoleImages\JgLarge.png";
                    MidLaneShower = @"Python\RoleImages\MidLarge.png";
                    AdcLaneShower = @"Python\RoleImages\AdcLarge.png";
                    SupportLaneShower = @"Python\RoleImages\SupLarge.png";
                    break;
                case WindowSize.SMALL:
                    PressedAceptButton = @"Python\AcceptButtonImages\AcceptButtonPressedSmall.png";
                    AcceptButtonPath = @"Python\AcceptButtonImages\AcceptButtonSmall.png";
                    PickPhaseShower = @"Python\PhaseShowerImages\PickPhaseShowerSmall.png";
                    BanPhaseShower = @"Python\PhaseShowerImages\BanPhaseShowerSmall.png";
                    QueuePhaseShower = @"Python\PhaseShowerImages\QueuePhaseShower.png";
                    SearchBarPath = @"Python\SearchBar\SearchBarSmall.png";
                    PickButtonPath = @"Python\PickButton\PickButtonSmall.png";
                    BanButtonPath = @"Python\BanButton\BanButtonSmall.png";
                    PickBanOffset = new Vector2(0, 0);
                    SearchBarOffset = new Vector2(0, 0);
                    ClientSize = new Vector2(1024,576);
                    TopLaneShower = @"Python\RoleImages\TopSmall.png";
                    JungleLaneShower = @"Python\RoleImages\JgSmall.png";
                    MidLaneShower = @"Python\RoleImages\MidSmall.png";
                    AdcLaneShower = @"Python\RoleImages\AdcSmall.png";
                    SupportLaneShower = @"Python\RoleImages\SupSmall.png";
                    break;
                default:
                    Debug.WriteLine("|| -> CLIENT SIZE ERROR <- ||");
                    return;
            }
        }
    }
}
