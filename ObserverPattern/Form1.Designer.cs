namespace ObserverPattern
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
            lblTemp = new Label();
            btnChangeTemp = new Button();
            SuspendLayout();
            // 
            // lblTemp
            // 
            lblTemp.AutoSize = true;
            lblTemp.Location = new Point(58, 44);
            lblTemp.Name = "lblTemp";
            lblTemp.Size = new Size(39, 15);
            lblTemp.TabIndex = 0;
            lblTemp.Text = "label1";
            // 
            // btnChangeTemp
            // 
            btnChangeTemp.Location = new Point(159, 40);
            btnChangeTemp.Name = "btnChangeTemp";
            btnChangeTemp.Size = new Size(75, 23);
            btnChangeTemp.TabIndex = 1;
            btnChangeTemp.Text = "Change Temperature";
            btnChangeTemp.UseVisualStyleBackColor = true;
            btnChangeTemp.Click += btnChangeTemp_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnChangeTemp);
            Controls.Add(lblTemp);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTemp;
        private Button btnChangeTemp;
    }
}
