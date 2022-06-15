using LOL_AutoHandler_v2.Models;
using LOL_AutoHandler_v2.Utilty;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LOL_AutoHandler_v2.Python
{
    public class HandlePython
    {
        public HandlePython()
        {
            RunPythonScript(Helpers.GetProjectDirectory(@"Python"), Helpers.GetProjectDirectory(@"Python\DataLoader\DataLoader.exe"));
        }

        public bool CheckIfMatchIsRun()
        {
            string output = RunPythonScript(Helpers.GetProjectDirectory(@"Python"), Helpers.GetProjectDirectory(@"Python\LookForGameEXE\LookForGameEXE.exe"));

            if (output.Contains("true"))
            {
                return true;
            }
            else
                return false;
        }

        public int IdentifyRole(Vector2 clientPos, ClientDependableSizer sizer)
        {
            if(SilentlyLookForImage(clientPos,sizer, Helpers.GetProjectDirectory(sizer.TopLaneShower), .9f))
            {
                return 0;
            }
            if (SilentlyLookForImage(clientPos, sizer, Helpers.GetProjectDirectory(sizer.JungleLaneShower), .9f))
            {
                return 1;
            }
            if (SilentlyLookForImage(clientPos, sizer, Helpers.GetProjectDirectory(sizer.MidLaneShower), .9f))
            {
                return 2;
            }
            if (SilentlyLookForImage(clientPos, sizer, Helpers.GetProjectDirectory(sizer.AdcLaneShower), .9f))
            {
                return 3;
            }
            if (SilentlyLookForImage(clientPos, sizer, Helpers.GetProjectDirectory(sizer.SupportLaneShower), .9f))
            {
                return 4;
            }

            return -1;
        }

        public void ClickOnCords(Vector2 clientCords, Vector2 insideClientPos)
        {
            string temp = $"{clientCords.x + insideClientPos.x} {clientCords.y + insideClientPos.y}";
            RunPythonScript(Helpers.GetProjectDirectory(@"Python"), Helpers.GetProjectDirectory(@"Python\ClickOnCords\ClickOnCords.exe"), temp);
        }

        public bool SilentlyLookForImage(Vector2 corner,ClientDependableSizer sizer ,string targetPath, float confidence)
        {
            string temp = $"{corner.x} {corner.y} {sizer.ClientSize.x} {sizer.ClientSize.y} $\"{targetPath}\" {confidence}";
            string output = RunPythonScript(Helpers.GetProjectDirectory(@"Python"), Helpers.GetProjectDirectory(@"Python\SilentImageDetecion\SilentImageDetecion.exe"), temp);

            if (output.Contains("false"))
                return false;
            else
                return true;
        }

        public bool LookForImage(Vector2 corner,ClientDependableSizer sizer, string targetPath, string keyInput)
        {
            string temp = $"{corner.x} {corner.y} {sizer.ClientSize.x} {sizer.ClientSize.y} {keyInput} $\"{targetPath}\"";
            string output = RunPythonScript(Helpers.GetProjectDirectory(@"Python"), Helpers.GetProjectDirectory(@"Python\ImageDetecion\ImageDetecion.exe"), temp);

            if (output.Contains("false"))
                return false;
            else
                return true;
        }

        public void RemoveLetters(Vector2 clientPos, Vector2 insideClientPos, int lettersCount)
        {;
            string temp = $"{clientPos.x + insideClientPos.x} {clientPos.y + insideClientPos.y} {lettersCount}";
            string output = RunPythonScript(Helpers.GetProjectDirectory(@"Python"), Helpers.GetProjectDirectory(@"Python\RemoveLetters\RemoveLetters.exe"), temp);
        }

        public void GetClientWindowSetup()
        {
            string str = RunPythonScript(Helpers.GetProjectDirectory(@"Python"), Helpers.GetProjectDirectory(@"Python\DetectClient\DetectClient.exe"));

            string x, y, w, h;

            x = Helpers.GetStringWithin(str,"x=","endX");
            y = Helpers.GetStringWithin(str,"y=","endY");
            w = Helpers.GetStringWithin(str,"width=","endW");
            h = Helpers.GetStringWithin(str,"height=","endH");

            MainViewModel mvm = MainViewModel.instance;


            if (x == "" || y=="" || w =="" || h=="")
            {
                if (str.Contains("None")) mvm.ClientWindowSize = WindowSize.NONE;
                else if (str.Contains("WindowMinimized")) mvm.ClientWindowSize = WindowSize.MINIMIZED;
                else mvm.ClientWindowSize = WindowSize.OTHER;

                mvm.WindowPosition = new Vector2(-1, -1);
                return;
            }

            try
            {
                int xInt = Int32.Parse(x);
                int yInt = Int32.Parse(y);
                int wInt = Int32.Parse(w);
                int hInt = Int32.Parse(h);

                Vector2 winSize = new Vector2(wInt, hInt);

                switch(winSize.x)
                {
                    case Helpers.SmallWindowSizeX:
                        mvm.ClientWindowSize = WindowSize.SMALL;
                        break;
                    case Helpers.MediumWindowSizeX:
                        mvm.ClientWindowSize = WindowSize.MEDIUM;
                        break;
                    case Helpers.LargeWindowSizeX:
                        mvm.ClientWindowSize = WindowSize.LARGE;
                        break;
                    default:
                        mvm.ClientWindowSize = WindowSize.OTHER;
                        break;
                }

                //Debug.WriteLine(mvm.ClientWindowSize);

                mvm.WindowPosition = new Vector2(xInt,yInt);
            }
            catch(FormatException e)
            {
                throw new Exception(e.ToString());
            }
        }


        string RunPythonScript(string projectFolderPath, string exePath)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WorkingDirectory = projectFolderPath;
            processInfo.FileName = exePath;
            processInfo.ErrorDialog = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.CreateNoWindow = true;
             
            var error = "";
            var result = "";

            using (var process = Process.Start(processInfo))
            {
                error = process.StandardError.ReadToEnd();
                result = process.StandardOutput.ReadToEnd();
            }

            //Debug.WriteLine("Output: ");
            //Debug.Write(result);
            //Debug.WriteLine("Errors: ");
            //Debug.Write(error);

            return result;
        }

        string RunPythonScript(string projectFolderPath, string exePath, string input)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WorkingDirectory = projectFolderPath;
            processInfo.FileName = exePath;
            processInfo.ErrorDialog = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.CreateNoWindow = true;

            processInfo.Arguments = input;

            var error = "";
            var result = "";

            using (var process = Process.Start(processInfo))
            {
                error = process.StandardError.ReadToEnd();
                result = process.StandardOutput.ReadToEnd();
            }
            return result;
        }
    }
}
