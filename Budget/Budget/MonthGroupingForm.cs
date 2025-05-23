﻿using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    public partial class MonthGroupingForm : Form
    {
        public MonthGroupingForm()
        {
            InitializeComponent();
        }

        public void Initialize(DateTime cellMonth, string cellGrouping, string accountOwner, AssetType assetType)
        {
            viewBudgetWithMonthlyTableAdapter.Connection = Program.DbConnection;
            viewBudgetWithMonthlyTableAdapter.FillByMonthGroupingAccountOwnerIsInv(mainDataSet.ViewBudgetWithMonthly, 
                cellMonth, cellGrouping, accountOwner, (byte)(assetType == AssetType.Investments ? 1 : 0)); 
        }

        private void MonthGroupingForm_Load(object sender, EventArgs e)
        {
 
        }
    }
}
