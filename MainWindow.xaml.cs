using System;
using System.Collections.Generic;
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
using Microsoft.Win32;

namespace ArchiView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartUp();
        }
        int ChoosedState = 0;

        BitmapImage[,] bitmaps = new BitmapImage[9,26];
        
        


        void StartUp()
        {
            // 0 = Top
            //1 = front
            //2= right
            //3 = left
            //4 = back

            //5= LF left Front
            // 6 = RF 
            //7= LB left back
            // 8 = RB
            //
            //9 = Left Front Top
            //10 = R F T
            //11 = LEft Back Top
            //12 = R B T
            //13 =Front Top
            //14 = L T
            //15 = R T
            //16 = Back Top
            //17 = Bottom
            //18= Front Bottom
            //19 = left Bottom
            //20 R Bottom
            //21 Back Bottom

            //22 Front Left Bottom
            //23 Front Right Bottom
            //24 Back Left Bottm
            //25 Back Rigt Bottom
            for (int i = 0; i < 9; i++)
            {
                SetUpFirtsImages(0,i, "pack://application:,,,/Images/Top.png");
                SetUpFirtsImages(1, i, "pack://application:,,,/Images/Front.png");
                SetUpFirtsImages(2, i, "pack://application:,,,/Images/Right.png");
                SetUpFirtsImages(3, i, "pack://application:,,,/Images/Left.png");
                SetUpFirtsImages(4, i, "pack://application:,,,/Images/Back.png");
                SetUpFirtsImages(5, i, "pack://application:,,,/Images/Left_Front.png");
                SetUpFirtsImages(6, i, "pack://application:,,,/Images/Right_Front.png");
                SetUpFirtsImages(7, i, "pack://application:,,,/Images/Left_Back.png");
                SetUpFirtsImages(8, i, "pack://application:,,,/Images/Right_Back.png");
                SetUpFirtsImages(9, i, "pack://application:,,,/Images/Left_Front_Top.png");
                SetUpFirtsImages(10, i, "pack://application:,,,/Images/Right_Front_Top.png");
                SetUpFirtsImages(11, i, "pack://application:,,,/Images/Left_Back_Top.png");
                SetUpFirtsImages(12, i, "pack://application:,,,/Images/Right_Back_Top.png");
                SetUpFirtsImages(13, i, "pack://application:,,,/Images/Front_Top.png");
                SetUpFirtsImages(14, i, "pack://application:,,,/Images/Left_Top.png");
                SetUpFirtsImages(15, i, "pack://application:,,,/Images/Right_Top.png");
                SetUpFirtsImages(16, i, "pack://application:,,,/Images/Back_Top.png");
                SetUpFirtsImages(17, i, "pack://application:,,,/Images/Bottom.png");
                SetUpFirtsImages(18, i, "pack://application:,,,/Images/Front_Bottom.png");
                SetUpFirtsImages(19, i, "pack://application:,,,/Images/Left_Bottom.png");
                SetUpFirtsImages(20, i, "pack://application:,,,/Images/Right_Bottom.png");
                SetUpFirtsImages(21, i, "pack://application:,,,/Images/Back_Bottom.png");
                SetUpFirtsImages(22, i, "pack://application:,,,/Images/Front_Left_Bottom.png");
                SetUpFirtsImages(23, i, "pack://application:,,,/Images/Front_Right_Bottom.png");
                SetUpFirtsImages(24, i, "pack://application:,,,/Images/Back_Left_Bottom.png");
                SetUpFirtsImages(25, i, "pack://application:,,,/Images/Back_Right_Bottom.png");
            }
            
            
        }

        void SetUpFirtsImages(int state,int slot,string Uri)
        {
            BitmapImage bitimg = new();
            bitimg.BeginInit();

            bitimg.UriSource = new Uri(Uri);
            bitimg.EndInit();

            // bitmaps.Insert(ChoosedState, bitimg);
            bitmaps[slot,state] = bitimg;
        }
        bool hideWelcome = false;
        void SetImage (int State)
        {
            ImageBox.Source = bitmaps[Slot.SelectedIndex ,State];
            WelcomeGrid.Visibility = Visibility.Hidden;
            hideWelcome = true;

        }
        Microsoft.Win32.SaveFileDialog dialog = new();
        public void SaveFile()
        {
            string file_path;
            dialog.FileName = "ArchiView Format";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*txt";
            Nullable<bool> result = dialog.ShowDialog();
            file_path = dialog.FileName;
            System.IO.StreamWriter SaveFile = new(file_path);

            //SaveFile.WriteLine(" نام" + "       |       " + "تعداد" + "         |        " + "فی" + "      |      " + "قیمت کل");

            for (int v = 0; v < 9; v++)
            {
                for (int i = 0; i < 25; i++)
                {
                    SaveFile.WriteLine(bitmaps[v, i]);
                }
            }
           
                
            
            

            SaveFile.ToString();
            SaveFile.Close();

            MessageBox.Show("Saved!", "ArchiView", MessageBoxButton.OK);

        }
        Microsoft.Win32.OpenFileDialog Loaddialog = new();
        public void LoadFile()
        {
            string file_path;
            Loaddialog.FileName = "ArchiView Format";
            Loaddialog.DefaultExt = ".txt";
            Loaddialog.Filter = "Text documents (.txt)|*txt";
            Nullable<bool> result = Loaddialog.ShowDialog();
            file_path = Loaddialog.FileName;
            //System.IO.StreamWriter SaveFile = new(file_path);
            System.IO.StreamReader LoadFile = new(file_path);

            

            //SaveFile.WriteLine(" نام" + "       |       " + "تعداد" + "         |        " + "فی" + "      |      " + "قیمت کل");

            for (int v = 0; v < 9; v++)
            { 
                for (int i = 0; i < 25; i++)
                {
                    
                    SetUpFirtsImages(i,v, LoadFile.ReadLine());

                }
            }





            //LoadFile.ToString();

            LoadFile.Close();
            SetImage(ChoosedState);

          
            MessageBox.Show("File Loaded!", "ArchiView", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        Button lastBtn = null;

        private void ChangeBtnColor (object sender)
        {
            Button current = (Button)sender;
            
            current.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            if (lastBtn != null)
            {
                lastBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221 ,221));
            }
            RBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            //
            LFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            Frontbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Frontbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Rightbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Rightbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Leftbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Leftbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Backbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            Backbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            lastBtn = current;
        }

        private void ChangeBtnColor(object sender, Button btn)
        {
            Button current = (Button)sender;


            current.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            if (lastBtn != null)
            {
                lastBtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                btn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            }

            lastBtn = current;
        }

        private void AddImg_Click(object sender, RoutedEventArgs e)
        {
           
           

            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = "c:\\";
            dlg.Filter = "All Image Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|PNG(*.png)|*.png|Image files (*.jpg)|*.jpg; *jpeg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
      try
                {
                    BitmapImage bitimg = new();
                bitimg.BeginInit();
                bitimg.UriSource = new Uri(@selectedFileName);
                

                bitimg.EndInit();

                //bitmaps.Insert(ChoosedState, bitimg);
                bitmaps[Slot.SelectedIndex ,ChoosedState] = bitimg;
                } catch(NotSupportedException)
                {
                    MessageBox.Show("Selected File Format Isn't Supported!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                

                SetImage(ChoosedState);

            }

        }

       

        private void Topbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 0;
            SetImage(ChoosedState);
            ChangeBtnColor(sender);
           
        }
        
        private void Frontbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 1;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            Frontbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            Frontbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
        }
        
        private void Rightbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 2;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            Rightbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            Rightbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
        }

        private void Leftbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 3;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            Leftbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            Leftbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
        }

       

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 4;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            Backbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            Backbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
        }

        private void LFbtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 5;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            LFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            LFbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            RFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            //
            LFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            LFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            RFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));




        }

        private void RFbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 6;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            LFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            RFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            LBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            //
            LFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            RFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            LBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void LBbtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 7;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            LFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            LBbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            RBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            //
            LFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            LBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            RBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void RBbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 8;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
            RBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            RBbtn.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));

            LFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            //
            LFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RFbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            RFbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            LBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            LBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            RBbtn_Copy1.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));
            RBbtn_Copy2.Background = new SolidColorBrush(Color.FromRgb(163, 229, 134));


        }

        private void TFLbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 9;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void TFRbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 10;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void TBLbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 11;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void TBRbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 12;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void TFbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 13;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void TLbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 14;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void TRbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 15;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void TBbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 16;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        

        private void Bottombtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 17;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BFbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 18;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BLbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 19;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BRbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 20;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BBbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 21;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BFLbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 22;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BFRbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 23;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BBLbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 24;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void BBRbtn_Click(object sender, RoutedEventArgs e)
        {
            ChoosedState = 25;
            ChangeBtnColor(sender);
            SetImage(ChoosedState);
        }

        private void RemoveImg_Click(object sender, RoutedEventArgs e)
        {
            
            switch (ChoosedState) {
                case 0:
                    SetUpFirtsImages(0,Slot.SelectedIndex, "pack://application:,,,/Images/Top.png"); SetImage(ChoosedState);
                    break;
                case 1:
                    SetUpFirtsImages(1, Slot.SelectedIndex, "pack://application:,,,/Images/Front.png"); SetImage(ChoosedState);
                    break;
                case 2: SetUpFirtsImages(2, Slot.SelectedIndex, "pack://application:,,,/Images/Right.png"); SetImage(ChoosedState); break;
                case 3: SetUpFirtsImages(3, Slot.SelectedIndex, "pack://application:,,,/Images/Left.png"); SetImage(ChoosedState); break;
                case 4:
                    SetUpFirtsImages(4, Slot.SelectedIndex, "pack://application:,,,/Images/Back.png"); SetImage(ChoosedState);
                    break;
                case 5:
                    SetUpFirtsImages(5, Slot.SelectedIndex, "pack://application:,,,/Images/Left_Front.png"); SetImage(ChoosedState);
                    break;
                case 6: SetUpFirtsImages(6, Slot.SelectedIndex, "pack://application:,,,/Images/Right_Front.png"); SetImage(ChoosedState); break;

                case 7:
                    SetUpFirtsImages(7, Slot.SelectedIndex, "pack://application:,,,/Images/Left_Back.png"); SetImage(ChoosedState); break;
                case 8:
                    SetUpFirtsImages(8, Slot.SelectedIndex, "pack://application:,,,/Images/Right_Back.png"); SetImage(ChoosedState); break;
                case 9:
                    SetUpFirtsImages(9, Slot.SelectedIndex, "pack://application:,,,/Images/Left_Front_Top.png"); SetImage(ChoosedState); break;

                case 10:
                    SetUpFirtsImages(10, Slot.SelectedIndex, "pack://application:,,,/Images/Right_Front_Top.png"); SetImage(ChoosedState); break;

                case 11:
                    SetUpFirtsImages(11, Slot.SelectedIndex, "pack://application:,,,/Images/Left_Back_Top.png"); SetImage(ChoosedState); break;

                case 12:
                    SetUpFirtsImages(12, Slot.SelectedIndex, "pack://application:,,,/Images/Right_Back_Top.png"); SetImage(ChoosedState); break;

                case 13:
                    SetUpFirtsImages(13, Slot.SelectedIndex, "pack://application:,,,/Images/Front_Top.png"); SetImage(ChoosedState); break;

                case 14:
                    SetUpFirtsImages(14, Slot.SelectedIndex, "pack://application:,,,/Images/Left_Top.png"); SetImage(ChoosedState); break;
                case 15:
                    SetUpFirtsImages(15, Slot.SelectedIndex, "pack://application:,,,/Images/Right_Top.png"); SetImage(ChoosedState); break;
                case 16:
                    SetUpFirtsImages(16, Slot.SelectedIndex, "pack://application:,,,/Images/Back_Top.png"); SetImage(ChoosedState); break;
                case 17:
                    SetUpFirtsImages(17, Slot.SelectedIndex, "pack://application:,,,/Images/Bottom.png"); SetImage(ChoosedState); break;
                case 18:
                    SetUpFirtsImages(18, Slot.SelectedIndex, "pack://application:,,,/Images/Front_Bottom.png"); SetImage(ChoosedState); break;
                case 19:
                    SetUpFirtsImages(19, Slot.SelectedIndex, "pack://application:,,,/Images/Left_Bottom.png"); SetImage(ChoosedState); break;
                case 20:
                    SetUpFirtsImages(20, Slot.SelectedIndex, "pack://application:,,,/Images/Right_Bottom.png"); SetImage(ChoosedState); break;
                case 21:
                    SetUpFirtsImages(21, Slot.SelectedIndex, "pack://application:,,,/Images/Back_Bottom.png"); SetImage(ChoosedState); break;
                case 22:
                    SetUpFirtsImages(22, Slot.SelectedIndex, "pack://application:,,,/Images/Front_Left_Bottom.png"); SetImage(ChoosedState); break;
                case 23:
                    SetUpFirtsImages(23, Slot.SelectedIndex, "pack://application:,,,/Images/Front_Right_Bottom.png"); SetImage(ChoosedState); break;
                case 24:
                    SetUpFirtsImages(24, Slot.SelectedIndex, "pack://application:,,,/Images/Back_Left_Bottom.png"); SetImage(ChoosedState); break;
                case 25:
                    SetUpFirtsImages(25, Slot.SelectedIndex, "pack://application:,,,/Images/Back_Right_Bottom.png"); SetImage(ChoosedState); break;

                default:
                    SetUpFirtsImages(0, Slot.SelectedIndex, "pack://application:,,,/Images/Top.png"); SetImage(ChoosedState);
                    break;

                    
            }


        }

        private void Slot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hideWelcome)
            SetImage(ChoosedState);
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void Loadbtn_Click(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }
    }

   
   

    }
