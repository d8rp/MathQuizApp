using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MathQuizApp
{
    public partial class Form1 : Form
    {
        private Panel overlayPanel; // The overlay panel
        private Label lblIncorrectCdTimer;   // The label to display timer or message
        private Label lblCorrectedAnswer;

        public Form1()
        {
            InitializeComponent();
        }
  
        Random random = new Random();
        private int questionNum = 0; // Equilvalent to score
        private int answer = 0;
        private int time = 0;
        private int num1;
        private int num2;
        private Operation operation;
        private int maxNum = 10;
        private int minNum = 0;
        enum Operation
        {
            Add,
            Subtract,
            Multiply,
            Divide,
        }
        private Dictionary<Operation, Char> OperationToCharMap = new Dictionary<Operation, Char>(4);

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupOperations();
            SetupAnswerTextbox();
            SetupQuestionLabel();
            SetupTimer();
            SetupOverlayPanel();
        }

        private void SetupOverlayPanel()
        {
            overlayPanel = new Panel
            {
                Dock = DockStyle.Fill,  // Make it cover the whole form
                BackColor = Color.FromArgb(128, 255, 0, 0), // Red with transparency
                Visible = false, // Initially hidden
                Location = new Point(0, 0), // Position it at the top left corner
            };

            this.Controls.Add(overlayPanel);
            overlayPanel.BringToFront();

            // Initialize and configure the timer label
            lblIncorrectCdTimer = new Label
            {
                AutoSize = true,  // Let the label size adjust to its content
                Font = new Font("Arial", 24, FontStyle.Bold), // Customize the font and size
                ForeColor = Color.White, // White text color for contrast
                Text = "0", // Placeholder text for the timer
                Location = new Point((overlayPanel.Width - 100) / 2, // Center horizontally
                                     (overlayPanel.Height - 50) / 2), // Center vertically
                TextAlign = ContentAlignment.MiddleCenter // Ensure text is centered in the label
            };

            // Add the label to the overlay panel
            overlayPanel.Controls.Add(lblIncorrectCdTimer);

            // Initialize and configure the timer label
            lblCorrectedAnswer = new Label
            {
                AutoSize = true,  // Let the label size adjust to its content
                Font = new Font("Arial", 24, FontStyle.Bold), // Customize the font and size
                ForeColor = Color.White, // White text color for contrast
                Text = "0", // Placeholder text for the timer
                Location = new Point((overlayPanel.Width + 200) / 2, // Center horizontally
                                     (overlayPanel.Height + 100) / 2), // Center vertically
                TextAlign = ContentAlignment.MiddleCenter // Ensure text is centered in the label
            };

            // Add the label to the overlay panel
            overlayPanel.Controls.Add(lblCorrectedAnswer);
        }

        // Method to show the overlay
        private void ShowIncorrectOverlay()
        {
            overlayPanel.Visible = true;
            // Optionally, disable user controls while overlay is visible
            DisableControls(true);
        }

        // Method to hide the overlay
        private void HideIncorrectOverlay()
        {
            overlayPanel.Visible = false;
            // Optionally, re-enable user controls when overlay is hidden
            DisableControls(false);
        }

        // Method to enable/disable controls on the form (to block user interaction)
        private void DisableControls(bool disable)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != overlayPanel) // Skip the overlay itself
                {
                    ctrl.Enabled = !disable; // Enable or disable the control
                }
            }
        }

        private void SetupTimer()
        {
            time = 0;
            lblTimer.Text = time.ToString();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            time += 1;
            lblTimer.Text = time.ToString();
        }

        private void ResetTimer()
        {
            time = 0;
            timer.Stop();
            timer.Start();
        }

        private void StopTimer()
        {
            timer.Stop();
        }

        private void SetupOperations()
        {
            OperationToCharMap.Add(Operation.Add, '+');
            OperationToCharMap.Add(Operation.Subtract, '-');
            OperationToCharMap.Add(Operation.Multiply, 'x');
            OperationToCharMap.Add(Operation.Divide, '/');
        }

        public void Wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void SetupQuestionLabel()
        {
            DisplayNextQuestion();
        }

        private void SetupAnswerTextbox()
        {
            tbAnswer.KeyDown += TbAnswer_KeyDown;
        }

        private void SubmitAnswer()
        {
            // Check if answer is correct
            if (!tbAnswer.Text.Equals(answer.ToString())) // Wrong
            {
                OnIncorrectAnswer();
            }
            tbAnswer.Text = ""; // Clear the answer text input

            DisplayNextQuestion();
        }

        private void OnIncorrectAnswer()
        {
            StopTimer();
            ShowIncorrectOverlay();

            lblCorrectedAnswer.Text = answer.ToString();
            for (int i = 3; i > 0; i--)
            {
                lblIncorrectCdTimer.Text = i.ToString();
                Wait(1000);
            }

            lblQuestion.Text = answer.ToString();
            lblQuestion.BackColor = Color.Red;
            questionNum = 0;
            maxNum = 10;
            minNum = 0;

            lblQuestion.BackColor = Color.White;
            HideIncorrectOverlay();
        }

        private void DisplayNextQuestion()
        {
            ResetTimer();
            operation = (Operation)random.Next(0, 4);

            if (operation.Equals(Operation.Divide))
            {
                var (newNum1, newNum2) = GenerateDivisiblePair(minNum, maxNum);
                num1 = newNum1;
                num2 = newNum2;
            }
            else
            {
                // Next includes min but not max
                num1 = random.Next(minNum, maxNum+1);
                num2 = random.Next(minNum, maxNum+1);
            }

            string newQuestion = String.Format("{0} {1} {2}", num1, OperationToCharMap[operation], num2);
            int newAnswer = Calculate(operation, num1, num2);

            lblQuestion.Text = newQuestion;
            SetScore(questionNum);
            answer = newAnswer;
            questionNum += 1;

            maxNum += 1;
            minNum -= 1;
        }

        private void SetScore(int questionNum)
        {
            lblScore.Text = questionNum.ToString();

            int fontSize = 9 + (int)(questionNum / 5);
            Font newFont = new Font("Segoe UI", (float)fontSize);
            lblScore.Font = newFont;
        }

        private (int, int) GenerateDivisiblePair(int val1, int val2)
        {
            // Ensure that minVal is not zero (if the range allows for zero values)
            if (val1 == 0) val1 = 1;  // Adjust if needed based on your requirements

            // Flip one of the numbers half the time
            if (random.Next(0, 2) == 0) val1 *= -1;

            return (val1*val2, val1);
        }

        private int Calculate(Operation operation, int num1, int num2)
        {
            if (operation == Operation.Add)
            {
                return num1 + num2;
            }
            else if (operation == Operation.Subtract)
            {
                return num1 - num2;
            }
            else if (operation == Operation.Multiply)
            {
                return num1 * num2;
            }
            else
            {
                return num1 / num2;
            }
        }

        private void TbAnswer_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitAnswer();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubmitAnswer();
        }
    }
}
