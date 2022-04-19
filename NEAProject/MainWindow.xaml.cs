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
        Manager manager = ((App)Application.Current).manager; 
        //this gives reference to where it is newed in App.xaml.cs

        Dictionary<string, Label> labels = new Dictionary<string, Label>();
        Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        Dictionary<string, TextBox> textboxes = new Dictionary<string, TextBox>();
        Dictionary<string, ComboBox> comboboxes = new Dictionary<string, ComboBox>();
        Dictionary<string, Expander> expanders = new Dictionary<string, Expander>();
        // Comic SearchedComic;
        // bool linknotfiltered = false;



        public MainWindow()
        {
            InitializeComponent();

            BuildMainMenu();

            //Activates the correct subroutines if buttons are clicked
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

            manager.FilteredAtts.Clear(); //Resets the FilteredAtts list

            foreach (string Att in Comic.AttirbuteNames)
            {
                if (Att != "Title")
                {
                    if (Convert.ToString(comboboxes[$"{Att}Filter"].SelectedItem) == "Yes")
                    {
                        manager.FilteredAtts.Add(Att); //Adds all the attributes the user selected to the FilteredAtts list.
                    }
                }
            }

            string ComicChoice = Convert.ToString(comboboxes["titles"].SelectedItem);//ComicChoice variable set to the choice of the user.
            
            List<Link> Links = manager.GraphComic.Graph[ComicChoice].OrderLinks();
            //Calls the OrderLinks subroutine
            List<string> ComicsDest = new List<string>(); //Creates a list of destination comics.

            foreach (Link link in Links)
            {
                string display = link.DestComicAndAttsAsString(manager.GraphComic.Graph[ComicChoice].thisComic);
                if (link.GetStrength(manager.FilteredAtts) != 0)// if the link's strength isn't 0, i.e. if the link isnt filtered out entirely
                    MessageBox.Show(display);// Display the link in a messagebox
                ComicsDest.Add(link.GetOtherComicName(manager.GraphComic.Graph[ComicChoice].thisComic));//Add the destination comic to the list.
            }

            Database.StoreSearch(1, ComicChoice, manager.FilteredAtts, ComicsDest); // Calls the StoreSearch method in Database

            
        }

        void Start_Recommendation(object sender, RoutedEventArgs e) //Called when the user presses the "Get Reccomendation" button.
        {
            BuildUI(); //Calls the subroutine to build the UI for getting reccomendations
            buttons["selection"].Click += new RoutedEventHandler(Btn_confirm_Click);
            //if selection is clicked, execute appropriate subroutine
        }

        void Start_History(object sender, RoutedEventArgs e)
        {
            Show_History();
        }

        public void BuildMainMenu()
        {
            labels["getrecommend"] = UtilsGui.CreateLabel("Would you like to get a comic recommendation?", "getrecommend");
            Skp_Main.Children.Add(labels["getrecommend"]); //creates and inserts a label
            buttons["gotorecommend"] = UtilsGui.CreateButton("Get Recommendation", "gotorecommend");
            Skp_Main.Children.Add(buttons["gotorecommend"]); // creates and inserts the "Get Recommendation" button

            labels["seehistory"] = UtilsGui.CreateLabel("Or would you like to see your recommendation history?", "seehistory");
            Skp_Main.Children.Add(labels["seehistory"]); //creates and inserts a label
            buttons["gotohistory"] = UtilsGui.CreateButton("See History", "gotohistory");
            Skp_Main.Children.Add(buttons["gotohistory"]); // creates and inserts the "See History" button
        }

        public void BuildUI()
        {
            labels["comic"] = UtilsGui.CreateLabel("Which comic would you like to search?", "comic");
            Skp_Main.Children.Add(labels["comic"]); //Adds the above label

            comboboxes["titles"] = UtilsGui.CreateComboBox("titles"); //Creates a combobox, a dropdown menu with a list of choices.

            foreach (string Title in manager.GraphComic.GetTitles()) // cycles through each comic in the GraphComic list and adds the titles to the combobox.
            {
                comboboxes["titles"].Items.Add(Title);
            }
            Skp_Main.Children.Add(comboboxes["titles"]);

            foreach (string Att in Comic.AttirbuteNames)
            {
                if (Att != "Title")
                {   //Creates a combobox for each possible attribute, not including title, and asks if the user wants to filter that attribute out.
                    //The options for this combobox are yes and no.
                    labels[Att] = UtilsGui.CreateLabel($"Would you like to filter out suggestions including {Att}?", Att);
                    Skp_Main.Children.Add(labels[Att]);
                    comboboxes[$"{Att}Filter"] = UtilsGui.CreateComboBox($"{Att}Filter");
                    comboboxes[$"{Att}Filter"].Items.Add("Yes");
                    comboboxes[$"{Att}Filter"].Items.Add("No");
                    comboboxes[$"{Att}Filter"].SelectedIndex = 1;
                    Skp_Main.Children.Add(comboboxes[$"{Att}Filter"]);
                }
            }

            buttons["selection"] = UtilsGui.CreateButton("Confirm", "selection");//Creates a button called "selection"
            Skp_Main.Children.Add(buttons["selection"]);
        }

        public void Show_History()
        {
            History history = Database.GetSearchHistoryForUser(); //Executes the GetSearchHistory subroutine and stores the result
            labels["SearchedComicName"] = UtilsGui.CreateLabel($"Searched comic:{history.Comic_Source.GetTitle()}", "SourceComicName");
            Skp_Main.Children.Add(labels["SearchedComicName"]); //creates a new label with the name of the searched comic

            expanders["SearchComInfo"] = UtilsGui.CreateExpander("SearchComInfo", $"See more info on {history.Comic_Source.GetTitle()}", history.Comic_Source.GetInfo());
            Skp_Main.Children.Add(expanders["SearchComInfo"]); //Creates an expander with more info on the searched comic.

            labels["DateOfSearch"] = UtilsGui.CreateLabel($"Date and time of search: {history.Date_Time}", "DateOfSearch");
            Skp_Main.Children.Add(labels["DateOfSearch"]); //Shows the date and time of the search

            string FiltersAsString ="";
            foreach (string filter in history.Active_Filters) //Adds each of the filters that were active to a new list
            {
                if (filter != history.Active_Filters[0])
                {
                    FiltersAsString += $", {filter}";
                }
                else
                    FiltersAsString += filter;
            }


            labels["FiltersUsed"] = UtilsGui.CreateLabel($"Filters used in this search: {FiltersAsString}", "FiltersUsed");
            Skp_Main.Children.Add(labels["FiltersUsed"]); //Displays the previously active filters


            foreach (Comic comic in history.Dest_Comics)
            {
                labels[$"DestComic{comic.GetTitle()}"] = UtilsGui.CreateLabel($"Recommended comic:{comic.GetTitle()}", $"DestComic{comic.GetTitle()}");
                Skp_Main.Children.Add(labels[$"DestComic{comic.GetTitle()}"]); //Displays each destination comic title

                expanders[$"DestCom{comic.GetTitle()}Info"] = UtilsGui.CreateExpander($"DestCom{comic.GetTitle()}Info", $"See more info on {comic.GetTitle()}", comic.GetInfo());
                Skp_Main.Children.Add(expanders[$"DestCom{comic.GetTitle()}Info"]); //Creates an expander with more info on each one
            }

            
        }



    }
}
