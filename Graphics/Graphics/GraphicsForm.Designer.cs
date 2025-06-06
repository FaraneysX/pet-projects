namespace GraphicsPractice
{
    partial class GraphicsForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicsForm));
            this.HumanFullnessBox = new System.Windows.Forms.GroupBox();
            this.button_color_pants = new System.Windows.Forms.Button();
            this.LessText = new System.Windows.Forms.Label();
            this.MoreText = new System.Windows.Forms.Label();
            this.HumanFullnessTrackBar = new System.Windows.Forms.TrackBar();
            this.HumanFullnessText = new System.Windows.Forms.Label();
            this.pictureBox_human = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.HumanFullnessBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HumanFullnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_human)).BeginInit();
            this.SuspendLayout();
            // 
            // HumanFullnessBox
            // 
            this.HumanFullnessBox.Controls.Add(this.button_color_pants);
            this.HumanFullnessBox.Controls.Add(this.LessText);
            this.HumanFullnessBox.Controls.Add(this.MoreText);
            this.HumanFullnessBox.Controls.Add(this.HumanFullnessTrackBar);
            this.HumanFullnessBox.Controls.Add(this.HumanFullnessText);
            resources.ApplyResources(this.HumanFullnessBox, "HumanFullnessBox");
            this.HumanFullnessBox.Name = "HumanFullnessBox";
            this.HumanFullnessBox.TabStop = false;
            // 
            // button_color_pants
            // 
            this.button_color_pants.BackColor = System.Drawing.Color.CornflowerBlue;
            resources.ApplyResources(this.button_color_pants, "button_color_pants");
            this.button_color_pants.Name = "button_color_pants";
            this.button_color_pants.UseVisualStyleBackColor = false;
            this.button_color_pants.Click += new System.EventHandler(this.button_color_pants_Click);
            // 
            // LessText
            // 
            resources.ApplyResources(this.LessText, "LessText");
            this.LessText.Name = "LessText";
            // 
            // MoreText
            // 
            resources.ApplyResources(this.MoreText, "MoreText");
            this.MoreText.Name = "MoreText";
            // 
            // HumanFullnessTrackBar
            // 
            this.HumanFullnessTrackBar.LargeChange = 4;
            resources.ApplyResources(this.HumanFullnessTrackBar, "HumanFullnessTrackBar");
            this.HumanFullnessTrackBar.Maximum = 5;
            this.HumanFullnessTrackBar.Minimum = 1;
            this.HumanFullnessTrackBar.Name = "HumanFullnessTrackBar";
            this.HumanFullnessTrackBar.TickFrequency = 5;
            this.HumanFullnessTrackBar.Value = 3;
            this.HumanFullnessTrackBar.Scroll += new System.EventHandler(this.HumanFullnessTrackBar_Scroll);
            // 
            // HumanFullnessText
            // 
            resources.ApplyResources(this.HumanFullnessText, "HumanFullnessText");
            this.HumanFullnessText.Name = "HumanFullnessText";
            // 
            // pictureBox_human
            // 
            this.pictureBox_human.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.pictureBox_human, "pictureBox_human");
            this.pictureBox_human.Name = "pictureBox_human";
            this.pictureBox_human.TabStop = false;
            this.pictureBox_human.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_human_Paint);
            this.pictureBox_human.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_human_MouseClick);
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.CornflowerBlue;
            // 
            // GraphicsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_human);
            this.Controls.Add(this.HumanFullnessBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GraphicsForm";
            this.Load += new System.EventHandler(this.GraphicsForm_Load);
            this.HumanFullnessBox.ResumeLayout(false);
            this.HumanFullnessBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HumanFullnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_human)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox HumanFullnessBox;
        private System.Windows.Forms.Label HumanFullnessText;
        private System.Windows.Forms.TrackBar HumanFullnessTrackBar;
        private System.Windows.Forms.Label LessText;
        private System.Windows.Forms.Label MoreText;
        private System.Windows.Forms.Button button_color_pants;
        private System.Windows.Forms.PictureBox pictureBox_human;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}