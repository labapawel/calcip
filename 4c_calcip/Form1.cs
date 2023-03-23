namespace _4c_calcip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            IP ip = new IP(textBox1.Text);
            textBox2.Text = ip.toString(ip.Ip);
            label3.Text = ip.toString(ip.Ip, 2);
            textBox3.Text = ip.toString(ip.Mask);
            label4.Text = ip.toString(ip.Mask, 2);
            textBox4.Text = ip.toString(ip.Network);
            label6.Text = ip.toString(ip.Network, 2);
            textBox5.Text = ip.toString(ip.Broadcast);
            label8.Text = ip.toString(ip.Broadcast, 2);

            textBox6.Text = ip.toString(ip.MinHost);
            label10.Text = ip.toString(ip.MinHost, 2);

            textBox7.Text = ip.toString(ip.MaxHost);
            label12.Text = ip.toString(ip.MaxHost, 2);
            label14.Text = ip.HostCount.ToString();

        }
    }
}