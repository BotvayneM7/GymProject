using Microsoft.Maui.Controls;

namespace GymProject.Views
{
    public partial class WorkoutSchedulePage : ContentPage
    {
        public WorkoutSchedulePage()
        {
            InitializeComponent();
        }

        private async void OnContinueClicked(object sender, EventArgs e)
        {
            // Get user input values
            string workoutName = WorkoutNameEntry.Text;
            DateTime selectedDate = WorkoutDatePicker.Date;
            TimeSpan selectedTime = WorkoutTimePicker.Time;

            // Validate the workout name
            if (string.IsNullOrEmpty(workoutName))
            {
                await DisplayAlert("Invalid Input", "Please enter a workout name.", "OK");
                return;
            }

            // Combine date and time
            DateTime workoutDateTime = selectedDate.Add(selectedTime);

            // Navigate to WorkoutPlannerPage and pass the workout name and datetime
            await Navigation.PushAsync(new WorkoutPlannerPage(workoutName, workoutDateTime));
        }
        

    }
}