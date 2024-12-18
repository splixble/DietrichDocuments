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

            SharePriceDataGrid.ItemsSource = _SharePriceTable;
        }

        private void FundCombo_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _SharePriceAdap.FillByFund(_SharePriceTable, FundCombo.SelectedValue as string);
        }
    }
}