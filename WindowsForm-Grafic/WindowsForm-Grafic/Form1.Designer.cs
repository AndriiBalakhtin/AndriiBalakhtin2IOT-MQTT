namespace WindowsForm_Grafic
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
            cartesianChart = new LiveCharts.WinForms.CartesianChart();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // cartesianChart
            // 
            cartesianChart.Location = new Point(12, 118);
            cartesianChart.Name = "cartesianChart";
            cartesianChart.Size = new Size(937, 385);
            cartesianChart.TabIndex = 0;
            cartesianChart.Text = "cartesianChart";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 515);
            Controls.Add(cartesianChart);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer1;
    }
}
