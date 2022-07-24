using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        List<Item> file1 = null;
        List<Item> file2 = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;

                int flag = 0;
                if (openFileDialog.ShowDialog() == true)
                {
                    if (openFileDialog.FileNames.Length != 2)
                        throw new Exception();
                    try
                    {
                        if (openFileDialog.FileNames[1] != null) 
                        { 
                        if (!openFileDialog.FileNames[0].Contains("упаковка") && !openFileDialog.FileNames[1].Contains("упаковка"))
                            throw new Exception();
                        }
                    }
                    catch 
                    {
                        MessageBox.Show("Выберите два подходящих файла.");
                        return;
                    }
                    foreach (var filename in openFileDialog.FileNames)
                    {
                        string fileExt = System.IO.Path.GetExtension(filename); 

                        if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                        {
                                if (filename.Contains("упаковка1"))
                                    ReadExcel(filename, fileExt, ref file1);

                                if (filename.Contains("упаковка2"))
                                    ReadExcel(filename, fileExt, ref file2);
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста выберите .xls или .xlsx.");
                            return;
                        }
                    }
                    if (file2 == null || file1 == null) 
                        throw new Exception();
                }

            }
            catch
            {
                MessageBox.Show("Выберите два файла.");
                return;
            }
            MessageBox.Show("Файл(ы) загружен(ы).");
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
                //if (file2 == null || file1 == null) 
                //    throw new NullReferenceException();
                if (Table.Children.Count > 0) 
                {
                    Table.Children.RemoveRange(0, Table.Children.Count);
                }

                for (int iter = 0; iter < 7; iter++) 
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(0,GridUnitType.Auto);
                    Table.ColumnDefinitions.Add(column);
                }
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(0, GridUnitType.Auto);
                Table.RowDefinitions.Add(row);

                TextBlock ID = new TextBlock(); ID.Text = "ID"; ID.Margin = new Thickness(10, 10, 10, 10);
                TextBlock Name = new TextBlock(); Name.Text = "Наименование"; Name.Margin = new Thickness(10, 10, 10, 10);
                TextBlock Code = new TextBlock(); Code.Text = "Шифр"; Code.Margin = new Thickness(10, 10, 10, 10);
                TextBlock DateFrom = new TextBlock(); DateFrom.Text = "Дата действия с"; DateFrom.Margin = new Thickness(10, 10, 10, 10);
                TextBlock DateTo = new TextBlock(); DateTo.Text = "Дата действия по"; DateTo.Margin = new Thickness(10, 10, 10, 10);
                TextBlock isExt = new TextBlock(); isExt.Text = "isExt"; isExt.Margin = new Thickness(10, 10, 10, 10);
                TextBlock ExtID = new TextBlock(); ExtID.Text = "ExtID"; ExtID.Margin = new Thickness(10, 10, 10, 10);

                Table.Children.Add(ID); Grid.SetColumn(ID, 0); Grid.SetRow(ID, 0);
                Table.Children.Add(Name); Grid.SetColumn(Name, 1); Grid.SetRow(Name, 0);
                Table.Children.Add(Code); Grid.SetColumn(Code, 2); Grid.SetRow(Code, 0);
                Table.Children.Add(DateFrom); Grid.SetColumn(DateFrom, 3); Grid.SetRow(DateFrom, 0);
                Table.Children.Add(DateTo); Grid.SetColumn(DateTo, 4); Grid.SetRow(DateTo, 0);
                Table.Children.Add(isExt); Grid.SetColumn(isExt, 5); Grid.SetRow(isExt, 0);
                Table.Children.Add(ExtID); Grid.SetColumn(ExtID, 6); Grid.SetRow(ExtID, 0);

                List<Item> resFile = new List<Item>();
                DateTime datefrom;
                DateTime dateto;
                try 
                {
                    datefrom = (DateTime)dateFrom.SelectedDate;
                }
                catch (Exception) 
                {
                    datefrom = DateTime.MinValue;
                }

                try
                {
                    dateto = (DateTime)dateTo.SelectedDate;
                }
                catch (Exception)
                {
                    dateto = DateTime.MaxValue;
                }

                try
                {
                    foreach (Item item in file1)
                    {
                        if (item.DateFrom <= datefrom && item.DateTo >= dateto)
                        {
                            resFile.Add(item);
                            resFile.Last().isExt = 0;
                            foreach (Item item2 in file2)
                            {
                                if (item.Name == item2.Name)
                                {
                                    resFile.Last().ExtID = item2.ID;
                                    resFile.Last().isExt = 1;

                                    if (item.DateFrom > item2.DateFrom)
                                        resFile.Last().DateFrom = item2.DateFrom;
                                    if (item.DateTo < item2.DateTo) resFile.Last().DateTo = item2.DateTo;
                                }
                            }
                        }
                    }
                    foreach (Item item in file2)
                    {
                        bool flag = true;
                        if (item.DateFrom <= datefrom && item.DateTo >= dateto)
                        {
                            foreach (Item item2 in resFile)
                            {
                                if (item.Name == item2.Name) { flag = false; break; }
                            }
                            if (flag)
                            {
                                resFile.Add(item);
                                resFile.Last().isExt = 1;
                                resFile.Last().ExtID = item.ID;
                            }
                            flag = true;
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Выберите подходящие файлы.");
                }
                int i = 0;
                foreach (Item item in resFile)
                {
                    i++;
                    Table.RowDefinitions.Add(new RowDefinition());
                    TextBlock newID = new TextBlock(); newID.Text = item.ID.ToString();
                    TextBlock newName = new TextBlock(); newName.Text = item.Name;
                    TextBlock newCode = new TextBlock(); newCode.Text = item.Code;
                    TextBlock newDateFrom = new TextBlock();
                    if (item.DateFrom == DateTime.MinValue)
                        newDateFrom.Text = "";
                    else
                        newDateFrom.Text = item.DateFrom.Date.ToShortDateString();
                    TextBlock newDateTo = new TextBlock();
                    if (item.DateTo == DateTime.MaxValue)
                        newDateTo.Text = "";
                    else
                        newDateTo.Text = item.DateTo.Date.ToShortDateString();
                    TextBlock newisExt = new TextBlock(); newisExt.Text = item.isExt.ToString();
                    TextBlock newExtID = new TextBlock();
                    if (item.ExtID == -1)
                        newExtID.Text = "";
                    else
                        newExtID.Text = item.ExtID.ToString();
                    Table.Children.Add(newID); Grid.SetColumn(newID, 0); Grid.SetRow(newID, i); newID.Margin = new Thickness(10, 10, 10, 10);
                    Table.Children.Add(newName); Grid.SetColumn(newName, 1); Grid.SetRow(newName, i); newName.Margin = new Thickness(10, 10, 10, 10);
                    Table.Children.Add(newCode); Grid.SetColumn(newCode, 2); Grid.SetRow(newCode, i); newCode.Margin = new Thickness(10, 10, 10, 10);
                    Table.Children.Add(newDateFrom); Grid.SetColumn(newDateFrom, 3); Grid.SetRow(newDateFrom, i); newDateFrom.Margin = new Thickness(10, 10, 10, 10);
                    Table.Children.Add(newDateTo); Grid.SetColumn(newDateTo, 4); Grid.SetRow(newDateTo, i); newDateTo.Margin = new Thickness(10, 10, 10, 10);
                    Table.Children.Add(newisExt); Grid.SetColumn(newisExt, 5); Grid.SetRow(newisExt, i); newisExt.Margin = new Thickness(10, 10, 10, 10);
                    Table.Children.Add(newExtID); Grid.SetColumn(newExtID, 6); Grid.SetRow(newExtID, i); newExtID.Margin = new Thickness(10, 10, 10, 10);
                }
            
            //catch (NullReferenceException)
            //{
            //    MessageBox.Show("Выберите подходящие файлы.");
            //}


            
        }
        void ReadExcel(string fileName, string fileExt, ref List<Item> file)
        {
            file = new List<Item>();
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //xls  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";"; //xslx
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    con.Open();
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Лист1$]", con);
                    oleAdpt.Fill(dtexcel);
                }
                catch { }
            }
            foreach (DataRow row in dtexcel.Rows)
            {
                Item item = new Item();
                item.ID = Convert.ToInt32(row[0].ToString());
                item.Name = row[1].ToString();
                item.Code = row[2].ToString();
                if (!DBNull.Value.Equals(row[3])) item.DateFrom = (DateTime)row[3];
                else item.DateFrom = DateTime.MinValue;
                if (!DBNull.Value.Equals(row[4])) item.DateTo = (DateTime)row[4];
                else item.DateTo = DateTime.MaxValue;

                file.Add(item); 
            }
        }
    }
}
