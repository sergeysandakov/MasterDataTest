using System;
using System.Threading;
using System.Windows.Automation;

namespace MasterDataTest
{
    public class Program
    {
        private static EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static void Main(string[] args)
        {
                AutomationElement root = AutomationElement.RootElement;

                AutomationElement targetApp = null;
                bool inTime = false;    
                try
                {
                    Thread findAppThread = new Thread(() =>
                    {
                            targetApp = root.FindFirst(TreeScope.Descendants,
                            new PropertyCondition(AutomationElement.NameProperty, "MainWindow"));
                        ewh.Set();
                    });
                    findAppThread.Start();
                    inTime = ewh.WaitOne(1000, true);
                    if (!inTime) throw new NullReferenceException();
                }

                catch (NullReferenceException e)
                {
                    Thread.Sleep(1000);
                    Environment.Exit(1);
                }
                AutomationElement logInButton = AutomationElement.RootElement.FindFirst(TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.NameProperty, "Log In"));

                if (logInButton.GetCurrentPattern(InvokePattern.Pattern) is InvokePattern pushLogBot) pushLogBot.Invoke(); else { }

                AutomationElement connectWindow = AutomationElement.RootElement.FindFirst(TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.NameProperty, "Connect to myserver"));

                if (connectWindow.FindFirst(TreeScope.Descendants,
                    new AndCondition(
                        new PropertyCondition(AutomationElement.ClassNameProperty, "Edit"),
                        new PropertyCondition(AutomationElement.NameProperty, "User name:")))
                            .GetCurrentPattern(ValuePattern.Pattern) is ValuePattern loginFieldVp) loginFieldVp
                            .SetValue("sandakov");

                if (connectWindow.FindFirst(TreeScope.Descendants,
                    new AndCondition(
                        new PropertyCondition(AutomationElement.ClassNameProperty, "Edit"),
                        new PropertyCondition(AutomationElement.NameProperty, "Password:")))
                            .GetCurrentPattern(ValuePattern.Pattern) is ValuePattern passFieldVp) passFieldVp
                            .SetValue("sandakov");

                if (connectWindow.FindFirst(TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.NameProperty, "OK"))
                        .GetCurrentPattern(InvokePattern.Pattern) is InvokePattern confirmButtonVp) confirmButtonVp
                        .Invoke();

                Console.WriteLine("Ok");
        }
    }
}
