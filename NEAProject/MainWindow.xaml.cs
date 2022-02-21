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

namespace NEAProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // instance properties
        Manager PorkPie = ((App)Application.Current).manager; // stupid but memorable variable name, change this at some point
        //this gives reference to where it is newed in App.xaml.cs

        Dictionary<string, Label> labels = new Dictionary<string, Label>();
        Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        Dictionary<string, TextBox> textboxes = new Dictionary<string, TextBox>();
        Dictionary<string, ComboBox> comboboxes = new Dictionary<string, ComboBox>();
        Comic SearchedComic;
        bool linknotfiltered = false;



        public MainWindow()
        {
            InitializeComponent();

            BuildMainMenu();

            //Add thing here to go to recommendation window if button clicked
            buttons["gotorecommend"].Click += new RoutedEventHandler(Start_Recommendation);
            buttons["gotohistory"].Click += new RoutedEventHandler(Start_History);
        }
        /// <summary>
        /// Presents results when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            //foreach (Comic comic in PorkPie.GraphComic.GetComics())
            //{
            //MessageBox.Show(comic.GetTitle());
            //}

            PorkPie.FilteredAtts.Clear();

            foreach (string Att in Comic.AttirbuteNames)
            {
                if (Att != "Title")
                {
                    if (Convert.ToString(comboboxes[$"{Att}Filter"].SelectedItem) == "Yes")
                    {
                        PorkPie.FilteredAtts.Add(Att);
                    }
                }
            }

                string ComicChoice = Convert.ToString(comboboxes["titles"].SelectedItem);
            
            List<Link> Links = PorkPie.GraphComic.Graph[ComicChoice].OrderLinks(PorkPie.GraphComic.Graph[ComicChoice]);
            List<string> ComicsDest = new List<string>();

            foreach (Link link in Links)
            {
                string display = link.DestComicAndAttsAsString(PorkPie.GraphComic.Graph[ComicChoice].thisComic);
                if (link.GetStrength(PorkPie.FilteredAtts) != 0)
                    MessageBox.Show(display);
                ComicsDest.Add(link.GetOtherComicName(PorkPie.GraphComic.Graph[ComicChoice].thisComic));
            }

            Database.StoreSearch(1, ComicChoice, PorkPie.FilteredAtts, ComicsDest);

            
        }

        void Start_Recommendation(object sender, RoutedEventArgs e)
        {
            BuildUI();
            buttons["selection"].Click += new RoutedEventHandler(Btn_confirm_Click);
        }

        void Start_History(object sender, RoutedEventArgs e)
        {
            Show_History();
        }

        public void BuildMainMenu()
        {
            labels["getrecommend"] = UtilsGui.CreateLabel("Would you like to get a comic recommendation?", "getrecommend");
            Skp_Main.Children.Add(labels["getrecommend"]);
            buttons["gotorecommend"] = UtilsGui.CreateButton("Get Recommendation", "gotorecommend");
            Skp_Main.Children.Add(buttons["gotorecommend"]);

            labels["seehistory"] = UtilsGui.CreateLabel("Or would you like to see your recommendation history?", "seehistory");
            Skp_Main.Children.Add(labels["seehistory"]);
            buttons["gotohistory"] = UtilsGui.CreateButton("See History", "gotohistory");
            Skp_Main.Children.Add(buttons["gotohistory"]);
        }

        public void BuildUI()
        {
            labels["comic"] = UtilsGui.CreateLabel("Which comic would you like to search?", "comic");
            Skp_Main.Children.Add(labels["comic"]);

            comboboxes["titles"] = UtilsGui.CreateComboBox("titles");

            foreach (string Title in PorkPie.GraphComic.GetTitles())
            {
                comboboxes["titles"].Items.Add(Title);
                //    foreach (string Attribute in comic.GetAllAttributeNames())
                //    {
                //        if (Attribute == "Title")
                //            comboboxes["attributes"].Items.Add(comic.Atts[Attribute]);
                //    }

            }
            Skp_Main.Children.Add(comboboxes["titles"]);

            foreach (string Att in Comic.AttirbuteNames)
            {
                if (Att != "Title")
                {
                    labels[Att] = UtilsGui.CreateLabel($"Would you like to filter out suggestions including {Att}?", Att);
                    Skp_Main.Children.Add(labels[Att]);
                    comboboxes[$"{Att}Filter"] = UtilsGui.CreateComboBox($"{Att}Filter");
                    comboboxes[$"{Att}Filter"].Items.Add("Yes");
                    comboboxes[$"{Att}Filter"].Items.Add("No");
                    comboboxes[$"{Att}Filter"].SelectedIndex = 1;
                    Skp_Main.Children.Add(comboboxes[$"{Att}Filter"]);
                }
            }

            buttons["selection"] = UtilsGui.CreateButton("Confirm", "selection");
            Skp_Main.Children.Add(buttons["selection"]);
        }

        public void Show_History()
        {
            History history = Database.GetSearchHistoryForUser();

        }

    }
}
