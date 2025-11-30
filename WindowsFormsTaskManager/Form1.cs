using System;
using System.Windows.Forms;

namespace WindowsFormsTaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            btnAddTask.Click += btnAddTask_Click;
            btnRemoveSelected.Click += btnRemoveSelected_Click;
            btnClearAll.Click += btnClearAll_Click;
            txtTask.TextChanged += txtTask_TextChanged;
            lstTasks.SelectedIndexChanged += lstTasks_SelectedIndexChanged;

            btnAddTask.Enabled = false;
            lblStatus.Text = "Ready.";
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTask.Text))
            {
                lstTasks.Items.Add(txtTask.Text.Trim());
                txtTask.Clear();
                lblStatus.Text = $"Task added at {DateTime.Now:T}";
            }
            else
            {
                MessageBox.Show("Please enter a task first.", "Input Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblStatus.Text = "No task entered.";
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex == -1)
            {
                lblStatus.Text = "No task selected to remove.";
                return;
            }

            string selectedTask = lstTasks.SelectedItem?.ToString() ?? string.Empty;

            if (chkConfirmDelete.Checked)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete '{selectedTask}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    lblStatus.Text = "Delete cancelled.";
                    return;
                }
            }
            int index = lstTasks.SelectedIndex;
            lstTasks.Items.RemoveAt(index);
            lblStatus.Text = $"Task '{selectedTask}' removed.";
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (lstTasks.Items.Count > 0)
            {
                lstTasks.Items.Clear();
                lblStatus.Text = "All tasks cleared.";
            }
            else
            {
                lblStatus.Text = "Task list is already empty.";
            }
        }

        private void txtTask_TextChanged(object sender, EventArgs e)
        {
            btnAddTask.Enabled = !string.IsNullOrWhiteSpace(txtTask.Text);
        }

        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex != -1)
            {
                lblStatus.Text = $"Selected task: {lstTasks.SelectedItem}";
            }
        }
    }
}
