using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TypeLib.DateTimeExtensions;

namespace Budget
{
    public class MonthComboBox : ComboBox
    {
        List<MonthItem> _Values = new List<MonthItem>();

        public bool Quarterly = false; // list quarters instead of months

        public MonthComboBox() 
        {
        }

        public void Populate(DateTime minDate, DateTime maxDate)
        {
            DateTime monthToAdd = ConvertToMonthOrQuarter(minDate);
            while (monthToAdd < maxDate)
            {
                _Values.Add(new MonthItem(monthToAdd, Quarterly));
                if (Quarterly)
                    monthToAdd = monthToAdd.AddMonths(3);
                else
                    monthToAdd = monthToAdd.AddMonths(1);
            }

            this.DataSource = _Values;
            this.ValueMember = "Value"; // Why doesnt this point the combo's SelectedValue to the property Value? SelectedValue retuens the MonthItem itself.
            this.DisplayMember = "DisplayValue";
        }

        DateTime ConvertToMonthOrQuarter(DateTime dateTime)
        {
            if (Quarterly)
                return dateTime.FirstOfQuarter();
            else
                return dateTime.FirstOfMonth();
        }

        public DateTime SelectedMonth
        {
            get { return (DateTime)SelectedValue; }
            set { SelectedValue = ConvertToMonthOrQuarter(value); } // converts it to first of month or quarter rather than throwing exception, for convenience
        }

        public class MonthItem
        {
            public DateTime Value => _Value;
            DateTime _Value;

            bool _Quarterly;

            public MonthItem(DateTime value, bool quarterly)
            {
                _Value = value;
                _Quarterly = quarterly;
            }

            public string DisplayValue
            {  
                get 
                {
                    if (_Quarterly)
                        return "Q" + _Value.Quarter().ToString() + _Value.ToString(" yy");
                    else
                        return _Value.ToString("MM/yy");
                }
            }

        }
    }
}
