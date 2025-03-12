using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace GymProject.Views
{
    public partial class WorkoutPlannerPage : ContentPage
    {
        public ObservableCollection<Exercise> ExercisesList { get; set; }
        private int TotalGymTime { get; set; }
        public DateTime WorkoutDateTime { get; set; }

        public WorkoutPlannerPage(string workoutName, DateTime workoutDateTime)
        {
            InitializeComponent();
            WorkoutDateTime = workoutDateTime;
            ExercisesList = new ObservableCollection<Exercise>();
            ExercisesListView.ItemsSource = ExercisesList;
            BindingContext = this;
        }

        private void OnAddExerciseClicked(object sender, EventArgs e)
        {
            string exercise = ExerciseEntry.Text;
            string sets = SetsEntry.Text;
            string reps = RepsEntry.Text;
            string restTime = RestTimeEntry.Text;

            if (string.IsNullOrEmpty(exercise) || string.IsNullOrEmpty(sets) ||
                string.IsNullOrEmpty(reps) || string.IsNullOrEmpty(restTime))
            {
                DisplayAlert("Invalid Input", "Please fill in all fields.", "OK");
                return;
            }

            int setsCount = ParseNumber(sets);
            int repsCount = ParseNumber(reps);
            int restTimeInMinutes = ParseNumber(restTime);

            if (setsCount == -1 || repsCount == -1 || restTimeInMinutes == -1)
            {
                DisplayAlert("Invalid Input", "Please enter valid numeric values.", "OK");
                return;
            }

            int totalTimeAtGym = CalculateTotalGymTime(setsCount, restTimeInMinutes);
            var newExercise = new Exercise(exercise, setsCount, repsCount, restTimeInMinutes, totalTimeAtGym);

            ExercisesList.Insert(0, newExercise);
            TotalGymTime += totalTimeAtGym;
            TotalTimeLabel.Text = $"Total Time: {TotalGymTime} mins";

            ClearInputFields();
        }

        private void OnCreateWorkoutClicked(object sender, EventArgs e)
        {
            if (ExercisesList.Count == 0)
            {
                DisplayAlert("Empty Workout", "You haven't added any exercises yet.", "OK");
                return;
            }

            // ? Aquí puedes enviar el workout a otra página, base de datos, etc.
            DisplayAlert("Workout Created", $"You have successfully created your workout.\nTotal Time: {TotalGymTime} mins.", "OK");

            // Limpia la lista después de crear el workout
            ExercisesList.Clear();
            TotalGymTime = 0;
            TotalTimeLabel.Text = "Total Time: 0 mins";
        }

        private void ClearInputFields()
        {
            ExerciseEntry.Text = "";
            SetsEntry.Text = "";
            RepsEntry.Text = "";
            RestTimeEntry.Text = "";
        }

        private int ParseNumber(string input)
        {
            if (int.TryParse(input, out int result))
                return result;

            var parts = input.Split('-');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int minValue) &&
                int.TryParse(parts[1], out int maxValue))
            {
                return (minValue + maxValue) / 2;
            }

            return -1;
        }

        private int CalculateTotalGymTime(int sets, int restTime)
        {
            int exerciseTime = sets * 1;
            int totalRestTime = (sets - 1) * restTime;
            return exerciseTime + totalRestTime;
        }
    }

    public class Exercise
    {
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int RestTime { get; set; }
        public int TotalTimeAtGym { get; set; }

        public Exercise(string exerciseName, int sets, int reps, int restTime, int totalTimeAtGym)
        {
            ExerciseName = exerciseName;
            Sets = sets;
            Reps = reps;
            RestTime = restTime;
            TotalTimeAtGym = totalTimeAtGym;
        }

        public string RepsSets => $"{Sets}x{Reps}";
    }
}
