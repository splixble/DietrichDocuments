using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    public class MonthComboBox : ComboBox
    {
        List<MonthItem> _Values = new List<MonthItem>();

        public MonthComboBox() 
        {
        }

        public void Populate(DateTime minDate, DateTime maxDate, DateTime initialValue)
        {
            DateTime monthToAdd = new DateTime(minDate.Year, minDate.Month, 1);
            while (monthToAdd < maxDate) 
            {
                _Values.Add(new MonthItem(monthToAdd));
                monthToAdd = monthToAdd.AddMonths(1);
            }
            
            this.DataSource = _Values;
            this.ValueMember = "Value"; // Why doesnt this point the combo's SelectedValue to the property Value? SelectedValue retuens the MonthItem itself.
            this.DisplayMember = "DisplayValue";
             
            SelectedValue = new DateTime(initialValue.Year, initialValue.Month, 1); ;
        }

        public DateTime SelectedMonth => (DateTime)SelectedValue;

        public class MonthItem
        {
            public DateTime Value => _Value;
            DateTime _Value;

            public MonthItem(DateTime value)
            {
                _Value = value;
            }

            public string DisplayValue => _Value.ToString("MM/yy");
        }
    }
}
