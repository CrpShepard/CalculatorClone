using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorForm
{
    public class StateMonitor
    {
        public string expression;

        public event Action<string> ExpressionChanged;

        public string MonitoredString
        {
            get { return expression; }
            set
            {
                expression = value;
                // При изменении вызываем событие
                OnStringChanged(expression);
            }
        }

        public virtual void OnStringChanged(string str)
        {
            ExpressionChanged?.Invoke(str);
        }


        public bool clearEntry;

        public event Action ClearEntryChanged;

        public bool MonitoredClearEntry
        {
            get { return clearEntry; }
            set
            {
                clearEntry = value;
                // При изменении вызываем событие
                OnClearEntryChanged();
            }
        }

        public virtual void OnClearEntryChanged()
        {
            ClearEntryChanged?.Invoke();
        }
    }
}
