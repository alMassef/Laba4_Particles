using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        List<DirectionColorfulEmiter> emiters = new List<DirectionColorfulEmiter>();

        public Form1()
        {
            InitializeComponent();

            // привязал изображение
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            var rnd = new Random();
            for (var i = 0; i < 50; ++i)
            {
                emiters.Add(new DirectionColorfulEmiter
                {
                    ParticlesCount = 50,
                    Position = new Point(rnd.Next(picDisplay.Width), -20),
                    Radius = 2 + rnd.Next(5),
                });
            }
        }

        // добавил функцию обновления состояния системы
        private void UpdateState()
        {
            foreach (var emiter in emiters)
            {
                emiter.UpdateState();
            }

            //foreach (var particle in particles)
            //{
            //    particle.Life -= 1; // уменшаю здоровье
            //                        // если здоровье кончилось
            //    if (particle.Life < 0)
            //    {
            //        // восстанавливаю здоровье
            //        particle.Life = 20 + Particle.rand.Next(100);
            //        particle.Speed = 1 + Particle.rand.Next(10);

            //        // генерировать вдоль верхней границы изображения
            //        particle.X = Particle.rand.Next(picDisplay.Image.Width);
            //        particle.Y = 0;

            //        // добавил направление движения -90 градусов +-15
            //        particle.Direction = -90 + 15 - Particle.rand.Next(30);
            //        particle.Radius = 2 + Particle.rand.Next(10);
            //    }
            //    else
            //    {
            //        // а это наш старый код
            //        var directionInRadians = particle.Direction / 180 * Math.PI;
            //        particle.X += (float)(particle.Speed * Math.Cos(directionInRadians));
            //        particle.Y -= (float)(particle.Speed * Math.Sin(directionInRadians));
            //    }
            //}

            //// добавил генерацию частиц
            //// генерирую не более 10 штук за тик
            //for (var i = 0; i < 10; ++i)
            //{
            //    if (particles.Count < 500) // пока частиц менье 500 генерируем новые
            //    {
            //        // снег белый, поэтому придется использовать ParticleColorful
            //        var particle = ParticleColorful.Generate();


            //        // цвет от белого
            //        particle.FromColor = Color.White;
            //        // до белого прозрачного
            //        particle.ToColor = Color.FromArgb(0, Color.White);


            //        // координата X вдоль всей верхней границы может оказаться
            //        particle.X = Particle.rand.Next(picDisplay.Image.Width);
            //        particle.Y = 0;
            //        // направление движения чтобы вниз
            //        particle.Direction = -90 + 15 - Particle.rand.Next(30);

            //        particles.Add(particle);
            //    }
            //    else
            //    {
            //        break; // а если частиц уже 500 штук, то ничего не генерирую
            //    }
            //}
        }

        // функция рендеринга
        private void Render(Graphics g)
        {
            foreach (var emiter in emiters)
            {
                emiter.Render(g);
            }

            // утащили сюда отрисовку частиц
            //foreach (var particle in particles)
            //{
            //    particle.Draw(g);
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateState(); // каждый тик обновляем систему

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black); // добавил очистку

                Render(g); // рендерим систему
            }

            // обновить picDisplay
            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            foreach (var emiter in emiters)
            {
                emiter.Direction = tbDirection.Value;
            }
        }

        private void tbSpread_Scroll(object sender, EventArgs e)
        {
            foreach (var emiter in emiters)
            {
                emiter.Spread = tbSpread.Value;
            }
        }

        private void tbSpeed_Scroll(object sender, EventArgs e)
        {
            foreach (var emiter in emiters)
            {
                emiter.Speed = tbSpeed.Value;
            }  
        }

        private void tdLife_Scroll(object sender, EventArgs e)
        {
            foreach (var emiter in emiters)
            {
                emiter.Life = tdLife.Value;
            }
        }
    }
}
