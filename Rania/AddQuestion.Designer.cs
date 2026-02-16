namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    using System.Drawing;
    using System.Windows.Forms;

    public partial class AddQuestion : Form
    {
        private Button btnTF, btnMCQ, btnComplete;
        private Panel panelEdit;
        private DataGridView dataGridView;

        public void InitializeComponent()
        {
            string imagePath = Path.Combine(Application.StartupPath, "Resources", "qu.png");
           // PictureBox picture = new PictureBox
            //{
            //    Image = Image.FromFile(imagePath),
              //  SizeMode = PictureBoxSizeMode.Zoom,
              //  Size = new Size(250, 250),
               // Location = new Point(this.ClientSize.Width - 270, 170),
               // Anchor = AnchorStyles.Top | AnchorStyles.Right
           // };
            //this.Controls.Add(picture);

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1600, 900);
            this.Text = "All Questions";
            this.BackColor = Color.FromArgb(255, 248, 248);
            this.StartPosition = FormStartPosition.CenterScreen;

            panelEdit = new Panel()
            {
                Size = new Size(500, 500),
                Location = new Point(600, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                AutoScroll = true
            };
            this.Controls.Add(panelEdit);

            Label title = new Label()
            {
                Text = "Add a Question",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(title);

            Label chooseType = new Label()
            {
                Text = "Choose Type",
                Font = new Font("Segoe UI", 14F, FontStyle.Regular),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(30, 70),
                AutoSize = true
            };
            this.Controls.Add(chooseType);

            Font btnFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Size btnSize = new Size(180, 40);
            Color btnColor = ColorTranslator.FromHtml("#9FB3DF");

            btnTF = CreateTypeButton("✅ True / False", btnFont, btnSize, new Point(30, 110), btnColor);
            btnTF.Click += (s, e) => btnTF_Click(s, e);
            this.Controls.Add(btnTF);

            btnMCQ = CreateTypeButton("🅰️ Multiple Choice", btnFont, btnSize, new Point(230, 110), btnColor);
            btnMCQ.Click += (s, e) => btnOptions_Click(s, e);
            this.Controls.Add(btnMCQ);

            btnComplete = CreateTypeButton("✏️ Complete Sentence", btnFont, btnSize, new Point(430, 110), btnColor);
            btnComplete.Click += (s, e) => btnComplete_Click(s, e);
            this.Controls.Add(btnComplete);

            Label allQuestions = new Label()
            {
                Text = "All Questions",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(30, 170),
                AutoSize = true,
                ForeColor = Color.FromArgb(60, 60, 60)
            };
            this.Controls.Add(allQuestions);

            dataGridView = new DataGridView()
            {
                Location = new Point(30, 210),
                Width = 1200,
                Height = this.ClientSize.Height - 250,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom,
                BorderStyle = BorderStyle.FixedSingle,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.Both,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    WrapMode = DataGridViewTriState.True,
                    Padding = new Padding(10),
                    SelectionBackColor = Color.White,
                    SelectionForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.TopLeft
                }
            };

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = ColorTranslator.FromHtml("#9FB3DF"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                WrapMode = DataGridViewTriState.True,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dataGridView.Columns.Add("#", "#");
            dataGridView.Columns.Add("Question", "Question");
            dataGridView.Columns.Add("Type", "Type");
            dataGridView.Columns.Add("Options", "Options");
            dataGridView.Columns.Add("Correct", "Correct");
            dataGridView.Columns.Add("Level", "Level");
            dataGridView.Columns.Add("Category", "Category");
            dataGridView.Columns.Add("Hint", "Hint");

            DataGridViewButtonColumn actionCol = new DataGridViewButtonColumn();
            actionCol.Name = "Action";
            actionCol.HeaderText = "⋮";
            actionCol.Text = "Manage Question";
            actionCol.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(actionCol);

            dataGridView.CellContentClick += dataGridView_CellContentClick;
            this.Controls.Add(dataGridView);
        }

        public Button CreateTypeButton(string text, Font font, Size size, Point location, Color backColor)
        {
            return new Button()
            {
                Text = text,
                Font = font,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                Size = size,
                Location = location,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        public void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView.Columns[e.ColumnIndex].Name == "Action")
            {
                string number = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                string question = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                string type = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string options = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                string correct = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                string level = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                string category = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                string hint = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();

                this.BeginInvoke(new MethodInvoker(() =>
                {
                    ContextMenuStrip menu = new ContextMenuStrip();
                    menu.Items.Add("Edit", null, (s, ea) => ShowEditPanel(number, question, type, options, correct, level, category, hint));
                    menu.Items.Add("Remove", null, (s, ea) => DeleteQuestion(number));
                    var cellRect = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    menu.Show(dataGridView, new Point(cellRect.X + 10, cellRect.Y + 10));
                }));
            }
        }
    }
}
