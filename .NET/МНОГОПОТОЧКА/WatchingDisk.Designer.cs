namespace lab2
{
    partial class WatchingDisk
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
            this.name = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.disk_name = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.disk_picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.disk_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.Location = new System.Drawing.Point(39, 82);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(60, 24);
            this.name.TabIndex = 0;
            this.name.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(39, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "смотрит";
            // 
            // disk_name
            // 
            this.disk_name.AutoSize = true;
            this.disk_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.disk_name.Location = new System.Drawing.Point(39, 156);
            this.disk_name.Name = "disk_name";
            this.disk_name.Size = new System.Drawing.Size(60, 24);
            this.disk_name.TabIndex = 2;
            this.disk_name.Text = "label3";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(53, 254);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(673, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // disk_picture
            // 
            this.disk_picture.Location = new System.Drawing.Point(354, 296);
            this.disk_picture.Name = "disk_picture";
            this.disk_picture.Size = new System.Drawing.Size(70, 70);
            this.disk_picture.TabIndex = 4;
            this.disk_picture.TabStop = false;
            // 
            // WatchingDisk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.disk_picture);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.disk_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
            this.Name = "WatchingDisk";
            this.Text = "WatchingDisk";
            ((System.ComponentModel.ISupportInitialize)(this.disk_picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label disk_name;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox disk_picture;
    }
}