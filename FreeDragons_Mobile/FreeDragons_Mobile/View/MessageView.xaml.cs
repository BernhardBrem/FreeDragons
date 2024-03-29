﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreeDragons_Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageView : ContentView
    {
        public MessageView()
        {
            InitializeComponent();
            OK.Clicked += OK_Clicked; 
            IsVisible = false;
        }

        public void SetContent(string heading, string content)
        {
            this.Heading.Text = heading;
            this.TheContent.Text = content;
        }

        public void Message(string heading,string content)
        {
            SetContent(heading, content);
            IsVisible = true;
        }

        private void OK_Clicked(object sender, EventArgs e)
        {
            this.IsVisible=false;
        }
    }
}