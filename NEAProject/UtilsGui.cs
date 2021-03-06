using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace NEAProject
{
    //All borrowed from login app
    class UtilsGui
    {
        public static Button CreateButton(string Display, string Name)
        {
            Button Btn = new Button
            {
                Content = Display,
                Name = "Btn_" + Name
            };
            return Btn;
        }

        public static TextBox CreateTextBox(string Name)
        {
            TextBox Txt = new TextBox
            {
                Name = "Txt_" + Name
            };
            return Txt;
        }

        public static Label CreateLabel(string Display, string Name)
        {
            Label Lbl = new Label
            {
                Content = Display,
                Name = "Lbl_" + Name.Replace(" ", "_")
        };
            return Lbl;
        }

        public static ComboBox CreateComboBox(string Name)
        {
            ComboBox Cmb = new ComboBox
            {
                Name = "Cmb_" + Name
            };
            return Cmb;
        }

        public static Expander CreateExpander(string Name,string Header, string Content)
        {
            Expander Exp = new Expander()
            {
                Name = "Exp_" + Name.Replace(" ", "_")
            };
                Exp.Header = Header;
                Exp.HorizontalAlignment = HorizontalAlignment.Left;
                Exp.Width = 250;
                Exp.IsExpanded = false;
                Exp.Content = Content;
                return Exp;
            }
        }
    }
;
