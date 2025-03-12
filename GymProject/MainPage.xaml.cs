using System;
using System.Collections.Generic;
using GymProject.Views;
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

        // ✅ Muestra una cita aleatoria
        private void ShowRandomQuote()
        {
            Random random = new Random();
            int index = random.Next(bodybuildingQuotes.Count);
            RandomQuoteLabel.Text = bodybuildingQuotes[index];
        }

        // ✅ Navegar a la página para agregar ejercicios
        private async void OnAddExerciseClicked(object sender, EventArgs e)
        {
            string workoutName = "New Custom Workout";
            DateTime workoutDateTime = DateTime.Now;
            await Navigation.PushAsync(new WorkoutPlannerPage(workoutName, workoutDateTime));
        }

        private async void OnViewProfileClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Profile", "This is your profile page.", "OK");
        }

        private async void OnViewCalendarClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Calendar", "Here is your calendar.", "OK");
        }

        private async void OnViewHomeClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Home", "You are already on Home.", "OK");
        }

        private async void OnViewProgressClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Progress", "This is your progress page.", "OK");
        }
    }
}
