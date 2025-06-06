namespace Lab_1
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LogsTextBox = new TextBox();
            BufferTextBox = new TextBox();
            StartButton = new Button();
            CloseButton = new Button();
            BufferLabel = new Label();
            SuspendLayout();
            // 
            // LogsTextBox
            // 
            LogsTextBox.Location = new Point(174, 36);
            LogsTextBox.Multiline = true;
            LogsTextBox.Name = "LogsTextBox";
            LogsTextBox.ReadOnly = true;
            LogsTextBox.ScrollBars = ScrollBars.Vertical;
            LogsTextBox.Size = new Size(325, 402);
            LogsTextBox.TabIndex = 0;
            // 
            // BufferTextBox
            // 
            BufferTextBox.Location = new Point(516, 36);
            BufferTextBox.Multiline = true;
            BufferTextBox.Name = "BufferTextBox";
            BufferTextBox.ReadOnly = true;
            BufferTextBox.ScrollBars = ScrollBars.Vertical;
            BufferTextBox.Size = new Size(272, 402);
            BufferTextBox.TabIndex = 1;
            // 
            // StartButton
            // 
            StartButton.AutoSize = true;
            StartButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            StartButton.Location = new Point(12, 128);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(156, 77);
            StartButton.TabIndex = 2;
            StartButton.Text = "Запуск";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.AutoSize = true;
            CloseButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            CloseButton.Location = new Point(12, 221);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(156, 76);
            CloseButton.TabIndex = 4;
            CloseButton.Text = "Закрыть";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // BufferLabel
            // 
            BufferLabel.AutoSize = true;
            BufferLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            BufferLabel.Location = new Point(626, 9);
            BufferLabel.Name = "BufferLabel";
            BufferLabel.Size = new Size(55, 21);
            BufferLabel.TabIndex = 6;
            BufferLabel.Text = "Буфер";
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BufferLabel);
            Controls.Add(CloseButton);
            Controls.Add(StartButton);
            Controls.Add(BufferTextBox);
            Controls.Add(LogsTextBox);
            Name = "Form";
            Text = "Лабораторная";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox LogsTextBox;
        private TextBox BufferTextBox;
        private Button StartButton;
        private Button CloseButton;
        private Label BufferLabel;
    }
}