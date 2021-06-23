using System;
using System.Threading;
using System.Windows.Automation;

namespace MasterDataTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AutomationElement ae = AutomationElement.RootElement.FindFirst
                (TreeScope.Descendants, new PropertyCondition
                (AutomationElement.NameProperty, "MainWindow"));

            AutomationElement main = ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Log In"));
            InvokePattern ivkpt = main.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            ivkpt.Invoke();

            AutomationElement desk = AutomationElement.RootElement.FindFirst(TreeScope.Children, new PropertyCondition
                (AutomationElement.NameProperty, "Подключение к myserver"));

            if (desk != null)
            {
                AutomationElement log = desk.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "1002"));
                AutomationElement loginfield = log.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, "Edit"));
                AutomationElement pass = desk.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "1005"));

                object login = null;
                object password = null;

                Thread.Sleep(1000);

                if (!loginfield.TryGetCurrentPattern(ValuePattern.Pattern, out login)) {}
                else
                {
                    ((ValuePattern)login).SetValue("user12");
                }

                Thread.Sleep(1000);

                if (!pass.TryGetCurrentPattern(ValuePattern.Pattern, out password)){ }
                else
                {
                    ((ValuePattern)password).SetValue("123451111");
                }
                AutomationElement buttonOk = desk.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "1"));

                Thread.Sleep(1000);

                InvokePattern ivkp = buttonOk.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                ivkp.Invoke();
            }
        }
    }
}
