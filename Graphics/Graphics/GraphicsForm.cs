using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GraphicsPractice
{
    public partial class GraphicsForm : Form
    {
        private Human human = new Human(100,Color.DarkBlue);
       

        public GraphicsForm()
        {
            InitializeComponent();

        }

        private void GraphicsForm_Load(object sender, System.EventArgs e) { }


        private void DrawHuman(Graphics graphics,int width,Color new_color_pants)
        {
            human=new Human(width, new_color_pants) { list_rivet = human.list_rivet };
            graphics.FillEllipse(new SolidBrush(human.Jacket_color), human.body);//тело
            graphics.FillEllipse(new SolidBrush(human.Body_color), human.left_leg);//левая нога
            graphics.FillEllipse(new SolidBrush(human.Body_color), human.right_leg);//правая нога
            graphics.FillEllipse(new SolidBrush(human.Body_color), human.head);//голова
            graphics.FillPolygon(new SolidBrush(human.Jacket_color), human.left_hand);//левая рука
            graphics.FillPolygon(new SolidBrush(human.Jacket_color), human.right_hand);//правая рука
            graphics.FillPolygon(new SolidBrush(human.Pants_color), human.Pants);//штаны
            graphics.FillPolygon(new SolidBrush(human.Jacket_color), human.Bottom_jacket);//низ куртки
            graphics.FillRectangle(new SolidBrush(human.Zip_color), human.Zip);//молния
            foreach (GraphicsPath rivet in human.list_rivet)
                graphics.FillPath(new SolidBrush(Color.Gray), rivet);
        }

        private void pictureBox_human_Paint(object sender, PaintEventArgs e)
        {
            switch(HumanFullnessTrackBar.Value)
            {
                case 1:
                    DrawHuman(e.Graphics, 60,colorDialog1.Color);
                    break;
                case 2:
                    DrawHuman(e.Graphics, 90, colorDialog1.Color);
                    break;
                case 3:
                    DrawHuman(e.Graphics, 120, colorDialog1.Color);
                    break;
                case 4:
                    DrawHuman(e.Graphics, 150, colorDialog1.Color);
                    break;
                case 5:
                    DrawHuman(e.Graphics, 180,colorDialog1.Color);
                    break;
            }
        }


        
        private void HumanFullnessTrackBar_Scroll(object sender, System.EventArgs e)
        {
            pictureBox_human.Invalidate();
        }

        private void button_color_pants_Click(object sender, System.EventArgs e)
        {
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                button_color_pants.BackColor = colorDialog1.Color;
                if (colorDialog1.Color == Color.Black)
                    button_color_pants.ForeColor = Color.White;
                else
                    button_color_pants.ForeColor = Color.Black;
                pictureBox_human.Invalidate();
            }
        }


        private void pictureBox_human_MouseClick(object sender, MouseEventArgs e)
        {
                if (e.Button == MouseButtons.Left)//добавление
                    human.AddRivet(e.X, e.Y);
                if (e.Button == MouseButtons.Right)//удаление
                    human.DeleteRivet(new Point(e.X, e.Y));
                pictureBox_human.Invalidate(); 
        }
    }
}