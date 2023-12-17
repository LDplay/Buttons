namespace WinFormsApp13
{
    public partial class Form1 : Form
    {
        private int buttonCount = 0; 

        public Form1()
        {
            InitializeComponent();

            this.MouseDown += MainForm_MouseDown;
            this.MouseMove += MainForm_MouseMove;
            this.MouseUp += MainForm_MouseUp;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentPoint = e.Location;
                this.Invalidate();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button newButton = CreateButton();
                this.Controls.Add(newButton);
            }
        }

        private Button CreateButton()
        {
            Button button = new Button
            {
                Name = "button" + buttonCount,
                Text = buttonCount.ToString(),
                Location = new Point(Math.Min(startPoint.X, currentPoint.X), Math.Min(startPoint.Y, currentPoint.Y)),
                Size = new Size(Math.Abs(startPoint.X - currentPoint.X), Math.Abs(startPoint.Y - currentPoint.Y)),
                BackColor = GenerateRandomColor()
            };

            button.Click += Button_Click;

            buttonCount++; 

            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Вы нажали на кнопку {((Button)sender).Text}");
        }

        private Color GenerateRandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        private Point startPoint;
        private Point currentPoint;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (startPoint != currentPoint)
            {
                Rectangle rect = new Rectangle(
                    Math.Min(startPoint.X, currentPoint.X),
                    Math.Min(startPoint.Y, currentPoint.Y),
                    Math.Abs(startPoint.X - currentPoint.X),
                    Math.Abs(startPoint.Y - currentPoint.Y));

                e.Graphics.DrawRectangle(Pens.Black, rect);
            }
        }
    }
}