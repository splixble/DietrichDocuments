using Microsoft.VisualBasic;
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
using Microsoft.Data.SqlClient; // NOTE: Must be Microsoft.Data.SqlClient, NOT System.Data.SqlClient
using static BudgetWPF.Constants;
using System.Xml.Linq;


namespace BudgetWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainDataSet.GroupingsInOrderDataTable GroupingsInOrderTbl => _GroupingsInOrderTbl;
        MainDataSet.GroupingsInOrderDataTable _GroupingsInOrderTbl = new MainDataSet.GroupingsInOrderDataTable();

        public char AccountType => 'B'; // DIAG flesh out w/combo

        public string AccountOwner => "D"; // DIAG flesh out w/combo -- comboAccountOwner.SelectedValue as string;

        public MainWindow()
        {
            InitializeComponent();

            // initialize test tree DIAG
            FooViewModel[] fooViewModels = BuildGroupingsTree();
            tvGroupings.ItemsSource = fooViewModels;
            FooViewModel root = fooViewModels[0]; // DIAG s/n assume it's first element

            base.CommandBindings.Add(
                new CommandBinding(
                    ApplicationCommands.Undo,
                    (sender, e) => // Execute
                    {
                        e.Handled = true;
                        root.IsChecked = false;
                        this.tree.Focus();
                    },
                    (sender, e) => // CanExecute
                    {
                        e.Handled = true;
                        e.CanExecute = (root.IsChecked != false);
                    }));

            this.tree.Focus();
        }

        private void _Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
        }

        void RefreshDisplay()
        {
            // DIAG Do Refresh of the data source instead -- BuildGroupingsTree();
        }

       public List<FooViewModel> XDummy()
        {
            return new List<FooViewModel>(new FooViewModel[] { new FooViewModel("Jimy", "Jammeess"), new FooViewModel("Flppoel") });
        }

        FooViewModel[] BuildGroupingsTree()
        {
            // returns root node


            // DIAG rename w/ "_"
            FooViewModel balanceTotalNode = null;
            FooViewModel incomeNode = null;
            FooViewModel expensesNode = null;

            FooViewModel rootNode = new FooViewModel("Things");

            List<FooViewModel> allNodes = new List<FooViewModel>();
            SortedList<string, FooViewModel> parentNodesByName = new SortedList<string, FooViewModel>();

            // Clear and requery the table: 
            _GroupingsInOrderTbl.Clear();
            string groupingsInOrderSelectStr = "SELECT DISTINCT TOP (100) PERCENT Grouping, "
                + "CASE [grouping] WHEN 'Income' THEN 1 WHEN 'Expenses' THEN 2 ELSE 3 END AS OrderNum, "
                + "GroupingType, dbo.BudgetTypeGroupings.ParentGroupingLabel "
                + "FROM ViewBudgetWithMonthly left join BudgetTypeGroupings on ViewBudgetWithMonthly.Grouping = BudgetTypeGroupings.GroupingLabel "
                + "WHERE (Grouping IS NOT NULL) AND AccountOwner = '" + AccountOwner + "'";
            if (AccountType != Constants.AccountType.BothValue)
                groupingsInOrderSelectStr += "AND AccountType = '" + AccountType + "'";
            groupingsInOrderSelectStr += "ORDER BY OrderNum, Grouping";

            using (SqlConnection reportDataConn = new SqlConnection(Constants.ConnectionString))
            {
                // reportDataConn.Open();
                SqlCommand groupingsInOrderCmd = new SqlCommand();
                groupingsInOrderCmd.Connection = reportDataConn;
                // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
                groupingsInOrderCmd.CommandText = groupingsInOrderSelectStr;
                SqlDataAdapter groupingsInOrderAdap = new SqlDataAdapter(groupingsInOrderCmd);

                groupingsInOrderAdap.Fill(_GroupingsInOrderTbl);
            }

            // Populate Groupings tree, and save certain nodes for future reference:
            // Add parent nodes:
            foreach (MainDataSet.GroupingsInOrderRow groupingRow in _GroupingsInOrderTbl)
            {
                if (groupingRow.IsParentGroupingLabelNull() && groupingRow.GroupingType != Constants.GroupingType.BalanceOfAccount)
                {
                    FooViewModel node = new FooViewModel(groupingRow.Grouping, groupingRow.Grouping);
                    allNodes.Add(node);
                    parentNodesByName.Add(node.Name, node);

                    if (groupingRow.GroupingType == Constants.GroupingType.BalanceTotal)
                        balanceTotalNode = node;
                    else if (groupingRow.Grouping == Constants.GroupingName.Income)
                        incomeNode = node;
                    else if (groupingRow.Grouping == Constants.GroupingName.Expense)
                        expensesNode = node;
                }
            }

            // Now, add child nodes:
            foreach (MainDataSet.GroupingsInOrderRow groupingRow in _GroupingsInOrderTbl)
            {
                FooViewModel parentNode = null;
                if (!groupingRow.IsParentGroupingLabelNull() && parentNodesByName.ContainsKey(groupingRow.ParentGroupingLabel))
                    parentNode = parentNodesByName[groupingRow.ParentGroupingLabel];

                /* DIAG REMOVE
                if (!groupingRow.IsParentGroupingLabelNull())
                {
                    // Search top level for parent node:
                    // TODO should be some binding method to do this binarily -- or make my own dictionary
                    foreach (object obItem in tvGroupings.Items)
                    {
                        if (((TreeViewItem)obItem).Tag as string == groupingRow.ParentGroupingLabel)
                        {
                            parentNode = (TreeViewItem)obItem;
                            break;
                        }
                    }

                    orig winforms code:
                    TreeViewItem[] parentNodeArray = tvGroupings.Items.Find(groupingRow.ParentGroupingLabel, false); // search only top level
                    if (parentNodeArray.Length > 0) // should never be 0, or >1
                        parentNode = parentNodeArray[0];
                    
                }
                */
                else if (groupingRow.GroupingType == Constants.GroupingType.BalanceOfAccount)
                    parentNode = balanceTotalNode;
                else
                    parentNode = rootNode;

                if (parentNode != null)
                {
                    FooViewModel childNode = new FooViewModel(groupingRow.Grouping, groupingRow.Grouping);
                    allNodes.Add(childNode);
                    parentNode.Children.Add(childNode);
                }
            }

            /* DIAG implement checkbox setting:
            // Check initial default groupings:
            switch (AccountType)
            {
                case Constants.AccountType.Bank:
                    _ExpensesNode.Checked = true;
                    _IncomeNode.Checked = true;
                    break;
                case Constants.AccountType.Investment:
                    _BalanceTotalNode.Checked = true;
                    break;
            }
            */
            return allNodes.ToArray();
        }
    }
}