namespace Seminar8
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
            DRAW_SIDES = new Button();
            TRIANGULARE_PRIN_OTECTOMIE = new Button();
            THREE_COLORING = new Button();
            SuspendLayout();
            // 
            // DRAW_SIDES
            // 
            DRAW_SIDES.Location = new Point(27, 30);
            DRAW_SIDES.Name = "DRAW_SIDES";
            DRAW_SIDES.Size = new Size(157, 23);
            DRAW_SIDES.TabIndex = 0;
            DRAW_SIDES.Text = "DRAW SIDES";
            DRAW_SIDES.UseVisualStyleBackColor = true;
            DRAW_SIDES.Click += DRAW_SIDES_Click;
            // 
            // TRIANGULARE_PRIN_OTECTOMIE
            // 
            TRIANGULARE_PRIN_OTECTOMIE.Location = new Point(209, 30);
            TRIANGULARE_PRIN_OTECTOMIE.Name = "TRIANGULARE_PRIN_OTECTOMIE";
            TRIANGULARE_PRIN_OTECTOMIE.Size = new Size(198, 23);
            TRIANGULARE_PRIN_OTECTOMIE.TabIndex = 1;
            TRIANGULARE_PRIN_OTECTOMIE.Text = "TRIANGULARE PRIN OTECTOMIE";
            TRIANGULARE_PRIN_OTECTOMIE.UseVisualStyleBackColor = true;
            TRIANGULARE_PRIN_OTECTOMIE.Click += TRIANGULARE_PRIN_OTECTOMIE_Click;
            // 
            // THREE_COLORING
            // 
            THREE_COLORING.Location = new Point(442, 30);
            THREE_COLORING.Name = "THREE_COLORING";
            THREE_COLORING.Size = new Size(172, 23);
            THREE_COLORING.TabIndex = 2;
            THREE_COLORING.Text = "THREE-COLORING";
            THREE_COLORING.UseVisualStyleBackColor = true;
            THREE_COLORING.Click += THREE_COLORING_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(THREE_COLORING);
            Controls.Add(TRIANGULARE_PRIN_OTECTOMIE);
            Controls.Add(DRAW_SIDES);
            Name = "Form1";
            Text = "Form1";
            Click += Form1_Click;
            Paint += Form1_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Button DRAW_SIDES;
        private Button TRIANGULARE_PRIN_OTECTOMIE;
        private Button THREE_COLORING;
    }
}
