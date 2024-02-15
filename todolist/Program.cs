using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace todolist
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            string connectionString = "To-do List";

            // Example usage of CRUD operations
            CreateTask(connectionString, 1, "Sample Task", "This is a sample task", DateTime.Now.AddDays(1));
            ReadTasks(connectionString, 1);

            CreateImportant(connectionString, 1, "Important Item", "This is an important item");
            ReadImportant(connectionString, 1);

            CreatePlanned(connectionString, 1, "Planned Task", "This is a planned task", DateTime.Now.AddDays(7));
            ReadPlanned(connectionString, 1);

            // Example usage of marking a task as completed and moving it to the Completed table
            MarkTaskAsCompleted(connectionString, 1, 1); // Assuming task with TaskID 1 for UserID 1 is completed
            // Example usage of marking an important item as completed and moving it to the Completed table
            MarkImportantAsCompleted(connectionString, 1, 1); // Assuming important item with ImportantID 1 for UserID 1 is completed
            // Example usage of marking a planned task as completed and moving it to the Completed table
            MarkPlannedAsCompleted(connectionString, 1, 1); // Assuming planned task with PlannedID 1 for UserID 1 is completed

            ReadAllTasks(connectionString, 1);
        }

        static void CreateTask(string connectionString, int userID, string taskName, string description, DateTime? dueDate)
        {
            string insertQuery = "INSERT INTO Tasks (UserID, TaskName, Description, DueDate) VALUES (@UserID, @TaskName, @Description, @DueDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@TaskName", taskName);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@DueDate", (object)dueDate ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        static void ReadTasks(string connectionString, int userID)
        {
            string selectQuery = "SELECT TaskID, TaskName, Description, DueDate, Completed FROM Tasks WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"TaskID: {reader["TaskID"]}, TaskName: {reader["TaskName"]}, Description: {reader["Description"]}, DueDate: {reader["DueDate"]}, Completed: {reader["Completed"]}");
                    }
                }
            }
        }

        static void UpdateTask(string connectionString, int taskID, string taskName, string description, DateTime? dueDate, bool completed)
        {
            string updateQuery = "UPDATE Tasks SET TaskName = @TaskName, Description = @Description, DueDate = @DueDate, Completed = @Completed WHERE TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@TaskID", taskID);
                    command.Parameters.AddWithValue("@TaskName", taskName);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@DueDate", (object)dueDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Completed", completed);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        static void DeleteTask(string connectionString, int taskID)
        {
            string deleteQuery = "DELETE FROM Tasks WHERE TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@TaskID", taskID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Create Important
        static void CreateImportant(string connectionString, int userID, string importantName, string description)
        {
            string insertQuery = "INSERT INTO Important (UserID, ImportantName, Description) VALUES (@UserID, @ImportantName, @Description)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ImportantName", importantName);
                    command.Parameters.AddWithValue("@Description", description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Read Important
        static void ReadImportant(string connectionString, int userID)
        {
            string selectQuery = "SELECT ImportantID, ImportantName, Description FROM Important WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"ImportantID: {reader["ImportantID"]}, ImportantName: {reader["ImportantName"]}, Description: {reader["Description"]}");
                    }
                }
            }
        }

        // Update Important
        static void UpdateImportant(string connectionString, int importantID, string importantName, string description)
        {
            string updateQuery = "UPDATE Important SET ImportantName = @ImportantName, Description = @Description WHERE ImportantID = @ImportantID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@ImportantID", importantID);
                    command.Parameters.AddWithValue("@ImportantName", importantName);
                    command.Parameters.AddWithValue("@Description", description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete Important
        static void DeleteImportant(string connectionString, int importantID)
        {
            string deleteQuery = "DELETE FROM Important WHERE ImportantID = @ImportantID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ImportantID", importantID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Create Planned
        static void CreatePlanned(string connectionString, int userID, string plannedName, string description, DateTime? dueDate)
        {
            string insertQuery = "INSERT INTO Planned (UserID, PlannedName, Description, DueDate) VALUES (@UserID, @PlannedName, @Description, @DueDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@PlannedName", plannedName);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@DueDate", (object)dueDate ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Read Planned
        static void ReadPlanned(string connectionString, int userID)
        {
            string selectQuery = "SELECT PlannedID, PlannedName, Description, DueDate FROM Planned WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"PlannedID: {reader["PlannedID"]}, PlannedName: {reader["PlannedName"]}, Description: {reader["Description"]}, DueDate: {reader["DueDate"]}");
                    }
                }
            }
        }

        // Update Planned
        static void UpdatePlanned(string connectionString, int plannedID, string plannedName, string description, DateTime? dueDate)
        {
            string updateQuery = "UPDATE Planned SET PlannedName = @PlannedName, Description = @Description, DueDate = @DueDate WHERE PlannedID = @PlannedID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@PlannedID", plannedID);
                    command.Parameters.AddWithValue("@PlannedName", plannedName);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@DueDate", (object)dueDate ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete Planned
        static void DeletePlanned(string connectionString, int plannedID)
        {
            string deleteQuery = "DELETE FROM Planned WHERE PlannedID = @PlannedID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@PlannedID", plannedID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        static void MarkTaskAsCompleted(string connectionString, int userID, int taskID)
        {
            // Step 1: Retrieve the task details
            Task task = ReadTask(connectionString, userID, taskID);

            if (task != null)
            {
                // Step 2: Create a record in the Completed table
                CreateCompleted(connectionString, task.UserID, task.TaskName, task.Description, DateTime.Now);

                // Step 3: Delete the task from Tasks table
                DeleteTask(connectionString, taskID);

                Console.WriteLine("Task marked as completed and moved to Completed table.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        // Read Task
        static Task ReadTask(string connectionString, int userID, int taskID)
        {
            string selectQuery = "SELECT UserID, TaskName, Description FROM Tasks WHERE UserID = @UserID AND TaskID = @TaskID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@TaskID", taskID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Task task = new Task
                        {
                            UserID = (int)reader["UserID"],
                            TaskName = (string)reader["TaskName"],
                            Description = (string)reader["Description"]
                        };
                        return task;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // Create Completed
        static void CreateCompleted(string connectionString, int userID, string taskName, string description, DateTime completionDate)
        {
            string insertQuery = "INSERT INTO Completed (UserID, CompletedName, Description, CompletionDate) VALUES (@UserID, @CompletedName, @Description, @CompletionDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CompletedName", taskName);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@CompletionDate", completionDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Mark Important Item as Completed and Move to Completed Table
        static void MarkImportantAsCompleted(string connectionString, int userID, int importantID)
        {
            // Step 1: Retrieve the important item details
            Important importantItem = ReadImportant(connectionString, userID, importantID);

            if (importantItem != null)
            {
                // Step 2: Create a record in the Completed table
                CreateCompleted(connectionString, importantItem.UserID, importantItem.ImportantName, importantItem.Description, DateTime.Now);

                // Step 3: Delete the important item from Important table
                DeleteImportant(connectionString, importantID);

                Console.WriteLine("Important item marked as completed and moved to Completed table.");
            }
            else
            {
                Console.WriteLine("Important item not found.");
            }
        }

        // Read Important Item
        static Important ReadImportant(string connectionString, int userID, int importantID)
        {
            string selectQuery = "SELECT UserID, ImportantName, Description FROM Important WHERE UserID = @UserID AND ImportantID = @ImportantID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ImportantID", importantID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Important importantItem = new Important
                        {
                            UserID = (int)reader["UserID"],
                            ImportantName = (string)reader["ImportantName"],
                            Description = (string)reader["Description"]
                        };
                        return importantItem;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // Mark Planned Task as Completed and Move to Completed Table
        static void MarkPlannedAsCompleted(string connectionString, int userID, int plannedID)
        {
            // Step 1: Retrieve the planned task details
            Planned plannedTask = ReadPlanned(connectionString, userID, plannedID);

            if (plannedTask != null)
            {
                // Step 2: Create a record in the Completed table
                CreateCompleted(connectionString, plannedTask.UserID, plannedTask.PlannedName, plannedTask.Description, DateTime.Now);

                // Step 3: Delete the planned task from Planned table
                DeletePlanned(connectionString, plannedID);

                Console.WriteLine("Planned task marked as completed and moved to Completed table.");
            }
            else
            {
                Console.WriteLine("Planned task not found.");
            }
        }

        // Read Planned Task
        static Planned ReadPlanned(string connectionString, int userID, int plannedID)
        {
            string selectQuery = "SELECT UserID, PlannedName, Description FROM Planned WHERE UserID = @UserID AND PlannedID = @PlannedID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@PlannedID", plannedID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Planned plannedTask = new Planned
                        {
                            UserID = (int)reader["UserID"],
                            PlannedName = (string)reader["PlannedName"],
                            Description = (string)reader["Description"]
                        };
                        return plannedTask;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        static void ReadAllTasks(string connectionString, int userID)
        {
            Console.WriteLine("All To-Do Lists:");

            // Read tasks
            Console.WriteLine("\nTasks:");
            ReadTasks(connectionString, userID);

            // Read important items
            Console.WriteLine("\nImportant Items:");
            ReadImportant(connectionString, userID);

            // Read planned tasks
            Console.WriteLine("\nPlanned Tasks:");
            ReadPlanned(connectionString, userID);
        }

    }
}


class Task
{
    public int UserID { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
}


class Important
{
    public int UserID { get; set; }
    public string ImportantName { get; set; }
    public string Description { get; set; }
}

class Planned
{
    public int UserID { get; set; }
    public string PlannedName { get; set; }
    public string Description { get; set; }
}