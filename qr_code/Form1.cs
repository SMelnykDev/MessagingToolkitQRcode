using System;
using System.Drawing;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;

namespace qr_code
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button4.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string qrtext = textBox1.Text;
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L; // - Using LOW for more storage
            Bitmap qrcode = encoder.Encode(qrtext);
            pictureBox1.Image = qrcode as Image;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image.Save(save.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();
            if (load.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.ImageLocation = load.FileName;
                button4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try {
                QRCodeDecoder decoder = new QRCodeDecoder();
                MessageBox.Show(decoder.decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap)),
                    "Info about QRcode",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            } catch (Exception exp)
            {
                MessageBox.Show("I am sorry, but this app can recognize only qrcode which was created by itself. It is in planning.",
                    "Error, can't recognized.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if (key < 'A' || key > 'z')
            {
                e.Handled = true;
            }
        }
    }
}
