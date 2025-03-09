using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace GymProject
{
    public partial class MainPage : ContentPage
    {
        private List<string> bodybuildingQuotes = new List<string>
        {
            "No pain, no gain. - Arnold Schwarzenegger",
            "Everybody wants to be a bodybuilder, but don’t nobody want to lift no heavy-ass weights. - Ronnie Coleman",
            "Success isn’t always about greatness. It’s about consistency. - Dwayne 'The Rock' Johnson",
            "The distance between your dreams and reality is called discipline. - Lou Ferrigno",
            "It’s not about perfect. It’s about effort. - Jay Cutler"
        };

        public MainPage()
        {
            InitializeComponent();
            ShowRandomQuote();
        }

        private void ShowRandomQuote()
        {
            Random random = new Random();
            int index = random.Next(bodybuildingQuotes.Count);
            RandomQuoteLabel.Text = bodybuildingQuotes[index];
        }

        private async void OnPlanWorkoutClicked(object sender, EventArgs e)
        {
            // Navegar a WorkoutPlannerPage
            await Navigation.PushAsync(new GymProject.Views.WorkoutPlannerPage());
        }

        private async void OnViewCalendarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GymProject.Views.CalendarPage());
        }


        private async void OnViewProgressClicked(object sender, EventArgs e)
        {
          
            await Navigation.PushAsync(new GymProject.Views.ProgressPage());
        }

    }
}