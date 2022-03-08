using System;
using System.Collections.Generic;
using System.Windows;
using HaoRestaurant.EditedScripts;
namespace HaoRestaurant
{
    public partial class MainWindow
    {
        private bool IsShippedToCook = false;
        public MainWindow()
        {
            InitializeComponent();
            WaterTypeName.Items.Add("Tea");
            WaterTypeName.Items.Add("Coca-Cola");
            WaterTypeName.Items.Add("Pepsi");
            WaterTypeName.SelectedIndex = 0;
        }
        private readonly Server server = new Server();
        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            Type water = typeof(Pepsi);
            switch (WaterTypeName.SelectedIndex)
            {
                case 0:
                    water = typeof(Tea);
                    break;
                case 1:
                    water = typeof(Coca_Cola);
                    break;
            }
            int a = -2;
            try
            {
                a = int.Parse(ChickenQuantityName.Text);
                if (a < 0)
                {
                    a = -11;
                    int.Parse("d");
                }
                a = -1;
                a = int.Parse(EggQuantityName.Text);
                if (a < 0)
                {
                    a = -22;
                    int.Parse("d");
                }
                server.Receive(CustomerName.Text,int.Parse(EggQuantityName.Text), int.Parse(ChickenQuantityName.Text), water);
            }
            catch (Exception exception)
            {
                switch (a)
                {
                    case -1:
                        MessageBox.Show("please input integer type in Egg Quantity");
                        break;
                    case -11:
                        MessageBox.Show("please input zero or natural number in Chicken Quantity");
                        break;
                    case -22:
                        MessageBox.Show("please input zero or natural number in Egg Quantity"); 
                        break;
                    case -2:
                        MessageBox.Show("please input integer type in Chicken Quantity");
                        break;
                }
            }
        }
        private void SendToCook_Click(object sender, RoutedEventArgs e)
        {
            if (IsShippedToCook)
            {
                MessageBox.Show("first order something");
            }
            else
            {
                server.Invoke();
                IsShippedToCook = true;
            }
        }

        private void ResultView_Click(object sender, RoutedEventArgs e)
        {
            IsShippedToCook = false;
            var outs = server.Serve();
            if (outs.Count == 0)
            {
                MessageBox.Show("no orders to give");
            }
            else
            {
                for (int i = 0; i < outs.Count; i++)
                {
                    ResultList.Items.Add(outs[i]);
                }
                
            }
            
        }
    }
}
