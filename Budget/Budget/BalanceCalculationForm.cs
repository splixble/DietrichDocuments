﻿using System;
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
    public partial class BalanceCalculationForm : Form
    {
        public BalanceCalculationForm()
        {
            InitializeComponent();

            comboAccount.DataSource = Program.LookupTableSet.MainDataSet.BudgetAccount;
            comboAccount.DisplayMember = "AccountName";
            comboAccount.ValueMember = "AccountID";
        }

        private void comboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            budgetCtrl.BudgetAdapter.FillByAccount(budgetCtrl.BudgetTable, (string)comboAccount.SelectedValue);
        }

        private void btnCalcBalances_Click(object sender, EventArgs e)
        {
            bool foundBenchmarkBalance = false;
            decimal currentBalance = 0;
            for (int rowNum = 0; rowNum < budgetCtrl.BudgetTable.Count; rowNum++)
            {
                MainDataSet.BudgetRow row = budgetCtrl.BudgetTable[rowNum];
                if (foundBenchmarkBalance)
                {
                    currentBalance += row.Amount;
                    if (!row.IsBalanceNull() && !row.BalanceIsCalculated)
                    {
                        // found a "real" balance; check if it matches our calc'd balance:
                        if (currentBalance != row.Balance)
                        {
                            MessageBox.Show("Discrepancy between calculated balance ($" + currentBalance.ToString("0.00") + 
                                ") and entered balance($" + row.Balance.ToString("0.00") + "), on ID=" + row.ID.ToString());
                            return;
                        }
                    }
                    else
                    {
                        // set calculated balance:
                        row.Balance = currentBalance;
                        row.BalanceIsCalculated = true;
                    }
                }
                else
                {
                    if (!row.IsBalanceNull() && !row.BalanceIsCalculated)
                    {
                        // found our first "real" (benchmark) balance; so go backward through the row and set balance retroactively:
                        foundBenchmarkBalance = true;
                        currentBalance = row.Balance;
                        decimal previousBalance = currentBalance - row.Amount;
                        for (int prevRowNum = rowNum-1; prevRowNum >= 0; prevRowNum--)
                        {
                            MainDataSet.BudgetRow prevRow = budgetCtrl.BudgetTable[prevRowNum];
                            prevRow.Balance = previousBalance;
                            previousBalance -= prevRow.Amount;
                            prevRow.BalanceIsCalculated = true;
                        }
                    }
                }
            }
            budgetCtrl.Grid.Refresh();
            MessageBox.Show("Balance calculation complete.");
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            budgetCtrl.BudgetAdapter.Update(budgetCtrl.BudgetTable);
            budgetCtrl.Grid.Refresh();
        }
    }
}