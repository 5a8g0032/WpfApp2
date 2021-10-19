using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        List<Orderitem> order = new List<Orderitem>();
        List<Drink> drinks = new List<Drink>();

        public MainWindow()
        {
            InitializeComponent();
            drinks.Add(new Drink() { Name = "咖啡", Size = "大杯", Price = 60 });
            drinks.Add(new Drink() { Name = "咖啡", Size = "中杯", Price = 50 });
            drinks.Add(new Drink() { Name = "紅茶", Size = "大杯", Price = 30 });
            drinks.Add(new Drink() { Name = "紅茶", Size = "中杯", Price = 20 });
            drinks.Add(new Drink() { Name = "綠茶", Size = "大杯", Price = 30 });
            drinks.Add(new Drink() { Name = "綠茶", Size = "中杯", Price = 20 });

            DisplyDrinks(drinks);

        }
        string takeout;
    

        private void DisplyDrinks(List<Drink> myDrink)
        {
            foreach (Drink d in myDrink)
            {
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                Slider sl = new Slider();
                Label lb = new Label();

                sl.Width = 100;
                sl.Value = 0;
                sl.Minimum = 0;
                sl.Maximum = 10;
                sl.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
                sl.Value = 0;
                sl.IsSnapToTickEnabled = true;

                lb.Content = "0";
                lb.Width = 50;
                lb.Content = "0";

                cb.Content = d.Name + d.Size + d.Price;

                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(cb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);

                object p = drink_menu.Children.Add(sp);
                Binding myBinding = new Binding("Value");
                myBinding.Source = sl;
                lb.SetBinding(ContentProperty, myBinding);
            }
        }

        private void Rb_checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.IsChecked == true) takeout = rb.Content.ToString();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            order.Clear();
            for (int i = 0; i < drink_menu.Children.Count; i++)
            {
                StackPanel sp = drink_menu.Children[i] as StackPanel;
                CheckBox cb = sp.Children[0] as CheckBox;
                Slider sl = sp.Children[1] as Slider;
                int quantity = Convert.ToInt32(sl.Value);
                if (cb.IsChecked == true && quantity != 0)
                {
                    int price = drinks[i].Price;
                    order.Add(new Orderitem() { Index = i, Quantity = quantity, SubTotal = quantity * price });
                }
            }
            int total = 0, j = 1, sellPrice = 0;
            string message = "";
            NewMethod1();
            NewMethod();
            foreach (Orderitem oi in order)
            {
                total += oi.SubTotal;
                if (total >= 500)
                {
                    message = "訂購滿 500 元打 8 折";
                    sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.8));
                }
                else if (total >= 300 && total <= 500)
                {
                    message = "訂購滿 300 元打 85 折";
                    sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.85));
                }
                else if (total >= 200 && total <= 300)
                {
                    message = "訂購滿 200 元打 9 折";
                    sellPrice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.9));
                }
                else
                {
                    message = "未滿 500 或 1000 元";
                    sellPrice = total;
                }
                Textblock1.Text += $"第{j}項:{drinks[oi.Index].Name}{drinks[oi.Index].Size}訂購{oi.Quantity}杯,每杯{drinks[oi.Index].Price}元,總共{oi.Quantity}杯,小計{oi.SubTotal}元。\n";
                j++;
            }
            Textblock1.Text += $"訂購合計{sellPrice}元,{message}";
        }

        private void NewMethod1()
        {
            Textblock1.Text = "";
        }

        private void NewMethod()
        {
            Textblock1.Text += $"您的飲料是{takeout},訂購清單如下:\n";
        }
    }
}
