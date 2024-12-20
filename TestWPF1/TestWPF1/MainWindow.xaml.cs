using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPF1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet1.FundDataTable _FundTable = new DataSet1.FundDataTable();
        DataSet1TableAdapters.FundTableAdapter _FundAdap = new DataSet1TableAdapters.FundTableAdapter();

        DataSet1.SharePriceDataTable _SharePriceTable = new DataSet1.SharePriceDataTable();
        DataSet1TableAdapters.SharePriceTableAdapter _SharePriceAdap = new DataSet1TableAdapters.SharePriceTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void _Loaded(object sender, RoutedEventArgs e)
        {
            GenText.Text = "Speep bop";

            _FundAdap.Fill(_FundTable);

            FundCombo.ItemsSource = _FundTable;
            FundCombo.DisplayMemberPath = "FundName";
            //FundCombo.SelectedItem = "{Binding FundID}";
            FundCombo.SelectedValuePath = "FundID";

            // Original simple binding:
            SharePriceDataGrid.ItemsSource = _SharePriceTable;

            /* this works but cant filter
            CollectionView cv = new CollectionView(_SharePriceTable);
            SharePriceDataGrid.ItemsSource = cv;
            cv.Filter = new Predicate<object>(TempFilter);
            */

            /* NOTE: Can filter by just creating my own DataView manually: 
                DataTable orders = _dataSet.Tables["SalesOrderDetail"];
                EnumerableRowCollection<DataRow> query = from order in orders.AsEnumerable()
                                                         where order.Field<short>("OrderQty") > 2 && order.Field<short>("OrderQty") < 6
                                                         select order;
                DataView view = query.AsDataView();
             */

            DataGridTextColumn column = new DataGridTextColumn();
            column.Width = 300;
            column.Header = "Price";
            column.Binding = new Binding("PricePerShare");
            SharePriceDataGrid.Columns.Add(column);
        }

        private void _itemSourceList_Filter(object sender, FilterEventArgs e)
        {
            throw new NotImplementedException();
        }

        private bool TempFilter(object ob)
        {
            return true;
        }

        private void FundCombo_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _SharePriceAdap.FillByFund(_SharePriceTable, FundCombo.SelectedValue as string);
        }
    }
}