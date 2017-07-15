﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
delegate void SetTextCallback(string dato);
namespace OpenEFI_Tuner{

    public partial class Form1 : Form{
        public bool conectado = false;
        SerialPort ArduinoPort = new SerialPort(); //DECLARAMOS instancia de serial port para luego empezar comunicacion
        public Form1(){

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e){

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e){

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports){
                listBox1.Items.Add(port);
            }
        }

        public void button1_Click(object sender, EventArgs e) {
            Conectar();
        }
        public void Conectar() {
            ArduinoPort.PortName = (string)listBox1.SelectedItem; //el puerto lo sacamos del listbox1 
            ArduinoPort.BaudRate = 9600; //la veloidad siempre queda fija
            ArduinoPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            try
            {
                ArduinoPort.Open(); //intentamos conectarnos al arduino
                MessageBox.Show("Se puedo conectar :D");
                conectado = true;
            }
            catch
            {
                MessageBox.Show("Te mandaste una cagada boludo 7-7");
            }
           

        }
        private void aquaGauge1_Load(object sender, EventArgs e)
        {
            
        }

        private void aquaGauge1_Load_1(object sender, EventArgs e)
        {
            aquaGauge1.MaxValue = 8000;
            aquaGauge1.MinValue = 0;
            aquaGauge1.DialText = "RPM";

            aquaGauge2.MaxValue = 120;
            aquaGauge2.MinValue = -10;
            aquaGauge2.Value = 50;
            aquaGauge2.DialText = "Temp °C";
        }

        public void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e){
            
            // Leemos el dato recibido del puerto serie
            string inData = ArduinoPort.ReadLine().ToString();
            actualizar(inData.ToString());
        }

        public void actualizar(string dato){
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(actualizar);
                this.Invoke(d, new object[] { dato });
            }
            else
            {
                int dato2 = Convert.ToInt32(dato);
                this.aquaGauge1.Value = dato2;
                this.textBox1.Text += dato + Environment.NewLine;
            }
        }

        private void sevenSegmentArray1_Load(object sender, EventArgs e)
        {
            sevenSegmentArray1.Value = "12.4";
            sevenSegmentArray2.Value = "12.4";
            sevenSegmentArray3.Value = "12.4";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        
    }
}