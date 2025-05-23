namespace SimulacionPIndividual
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblConclusion = new System.Windows.Forms.Label();
            this.nudSimulaciones = new System.Windows.Forms.NumericUpDown();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.nudClientes = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudDespacho = new System.Windows.Forms.NumericUpDown();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.chartWaitingTimes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BtnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSimulaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDespacho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartWaitingTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConclusion
            // 
            this.lblConclusion.AutoSize = true;
            this.lblConclusion.Location = new System.Drawing.Point(12, 188);
            this.lblConclusion.MaximumSize = new System.Drawing.Size(450, 300);
            this.lblConclusion.Name = "lblConclusion";
            this.lblConclusion.Size = new System.Drawing.Size(35, 13);
            this.lblConclusion.TabIndex = 1;
            this.lblConclusion.Text = "label1";
            // 
            // nudSimulaciones
            // 
            this.nudSimulaciones.Location = new System.Drawing.Point(128, 37);
            this.nudSimulaciones.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudSimulaciones.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSimulaciones.Name = "nudSimulaciones";
            this.nudSimulaciones.Size = new System.Drawing.Size(120, 20);
            this.nudSimulaciones.TabIndex = 2;
            this.nudSimulaciones.Tag = "1";
            this.nudSimulaciones.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.AllowDrop = true;
            this.btnEjecutar.Location = new System.Drawing.Point(295, 37);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(129, 23);
            this.btnEjecutar.TabIndex = 3;
            this.btnEjecutar.Text = "Iniciar simulacion";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.Ejecutar_Click);
            // 
            // nudClientes
            // 
            this.nudClientes.Location = new System.Drawing.Point(128, 79);
            this.nudClientes.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudClientes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudClientes.Name = "nudClientes";
            this.nudClientes.Size = new System.Drawing.Size(120, 20);
            this.nudClientes.TabIndex = 4;
            this.nudClientes.Tag = "1";
            this.nudClientes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 30);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cantidad de simulaciones";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(30, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "Clientes cada 2 minutos";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tiempo de despacho por cliente(Minutos)";
            // 
            // nudDespacho
            // 
            this.nudDespacho.Location = new System.Drawing.Point(128, 129);
            this.nudDespacho.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudDespacho.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDespacho.Name = "nudDespacho";
            this.nudDespacho.Size = new System.Drawing.Size(120, 20);
            this.nudDespacho.TabIndex = 9;
            this.nudDespacho.Tag = "1";
            this.nudDespacho.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Location = new System.Drawing.Point(16, 372);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.Size = new System.Drawing.Size(799, 242);
            this.dataGridViewResults.TabIndex = 10;
            // 
            // chartWaitingTimes
            // 
            chartArea1.Name = "ChartArea1";
            this.chartWaitingTimes.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartWaitingTimes.Legends.Add(legend1);
            this.chartWaitingTimes.Location = new System.Drawing.Point(439, 48);
            this.chartWaitingTimes.Name = "chartWaitingTimes";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartWaitingTimes.Series.Add(series1);
            this.chartWaitingTimes.Size = new System.Drawing.Size(376, 300);
            this.chartWaitingTimes.TabIndex = 11;
            this.chartWaitingTimes.Text = "chart1";
            // 
            // BtnClear
            // 
            this.BtnClear.AllowDrop = true;
            this.BtnClear.Location = new System.Drawing.Point(274, 129);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(129, 23);
            this.BtnClear.TabIndex = 12;
            this.BtnClear.Text = "Limpiar datos";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SimulacionPIndividual.Properties.Resources._951579b67002ac243753cd5037caf608;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1204, 635);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.chartWaitingTimes);
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.nudDespacho);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudClientes);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.nudSimulaciones);
            this.Controls.Add(this.lblConclusion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Simulador de cajas";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSimulaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDespacho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartWaitingTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblConclusion;
        private System.Windows.Forms.NumericUpDown nudSimulaciones;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.NumericUpDown nudClientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudDespacho;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWaitingTimes;
        private System.Windows.Forms.Button BtnClear;
    }
}

