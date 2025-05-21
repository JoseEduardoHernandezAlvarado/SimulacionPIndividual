using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimulacionPIndividual
{
    public partial class Form1 : Form
    {
        // Parámetros de la simulación
        private double LAMBDA;
        private double MU;
        private int SIMULATION_TIME = 120;
        private int NUM_RUNS;
        private List<double> averageWaitingTimesMM1 = new List<double>();
        private List<double> averageServiceTimesMM1 = new List<double>();
        private List<double> averageQueueLengthsMM1 = new List<double>();
        private List<double> averageWaitingTimesMM2 = new List<double>();
        private List<double> averageServiceTimesMM2 = new List<double>();
        private List<double> averageQueueLengthsMM2 = new List<double>();

        // Parámetros del generador de números pseudoaleatorios
        private int a = 23; // Constante multiplicativa
        private int c = 101; // Constante aditiva
        private int m = 1009; // Módulo

        public Form1()
        {
            InitializeComponent();

            // Configurar DataGridView
            dataGridViewResults.Columns.Clear();
            dataGridViewResults.Columns.Add("Corrida", "Corrida");
            dataGridViewResults.Columns.Add("TiempoEspera", "Tiempo Espera (min)");
            dataGridViewResults.Columns.Add("TiempoDespacho", "Tiempo Despacho (min)");
            dataGridViewResults.Columns.Add("LongitudCola", "Longitud Cola");
            dataGridViewResults.Columns.Add("Modelo", "Modelo");
            dataGridViewResults.Columns["TiempoEspera"].DefaultCellStyle.Format = "N2";
            dataGridViewResults.Columns["TiempoDespacho"].DefaultCellStyle.Format = "N2";
            dataGridViewResults.Columns["LongitudCola"].DefaultCellStyle.Format = "N2";

            // Configurar Chart
            chartWaitingTimes.Series.Clear();
            chartWaitingTimes.ChartAreas.Clear();
            chartWaitingTimes.ChartAreas.Add(new ChartArea("ChartArea1") { Tag = "Tiempos de Espera" });
            chartWaitingTimes.Series.Add("Tiempo Espera MM1");
            chartWaitingTimes.Series.Add("Tiempo Espera MM2");
            chartWaitingTimes.Series["Tiempo Espera MM1"].ChartType = SeriesChartType.Bar;
            chartWaitingTimes.Series["Tiempo Espera MM2"].ChartType = SeriesChartType.Bar;
            chartWaitingTimes.Series["Tiempo Espera MM1"].Color = System.Drawing.Color.Blue;
            chartWaitingTimes.Series["Tiempo Espera MM2"].Color = System.Drawing.Color.Green;
            chartWaitingTimes.ChartAreas["ChartArea1"].AxisX.Title = "Corrida";
            chartWaitingTimes.ChartAreas["ChartArea1"].AxisY.Title = "Tiempo de Espera (min)";

            // Ocultar lblConclusion inicialmente
            lblConclusion.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Ejecutar_Click(object sender, EventArgs e)
        {
            // Leer parámetros desde los controles
            NUM_RUNS = (int)nudSimulaciones.Value;
            LAMBDA = (double)nudClientes.Value / 2.0; // Clientes cada 2 minutos -> tasa por minuto
            MU = 1.0 / (double)nudDespacho.Value; // Tasa de servicio por minuto

            // Validar entradas
            if (LAMBDA <= 0 || MU <= 0 || NUM_RUNS <= 0)
            {
                MessageBox.Show("Por favor, ingrese valores válidos: tasas positivas y corridas > 0.");
                return;
            }

            // Ejecutar simulación
            EjecutarSimulacion();
        }

        // MÓDULO 1: Generación de números pseudoaleatorios
        private List<double> GenerarNumerosPseudoaleatorios(int cantidad, long semilla)
        {
            List<double> numeros = new List<double>();
            int X = (int)(semilla % m);
            for (int i = 0; i < cantidad; i++)
            {
                X = (a * X + c) % m;
                double Ri = (double)X / m;
                numeros.Add(Ri);
            }
            return numeros;
        }

        // MÓDULO 2: Simulación de cola M/M/1
        private (double avgWaitingTime, double avgServiceTime, double avgQueueLength) SimularColaMM1(List<double> serviceTimes, List<double> numeros, int startIdx, int endIdx)
        {
            double tiempoSimulacion = 0;
            Queue<double> cola = new Queue<double>();
            List<double> tiemposEspera = new List<double>();
            double tiempoFinUltimoServicio = 0;
            int idx = startIdx;
            int clientesAtendidos = 0;
            double totalQueueLength = 0;
            int queueLengthSamples = 0;

            while (tiempoSimulacion < SIMULATION_TIME && idx < endIdx)
            {
                double tiempoProximaLlegada = -Math.Log(1 - numeros[idx++]) / LAMBDA;
                if (tiempoProximaLlegada + tiempoSimulacion <= tiempoFinUltimoServicio)
                {
                    cola.Enqueue(tiempoProximaLlegada + tiempoSimulacion);
                    tiempoSimulacion += tiempoProximaLlegada;
                    totalQueueLength += cola.Count;
                    queueLengthSamples++;
                }
                else
                {
                    if (cola.Count > 0)
                    {
                        double tiempoLlegada = cola.Dequeue();
                        double tiempoEspera = tiempoFinUltimoServicio - tiempoLlegada;
                        tiemposEspera.Add(tiempoEspera);
                        double tiempoServicio = -Math.Log(1 - numeros[idx++]) / MU;
                        serviceTimes.Add(tiempoServicio);
                        tiempoFinUltimoServicio += tiempoServicio;
                        tiempoSimulacion = tiempoFinUltimoServicio;
                        clientesAtendidos++;
                        totalQueueLength += cola.Count;
                        queueLengthSamples++;
                    }
                    else
                    {
                        tiempoSimulacion += tiempoProximaLlegada;
                        tiempoFinUltimoServicio = tiempoSimulacion;
                        double tiempoServicio = -Math.Log(1 - numeros[idx++]) / MU;
                        serviceTimes.Add(tiempoServicio);
                        tiempoFinUltimoServicio += tiempoServicio;
                        clientesAtendidos++;
                    }
                }
            }

            double avgWaitingTime = tiemposEspera.Any() ? tiemposEspera.Average() : 0;
            double avgServiceTime = serviceTimes.Any() ? serviceTimes.Average() : 0;
            double avgQueueLength = queueLengthSamples > 0 ? totalQueueLength / queueLengthSamples : 0;
            return (avgWaitingTime, avgServiceTime, avgQueueLength);
        }

        // MÓDULO 3: Simulación de cola M/M/2
        private (double avgWaitingTime, double avgServiceTime, double avgQueueLength) SimularColaMM2(List<double> serviceTimes, List<double> numeros, int startIdx, int endIdx)
        {
            double tiempoSimulacion = 0;
            Queue<double> cola = new Queue<double>();
            List<double> tiemposEspera = new List<double>();
            double[] tiemposFinServicio = { 0, 0 };
            int servidoresOcupados = 0;
            int idx = startIdx;
            int clientesAtendidos = 0;
            double totalQueueLength = 0;
            int queueLengthSamples = 0;

            while (tiempoSimulacion < SIMULATION_TIME && idx < endIdx)
            {
                double tiempoProximaLlegada = -Math.Log(1 - numeros[idx++]) / LAMBDA;
                double minTiempoFinServicio = servidoresOcupados > 0 ? tiemposFinServicio.Min() : double.MaxValue;

                if (tiempoProximaLlegada + tiempoSimulacion <= minTiempoFinServicio)
                {
                    // Procesar llegada
                    tiempoSimulacion += tiempoProximaLlegada;
                    cola.Enqueue(tiempoSimulacion);
                    totalQueueLength += cola.Count;
                    queueLengthSamples++;
                }
                else if (servidoresOcupados > 0)
                {
                    // Procesar fin de servicio
                    tiempoSimulacion = minTiempoFinServicio;
                    int servidor = Array.IndexOf(tiemposFinServicio, minTiempoFinServicio);
                    tiemposFinServicio[servidor] = 0;
                    servidoresOcupados--;
                }
                else
                {
                    // Avanzar al próximo evento de llegada
                    tiempoSimulacion += tiempoProximaLlegada;
                    cola.Enqueue(tiempoSimulacion);
                    totalQueueLength += cola.Count;
                    queueLengthSamples++;
                }

                // Asignar clientes a servidores disponibles
                while (cola.Count > 0 && servidoresOcupados < 2)
                {
                    double tiempoLlegada = cola.Dequeue();
                    double tiempoEspera = tiempoSimulacion - tiempoLlegada;
                    tiemposEspera.Add(tiempoEspera);
                    int servidor = tiemposFinServicio[0] <= tiemposFinServicio[1] ? 0 : 1;
                    double tiempoServicio = -Math.Log(1 - numeros[idx++]) / MU;
                    serviceTimes.Add(tiempoServicio);
                    tiemposFinServicio[servidor] = tiempoSimulacion + tiempoServicio;
                    servidoresOcupados++;
                    clientesAtendidos++;
                    totalQueueLength += cola.Count;
                    queueLengthSamples++;
                }
            }

            double avgWaitingTime = tiemposEspera.Any() ? tiemposEspera.Average() : 0;
            double avgServiceTime = serviceTimes.Any() ? serviceTimes.Average() : 0;
            double avgQueueLength = queueLengthSamples > 0 ? totalQueueLength / queueLengthSamples : 0;
            return (avgWaitingTime, avgServiceTime, avgQueueLength);
        }

        // MÓDULO 4: Ejecución de la simulación y evaluación
        private void EjecutarSimulacion()
        {
            dataGridViewResults.Rows.Clear();
            chartWaitingTimes.Series["Tiempo Espera MM1"].Points.Clear();
            chartWaitingTimes.Series["Tiempo Espera MM2"].Points.Clear();
            averageWaitingTimesMM1.Clear();
            averageServiceTimesMM1.Clear();
            averageQueueLengthsMM1.Clear();
            averageWaitingTimesMM2.Clear();
            averageServiceTimesMM2.Clear();
            averageQueueLengthsMM2.Clear();

            // Usar la hora del sistema como semilla
            long semilla = DateTime.Now.Ticks;

            int cantidadNumeros = (int)(SIMULATION_TIME * LAMBDA * 2); // Aproximación de eventos
            int extra = (int)(cantidadNumeros * 0.5); // 50% más
            List<double> numerosMM1 = GenerarNumerosPseudoaleatorios(cantidadNumeros + extra, semilla);
            List<double> numerosMM2 = GenerarNumerosPseudoaleatorios(cantidadNumeros + extra, semilla + 1);
            List<double> serviceTimesMM1 = new List<double>();
            List<double> serviceTimesMM2 = new List<double>();

            // Agregar encabezado para M/M/1
            dataGridViewResults.Rows.Add("-- 1 Caja--", "", "", "", "");
            for (int i = 0; i < NUM_RUNS; i++)
            {
                int startIdx = i * (cantidadNumeros / NUM_RUNS);
                int endIdx = (i + 1) * (cantidadNumeros / NUM_RUNS);
                var (avgWaitingTime, avgServiceTime, avgQueueLength) = SimularColaMM1(serviceTimesMM1, numerosMM1, startIdx, endIdx);
                averageWaitingTimesMM1.Add(avgWaitingTime);
                averageServiceTimesMM1.Add(avgServiceTime);
                averageQueueLengthsMM1.Add(avgQueueLength);
                dataGridViewResults.Rows.Add(i + 1, avgWaitingTime, avgServiceTime, avgQueueLength, "M/M/1");
                chartWaitingTimes.Series["Tiempo Espera MM1"].Points.AddXY(i + 1, avgWaitingTime);
            }

            // Agregar encabezado para M/M/2
            dataGridViewResults.Rows.Add("--2 Cajas--", "", "", "", "");
            for (int i = 0; i < NUM_RUNS; i++)
            {
                int startIdx = i * (cantidadNumeros / NUM_RUNS);
                int endIdx = (i + 1) * (cantidadNumeros / NUM_RUNS);
                var (avgWaitingTime, avgServiceTime, avgQueueLength) = SimularColaMM2(serviceTimesMM2, numerosMM2, startIdx, endIdx);
                averageWaitingTimesMM2.Add(avgWaitingTime);
                averageServiceTimesMM2.Add(avgServiceTime);
                averageQueueLengthsMM2.Add(avgQueueLength);
                dataGridViewResults.Rows.Add(i + 1, avgWaitingTime, avgServiceTime, avgQueueLength, "M/M/2");
                chartWaitingTimes.Series["Tiempo Espera MM2"].Points.AddXY(i + 1, avgWaitingTime);
            }

            // Evaluar y mostrar conclusión
            double promedioEsperaMM1 = averageWaitingTimesMM1.Average();
            double promedioDespachoMM1 = averageServiceTimesMM1.Average();
            double proporcionSuperan5MinMM1 = (averageWaitingTimesMM1.Count(t => t > 5) / (double)NUM_RUNS) * 100;
            double diferenciaHistoricaMM1 = Math.Abs(promedioEsperaMM1 - 4.5);

            double promedioEsperaMM2 = averageWaitingTimesMM2.Average();
            double promedioDespachoMM2 = averageServiceTimesMM2.Average();
            double proporcionSuperan5MinMM2 = (averageWaitingTimesMM2.Count(t => t > 5) / (double)NUM_RUNS) * 100;
            double diferenciaHistoricaMM2 = Math.Abs(promedioEsperaMM2 - 4.5);

            string conclusion = "• Resultados con 1 caja (M/M/1):\n";
            conclusion += $"  - Tiempo promedio de espera: {promedioEsperaMM1:F2} minutos\n";
            conclusion += $"  - Tiempo promedio de despacho: {promedioDespachoMM1:F2} minutos\n";
            conclusion += $"  - Proporción de corridas con espera >5 min: {proporcionSuperan5MinMM1:F2}%\n";
            conclusion += $"  - Diferencia con valor histórico (4.5 min): {diferenciaHistoricaMM1:F2} minutos";
            conclusion += diferenciaHistoricaMM1 > 0.5 ? " (Diferencia significativa, revisar suposiciones)\n" : "\n";

            conclusion += "• Resultados con 2 cajas (M/M/2):\n";
            conclusion += $"  - Tiempo promedio de espera: {promedioEsperaMM2:F2} minutos\n";
            conclusion += $"  - Tiempo promedio de despacho: {promedioDespachoMM2:F2} minutos\n";
            conclusion += $"  - Proporción de corridas con espera >5 min: {proporcionSuperan5MinMM2:F2}%\n";
            conclusion += $"  - Diferencia con valor histórico (4.5 min): {diferenciaHistoricaMM2:F2} minutos";
            conclusion += diferenciaHistoricaMM2 > 0.5 ? " (Diferencia significativa, revisar suposiciones)\n" : "\n";

            double reduccion = ((promedioEsperaMM1 - promedioEsperaMM2) / promedioEsperaMM1) * 100;
            conclusion += "• Reducción del tiempo de espera con 2 cajas: " + reduccion.ToString("F2") + "%\n";
            if (proporcionSuperan5MinMM1 > 50)
            {
                conclusion += "• Conclusión: " + (reduccion >= 50
                    ? "Abrir una segunda caja cumple con el objetivo de reducción ≥ 50%."
                    : "Abrir una segunda caja no cumple con el objetivo de reducción ≥ 50%.") + "\n";
            }
            else
            {
                conclusion += "• Conclusión: Mantener 1 caja es suficiente.\n";
            }

            // Aplicar formato de negrita a la conclusión
            lblConclusion.Text = conclusion;
            lblConclusion.Visible = true;
            int boldStart = conclusion.IndexOf("Conclusión:");
          
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            nudSimulaciones.Value = 10;
            nudClientes.Value = 1;
            nudDespacho.Value = 3;
            dataGridViewResults.Rows.Clear();
            chartWaitingTimes.Series["Tiempo Espera MM1"].Points.Clear();
            chartWaitingTimes.Series["Tiempo Espera MM2"].Points.Clear();
            lblConclusion.Visible = false;
            averageWaitingTimesMM1.Clear();
            averageServiceTimesMM1.Clear();
            averageQueueLengthsMM1.Clear();
            averageWaitingTimesMM2.Clear();
            averageServiceTimesMM2.Clear();
            averageQueueLengthsMM2.Clear();
        }
    }
}

