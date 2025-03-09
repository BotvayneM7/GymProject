using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace GymProject.Views
{
    public partial class WorkoutPlannerPage : ContentPage
    {
        // ObservableCollection to bind to the ListView
        public ObservableCollection<Exercise> ExercisesList { get; set; }
        private int TotalGymTime { get; set; }

        public WorkoutPlannerPage()
        {
            InitializeComponent();
            ExercisesList = new ObservableCollection<Exercise>();
            BindingContext = this; // Set the BindingContext to the current page
        }

        // This is the handler for the "Add Exercise" button click
        private void OnAddExerciseClicked(object sender, EventArgs e)
        {
            // Get user input values
            string workoutName = WorkoutNameEntry.Text;
            string exercise = ExerciseEntry.Text;
            string repsSets = RepsSetsEntry.Text;
            string restTime = RestTimeEntry.Text;

            // Validate the inputs
            if (string.IsNullOrEmpty(workoutName) || string.IsNullOrEmpty(exercise) || string.IsNullOrEmpty(repsSets) || string.IsNullOrEmpty(restTime))
            {
                DisplayAlert("Invalid Input", "Please fill in all fields.", "OK");
                return;
            }

            // Validate and parse RestTime
            int restTimeInMinutes = ParseRestTime(restTime);
            if (restTimeInMinutes == -1)
            {
                DisplayAlert("Invalid Input", "Please enter a valid rest time (e.g., 3-4 or 3).", "OK");
                return;
            }

            // Calculate the total time at the gym for this exercise
            int totalTimeAtGym = CalculateTotalGymTime(repsSets, restTimeInMinutes);

            // Create a new exercise object
            var newExercise = new Exercise
            {
                ExerciseName = exercise,
                RepsSets = repsSets,
                RestTime = restTimeInMinutes,
                TotalTimeAtGym = totalTimeAtGym
            };

            // Add the exercise to the list
            ExercisesList.Add(newExercise);

            // Update the total gym time
            TotalGymTime += totalTimeAtGym;
            TotalTimeLabel.Text = $"Total Time: {TotalGymTime} mins";

            // Clear the input fields
            WorkoutNameEntry.Text = "";
            ExerciseEntry.Text = "";
            RepsSetsEntry.Text = "";
            RestTimeEntry.Text = "";
        }

        // Helper function to validate and parse rest time (could be a range or a single integer)
        private int ParseRestTime(string restTime)
        {
            // Check if it is a valid single integer
            if (int.TryParse(restTime, out int result))
            {
                return result; // Return single integer
            }
            else
            {
                // Check if it is a valid range (e.g., 3-4)
                var parts = restTime.Split('-');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int minTime) &&
                    int.TryParse(parts[1], out int maxTime) &&
                    minTime <= maxTime)
                {
                    // Return the average of the range
                    return (minTime + maxTime) / 2;
                }
            }
            return -1; // Invalid rest time format
        }

        // Helper function to calculate total gym time (exercise time + rest time)
        private int CalculateTotalGymTime(string repsSets, int restTimeInMinutes)
        {
            // Assuming each set takes 1 minute (you can adjust this logic)
            string[] repsSetsArray = repsSets.Split('x');
            if (repsSetsArray.Length == 2 &&
                int.TryParse(repsSetsArray[0], out int sets) &&
                int.TryParse(repsSetsArray[1], out int reps))
            {
                // Total time = (sets * 1 minute for each set) + (rest time between sets)
                int exerciseTime = sets * 1; // Assuming 1 minute per set
                int totalRestTime = (sets - 1) * restTimeInMinutes; // Rest time between sets (no rest after last set)
                return exerciseTime + totalRestTime;
            }
            return 0; // Return 0 if reps/sets format is invalid
        }
    }

    // Model class to represent an exercise
    public class Exercise
    {
        public string ExerciseName { get; set; }
        public string RepsSets { get; set; }
        public int RestTime { get; set; }
        public int TotalTimeAtGym { get; set; }
    }
}