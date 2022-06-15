using LOL_AutoHandler_v2.Models;
using LOL_AutoHandler_v2.Python;
using LOL_AutoHandler_v2.Utilty;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LOL_AutoHandler_v2
{
    public class ServiceLogic
    {
        MainViewModel _mvm;
        HandlePython _handlePython;
        public Thread MainThread;

        public ServiceLogic(MainViewModel m, HandlePython h)
        {
            _mvm = m;
            _handlePython = h;
        }

        public void AbortAndCleanup()
        {
            _mvm.CurrentClientState = ClientState.NONE;

           if(MainThread.IsAlive)MainThread.Abort();
        }

        public void RunLogic()
        {
            if (MainThread != null && MainThread.IsAlive) MainThread.Abort();

            MainThread = new Thread(UpdateLoop);
            MainThread.Start();
        }

        public void UpdateLoop()
        {
            int currenToPickIndex = 0;
            int currenToBanIndex = 0;
            bool isPicking = false;
            bool isBanning = false;
            ClientDependableSizer sizer = new ClientDependableSizer(_mvm.ClientWindowSize);
            int roleIndex = -1; // 0=top 1=jg 2=mid 3=adc 4=sup 

            while (_mvm.IsServiceOn)
            {
                if (_mvm.ClientWindowSize == WindowSize.MINIMIZED || _mvm.ClientWindowSize == WindowSize.NONE || _mvm.ClientWindowSize == WindowSize.OTHER) //Check if lol is 
                {
                    _handlePython.GetClientWindowSetup();
                    sizer = new ClientDependableSizer(_mvm.ClientWindowSize);
                    Thread.Sleep(1000);
                    goto END_LABEL;
                }
                

                if (_mvm.CurrentClientState != ClientState.PICKPHASE) isPicking = false;
                if (_mvm.CurrentClientState != ClientState.BANPHASE) isBanning = false;

                string acceptButtonPath = sizer.AcceptButtonPath;
                string banButtonPath =sizer.BanButtonPath;
                string pickButtonPath =sizer.PickButtonPath;
                string searchBarPath = sizer.SearchBarPath;
                string pickPhaseShower = sizer.PickPhaseShower; 
                string banPhaseShower = sizer.BanPhaseShower; 
                string queuePhaseShower = sizer.QueuePhaseShower;


                //CLIENT LOGIC
                switch (_mvm.CurrentClientState)
                {
                    #region PHASE IDENTIFY
                    case ClientState.NONE: //IDENTIFY STATE (SOMEHOW)
                        if (_handlePython.SilentlyLookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(banPhaseShower),.9f)) 
                        { 
                            roleIndex = _handlePython.IdentifyRole(_mvm.WindowPosition, sizer); 
                            if(roleIndex == -1)
                            {
                                Debug.WriteLine("|| -> CAN NOT IDENTIFY ROLE!");
                                return;
                            }
                            _mvm.CurrentClientState = ClientState.BANPHASE; 
                        } 
                        else if (_handlePython.SilentlyLookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(queuePhaseShower),.8f)) { _mvm.CurrentClientState = ClientState.QUEUE; } //QUEUE
                        else if (_handlePython.LookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(acceptButtonPath),"!")) { _mvm.CurrentClientState = ClientState.LOOKING_FOR_ROLE; } //QUEUE
                        else if (_handlePython.SilentlyLookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(pickPhaseShower), .9f)) //PICK
                        { 
                            roleIndex = _handlePython.IdentifyRole(_mvm.WindowPosition, sizer);
                            if (roleIndex == -1)
                            {
                                Debug.WriteLine("|| -> CAN NOT IDENTIFY ROLE!");
                                return;
                            }
                            _mvm.CurrentClientState = ClientState.PICKPHASE; 
                        }
                        break;
                    #endregion

                    #region QUEUE
                    case ClientState.QUEUE: //LOOK FOR ACCEPT BUTTON
                        
                        _handlePython.LookForImage(_mvm.WindowPosition, sizer, Helpers.GetProjectDirectory(acceptButtonPath), "!");
                        if(_mvm.IsPickService && _mvm.IsBanService && !_handlePython.SilentlyLookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(queuePhaseShower),.9f))
                        {
                            roleIndex = -1;
                            _mvm.CurrentClientState = ClientState.LOOKING_FOR_ROLE;
                        }

                        Thread.Sleep(100);
                        break;
                    #endregion

                    #region IDENTIFY ROLE
                    case ClientState.LOOKING_FOR_ROLE:

                        Thread.Sleep(100);
                        roleIndex = _handlePython.IdentifyRole(_mvm.WindowPosition, sizer);
                        if(roleIndex != -1)
                        {
                            _mvm.CurrentClientState = ClientState.BANPHASE;
                        }
                        else
                        {
                           _mvm.CurrentClientState = ClientState.QUEUE;
                        }
                        Debug.WriteLine(roleIndex);

                        break;
                    #endregion

                    #region BAN
                    case ClientState.BANPHASE: //BAN CHAMPION
                        if (!_mvm.IsBanService) _mvm.CurrentClientState = ClientState.PICKPHASE;
                        if (_handlePython.SilentlyLookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(banPhaseShower), .8f)) //Sprawdza czy mozna juz banowac
                        {
                            if (isBanning == false)
                            {
                                isBanning = true;
                            }
                        }
                        else
                        {
                            if (isBanning)
                                _mvm.CurrentClientState = ClientState.QUEUE;
                        }

                        while (_mvm.ToBanChampions[roleIndex,currenToBanIndex].Name == "None")
                        {
                            currenToBanIndex++;

                            if (currenToBanIndex == _mvm.ToBanChampions.GetLength(1))
                            {
                                //END OF TO BAN CHAMP
                                Debug.WriteLine("|| NO CHAMPION TO BAN ||");
                                _mvm.CurrentClientState = ClientState.PICKPHASE;
                            }
                        }

                        if (isBanning && _handlePython.LookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(searchBarPath), _mvm.ToBanChampions[roleIndex,currenToBanIndex].Name))
                        {
                            _handlePython.ClickOnCords(_mvm.WindowPosition, sizer.PickBanOffset);
                            if (_handlePython.LookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(banButtonPath), "!"))
                            {
                                _mvm.CurrentClientState = ClientState.PICKPHASE;
                            }
                            else
                            {
                                _handlePython.RemoveLetters(_mvm.WindowPosition, sizer.SearchBarOffset, _mvm.ToBanChampions[roleIndex,currenToBanIndex].Name.Length);
                                currenToBanIndex++;
                            }
                        }
                        Thread.Sleep(50);
                        break;
                    #endregion

                    #region PICK
                    case ClientState.PICKPHASE: //PICK CHAMPION
                        if (!_mvm.IsPickService) _mvm.CurrentClientState = ClientState.QUEUE;
                        if (_mvm.IsPickService && _handlePython.SilentlyLookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(pickPhaseShower), .8f)) //Sprawdza czy mozna juz pickowac
                        {
                            if (isPicking == false)
                            {
                                isPicking = true;
                            }
                        }
                        else
                        {
                            if (isPicking)
                                _mvm.CurrentClientState = ClientState.QUEUE;
                        }

                        while (_mvm.ToPickChampions[roleIndex,currenToPickIndex].Name == "None")
                        {
                            currenToPickIndex++;

                            if (currenToPickIndex == _mvm.ToPickChampions.GetLength(1))
                            {
                                //END OF TO PICK CHAMP
                                Debug.WriteLine("|| NO CHAMPION TO PICK ||");
                                goto LOOP_BREAK;
                            }
                        }

                        if (isPicking && _handlePython.LookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(searchBarPath), _mvm.ToPickChampions[roleIndex,currenToPickIndex].Name)) //szuka searchboxa i wpisuja nazwe championa
                        {
                            _handlePython.ClickOnCords(_mvm.WindowPosition, sizer.PickBanOffset);
                            if (_handlePython.LookForImage(_mvm.WindowPosition,sizer, Helpers.GetProjectDirectory(pickButtonPath), "!"))
                            {
                                _mvm.CurrentClientState = ClientState.GAME_OR_DODGE;
                            }
                            else
                            {
                                _handlePython.RemoveLetters(_mvm.WindowPosition, sizer.SearchBarOffset, _mvm.ToPickChampions[roleIndex,currenToPickIndex].Name.Length);
                                currenToPickIndex++;
                            }
                        }
                        Thread.Sleep(50);
                        break;
                    #endregion

                    #region GAME_OR_DODGE
                    case ClientState.GAME_OR_DODGE:
                        
                        if(_handlePython.CheckIfMatchIsRun()) //GAME
                        {
                            goto LOOP_BREAK;
                        }
                        else if(_handlePython.SilentlyLookForImage(_mvm.WindowPosition, sizer, Helpers.GetProjectDirectory(queuePhaseShower), .8f)) //DODGE
                        { 
                            _mvm.CurrentClientState = ClientState.QUEUE;
                            goto END_LABEL;
                        }
                        Thread.Sleep(500);
                        break;
                    #endregion
                }


            END_LABEL:
                Debug.WriteLine("|| -> Service is working... PHASE = |" + _mvm.CurrentClientState +"|");
            }

        LOOP_BREAK:
            _mvm.IsServiceOn = false;
            Debug.WriteLine("SERVICE CLOSED");
        }
    }

}
