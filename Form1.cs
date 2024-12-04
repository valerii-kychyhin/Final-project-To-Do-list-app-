using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace Final_project
{
    public partial class MainForm : Form
    {
        private List<string> tasks = new List<string>();
        private List<bool> completionStatus = new List<bool>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Add Task
        {
            string newTask = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(newTask))
            {
                tasks.Add(newTask);
                completionStatus.Add(false); // Default status is incomplete
                UpdateListBox();
                label1.Text = "Task added successfully!";
                textBox1.Clear();
            }
            else
            {
                label1.Text = "Task cannot be empty.";
            }
        }

        private void button2_Click(object sender, EventArgs e) // Edit Task
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < tasks.Count)
            {
                string editedTask = textBox1.Text.Trim();
                if (!string.IsNullOrEmpty(editedTask))
                {
                    tasks[selectedIndex] = editedTask;
                    UpdateListBox();
                    label1.Text = "Task updated successfully!";
                }
                else
                {
                    label1.Text = "Edited task cannot be empty.";
                }
            }
            else
            {
                label1.Text = "Please select a task to edit.";
            }
        }

        private void button3_Click(object sender, EventArgs e) // Delete Task
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < tasks.Count)
            {
                tasks.RemoveAt(selectedIndex);
                completionStatus.RemoveAt(selectedIndex);
                UpdateListBox();
                label1.Text = "Task deleted successfully!";
            }
            else
            {
                label1.Text = "Please select a task to delete.";
            }
        }

        private void button4_Click(object sender, EventArgs e) // Mark as Complete/Incomplete
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < completionStatus.Count)
            {
                completionStatus[selectedIndex] = !completionStatus[selectedIndex]; // Toggle status
                UpdateListBox();
                label1.Text = "Task status updated.";
            }
            else
            {
                label1.Text = "Please select a task to mark.";
            }
        }

        private void button5_Click(object sender, EventArgs e) // Save Tasks
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                Title = "Save Tasks"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        writer.WriteLine($"{tasks[i]}|{completionStatus[i]}");
                    }
                }
                label1.Text = "Tasks saved successfully!";
            }
        }

        private void button6_Click(object sender, EventArgs e) // Load Tasks
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                Title = "Load Tasks"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tasks.Clear();
                completionStatus.Clear();

                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            tasks.Add(parts[0]);
                            completionStatus.Add(bool.Parse(parts[1]));
                        }
                    }
                }
                UpdateListBox();
                label1.Text = "Tasks loaded successfully!";
            }
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < tasks.Count; i++)
            {
                string status = completionStatus[i] ? "[Completed]" : "[Incomplete]";
                listBox1.Items.Add($"{status} {tasks[i]}");
            }
        }
    }
}

