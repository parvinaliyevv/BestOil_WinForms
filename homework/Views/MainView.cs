using homework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework.Views
{
    public partial class MainView : Form, IMainView
    {
        public EventHandler<EventArgs> GetFuels { get; set; }

        public List<Fuel> Fuels { set { comboBox1.DataSource = value; } }

        public MainView() => InitializeComponent();

        private void MainView_Load(object sender, EventArgs e) => GetFuels.Invoke(sender, e);

        private void PaymentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CountEntry.Enabled = CountRadioButton.Checked ? true : false;
            CashEntry.Enabled = CashRadioButton.Checked ? true : false;

            if (sender is RadioButton rb)
            {
                if (rb.Name == nameof(CountRadioButton)) FuelCount_TextChanged(CountEntry, null);
                if (rb.Name == nameof(CashRadioButton)) FuelCount_TextChanged(CashEntry, null);
            }
        }

        private void DishRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            HotDogCount.Enabled = HotDogCheckBox.Checked ? true : false;
            HamburgerCount.Enabled = HamburgerCheckBox.Checked ? true : false;
            FrenchFriesCount.Enabled = FrenchFriesCheckBox.Checked ? true : false;
            CocaColaCount.Enabled = CocaColaCheckBox.Checked ? true : false;
        }

        private void DishPrice_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                double calc = 0;

                try
                {
                    if (HotDogCount.Enabled && !string.IsNullOrWhiteSpace(HotDogCount.Text))
                        calc += Convert.ToInt32(HotDogCount.Text) * Convert.ToDouble(HotDogPrice.Text);

                    if (HamburgerCount.Enabled && !string.IsNullOrWhiteSpace(HamburgerCount.Text))
                        calc += Convert.ToInt32(HamburgerCount.Text) * Convert.ToDouble(HamburgerPrice.Text);

                    if (FrenchFriesCount.Enabled && !string.IsNullOrWhiteSpace(FrenchFriesCount.Text))
                        calc += Convert.ToInt32(FrenchFriesCount.Text) * Convert.ToDouble(FrenchFriesPrice.Text);

                    if (CocaColaCount.Enabled && !string.IsNullOrWhiteSpace(CocaColaCount.Text))
                        calc += Convert.ToInt32(CocaColaCount.Text) * Convert.ToDouble(CocaColaPrice.Text);
                }
                catch
                {
                    MessageBox.Show("You can only enter integer values!", "Invalid Input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                DishPrice.Text = calc.ToString();
            }
        }

        private void Payment_TextChanged(object sender, EventArgs e) => TotalGroupBox.Text = (Convert.ToDouble(RefuelPrice.Text) + Convert.ToDouble(DishPrice.Text)).ToString();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FuelPrice.Text = String.Format("{0:0.00}", (comboBox1.SelectedItem as Fuel)?.Price);
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {
            if (TotalGroupBox.Text.Equals("0"))
            {
                MessageBox.Show("You haven't chosen anything!", "Invalid Operation!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Payment was successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FuelCount_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (CountRadioButton.Checked && !string.IsNullOrWhiteSpace(tb.Text) && !tb.Text.EndsWith("."))
                    {
                        CashEntry.TextChanged -= FuelCount_TextChanged;
                        CashEntry.Text = (Convert.ToDouble(tb.Text) * Convert.ToDouble(FuelPrice.Text)).ToString();
                        RefuelPrice.Text = CashEntry.Text;
                        CashEntry.TextChanged += FuelCount_TextChanged;
                    }
                    else if (CashRadioButton.Checked && !string.IsNullOrWhiteSpace(tb.Text) && !tb.Text.EndsWith("."))
                    {
                        CountEntry.TextChanged -= FuelCount_TextChanged;
                        CountEntry.Text = Math.Round((Convert.ToDouble(tb.Text) / Convert.ToDouble(FuelPrice.Text)), 2).ToString();
                        RefuelPrice.Text = tb.Text;
                        CountEntry.TextChanged += FuelCount_TextChanged;
                    }
                }
                catch
                {
                    MessageBox.Show("You can only enter integer values!", "Invalid Input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
