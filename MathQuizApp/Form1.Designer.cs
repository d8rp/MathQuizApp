namespace MathQuizApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblQuestion = new Label();
            tbAnswer = new TextBox();
            button1 = new Button();
            timer = new System.Windows.Forms.Timer(components);
            lblTimer = new Label();
            lblScore = new Label();
            SuspendLayout();
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Location = new Point(377, 172);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(38, 15);
            lblQuestion.TabIndex = 0;
            lblQuestion.Text = "label1";
            // 
            // tbAnswer
            // 
            tbAnswer.Location = new Point(346, 205);
            tbAnswer.Name = "tbAnswer";
            tbAnswer.Size = new Size(100, 23);
            tbAnswer.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(358, 234);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Submit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(750, 9);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(38, 15);
            lblTimer.TabIndex = 3;
            lblTimer.Text = "label1";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 9F);
            lblScore.Location = new Point(377, 133);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(38, 15);
            lblScore.TabIndex = 4;
            lblScore.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblScore);
            Controls.Add(lblTimer);
            Controls.Add(button1);
            Controls.Add(tbAnswer);
            Controls.Add(lblQuestion);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblQuestion;
        private TextBox tbAnswer;
        private Button button1;
        private System.Windows.Forms.Timer timer;
        private Label lblTimer;
        private Label lblScore;
    }
}
