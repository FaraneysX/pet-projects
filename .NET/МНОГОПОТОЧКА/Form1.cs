using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            VideoLibrary videoLibrary = new VideoLibrary();
            CountDisks(videoLibrary.Peter, number_disks_1);
            CountDisks(videoLibrary.Ivan, number_disks_2);
            CountDisks(videoLibrary.Anton, number_disks_3);
            CountDisks(videoLibrary.Dmitriy, number_disks_4);

            videoLibrary.Peter.SendDisk += disk =>
            {
                Image diskImage = GetDiskImage(disk);

                SetImage(disk_1, diskImage);
                SetVisible(disk_1, true);

                CountDisks(videoLibrary.Peter, number_disks_1);

                // Если Петр решил отправить диск Ивану.
                if (videoLibrary.Peter.Recipient == videoLibrary.Ivan)
                {
                    MoveToDown(disk_1);

                    CountDisks(videoLibrary.Ivan, number_disks_2);
                }
                else if (videoLibrary.Peter.Recipient == videoLibrary.Dmitriy)
                {
                    MoveToRight(disk_1);

                    CountDisks(videoLibrary.Dmitriy, number_disks_4);
                }
                else if (videoLibrary.Peter.Recipient == videoLibrary.Anton)
                {
                    MoveToRight(disk_1);
                    MoveToDown(disk_1);

                    CountDisks(videoLibrary.Anton, number_disks_3);
                }

                SetVisible(disk_1, false);
                SetPosition(disk_1, 143, 92);

                OpenWatchForm(videoLibrary.Peter, disk, diskImage);
            };

            videoLibrary.Ivan.SendDisk += disk =>
            {
                Image diskImage = GetDiskImage(disk);

                SetImage(disk_2, diskImage);
                SetVisible(disk_2, true);

                CountDisks(videoLibrary.Ivan, number_disks_2);

                if (videoLibrary.Ivan.Recipient == videoLibrary.Peter)
                {
                    MoveToUp(disk_2);

                    CountDisks(videoLibrary.Peter, number_disks_1);
                }
                else if (videoLibrary.Ivan.Recipient == videoLibrary.Anton)
                {
                    MoveToRight(disk_2);

                    CountDisks(videoLibrary.Anton, number_disks_3);
                }
                else if (videoLibrary.Ivan.Recipient == videoLibrary.Dmitriy)
                {
                    MoveToRight(disk_2);
                    MoveToUp(disk_2);

                    CountDisks(videoLibrary.Dmitriy, number_disks_4);
                }

                SetVisible(disk_2, false);
                SetPosition(disk_2, 143, 390);

                OpenWatchForm(videoLibrary.Ivan, disk, diskImage);
            };

            videoLibrary.Anton.SendDisk += disk =>
            {
                Image diskImage = GetDiskImage(disk);

                SetImage(disk_3, diskImage);
                SetVisible(disk_3, true);

                CountDisks(videoLibrary.Anton, number_disks_3);

                if (videoLibrary.Anton.Recipient == videoLibrary.Ivan)
                {
                    MoveToLeft(disk_3);

                    CountDisks(videoLibrary.Ivan, number_disks_2);
                }
                else if (videoLibrary.Anton.Recipient == videoLibrary.Dmitriy)
                {
                    MoveToUp(disk_3);

                    CountDisks(videoLibrary.Dmitriy, number_disks_4);
                }
                else if (videoLibrary.Anton.Recipient == videoLibrary.Peter)
                {
                    MoveToLeft(disk_3);
                    MoveToUp(disk_3);

                    CountDisks(videoLibrary.Peter, number_disks_1);
                }

                SetVisible(disk_3, false);
                SetPosition(disk_3, 758, 390);

                OpenWatchForm(videoLibrary.Anton, disk, diskImage);
            };

            videoLibrary.Dmitriy.SendDisk += disk =>
            {
                Image diskImage = GetDiskImage(disk);

                SetImage(disk_4, diskImage);
                SetVisible(disk_4, true);

                CountDisks(videoLibrary.Dmitriy, number_disks_4);

                if (videoLibrary.Dmitriy.Recipient == videoLibrary.Peter)
                {
                    MoveToLeft(disk_4);

                    CountDisks(videoLibrary.Peter, number_disks_1);
                }
                else if (videoLibrary.Dmitriy.Recipient == videoLibrary.Anton)
                {
                    MoveToDown(disk_4);

                    CountDisks(videoLibrary.Anton, number_disks_3);
                }
                else if (videoLibrary.Dmitriy.Recipient == videoLibrary.Ivan)
                {
                    MoveToLeft(disk_4);
                    MoveToDown(disk_4);

                    CountDisks(videoLibrary.Ivan, number_disks_2);
                }

                SetVisible(disk_4, false);
                SetPosition(disk_4, 758, 92);

                OpenWatchForm(videoLibrary.Dmitriy, disk, diskImage);
            };

            videoLibrary.Start();
        }

        public void SetImage(PictureBox pictureBox, Image image)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => pictureBox.Image = image));
            }
            else
            {
                pictureBox.Image = image;
            }
        }

        private Image GetDiskImage(Disk disk)
        {
            if (disk.Name == "The Shawshank Redemption: Special Edition")
            {
                return Properties.Resources.disk_1;
            }

            if (disk.Name == "Inception: Director's Cut")
            {
                return Properties.Resources.disk_2;
            }

            if (disk.Name == "Pulp Fiction: Collector's Edition")
            {
                return Properties.Resources.disk_3;
            }

            if (disk.Name == "The Dark Knight Trilogy: Ultimate Box Set")
            {
                return Properties.Resources.disk_4;
            }

            if (disk.Name == "The Matrix Reloaded: Limited Edition")
            {
                return Properties.Resources.disk_5;
            }

            if (disk.Name == "Fight Club: Anniversary Edition")
            {
                return Properties.Resources.disk_6;
            }

            if (disk.Name == "Goodfellas: Remastered")
            {
                return Properties.Resources.disk_7;
            }

            if (disk.Name == "Star Wars: Episode IV - A New Hope: Special Edition")
            {
                return Properties.Resources.disk_8;
            }

            return null;
        }

        private void MoveTo(PictureBox pictureBox, int top, int left)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => MoveTo(pictureBox, top, left)));
            }
            else
            {
                pictureBox.Top += top;
                pictureBox.Left += left;
            }
        }

        private void MoveToRight(PictureBox pictureBox)
        {
            for (int i = 0; i < 40; ++i)
            {
                MoveTo(pictureBox, 0, 15);
                Thread.Sleep(40);
            }
        }

        private void MoveToLeft(PictureBox pictureBox)
        {
            for (int i = 0; i < 40; ++i)
            {
                MoveTo(pictureBox, 0, -15);
                Thread.Sleep(40);
            }
        }

        private void MoveToUp(PictureBox pictureBox)
        {
            for (int i = 0; i < 20; ++i)
            {
                MoveTo(pictureBox, -15, 0);
                Thread.Sleep(40);
            }
        }

        private void MoveToDown(PictureBox pictureBox)
        {
            for (int i = 0; i < 20; ++i)
            {
                MoveTo(pictureBox, 15, 0);
                Thread.Sleep(40);
            }
        }

        public void SetVisible(PictureBox pictureBox, bool visible)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => pictureBox.Visible =  visible));
            }
            else
            {
                pictureBox.Visible = visible;
            }
        }

        public void SetPosition(PictureBox pictureBox, int x, int y)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetPosition(pictureBox, x, y)));
            }
            else
            {
                pictureBox.Left = x;
                pictureBox.Top = y;
            }
        }

        private void CountDisks(Friend friend, Label label)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => label.Text = friend.Discs.Count.ToString()));
            }
            else
            {
                label.Text = friend.Discs.Count.ToString();
            }
        }

        private void OpenWatchForm(Friend friend, Disk disk, Image diskImage)
        {
            WatchingDisk form = new WatchingDisk
            {
                Disk = disk
            };

            form.WatchingDiskProgress(disk);
            form.Subscribe(friend, diskImage);

            if (InvokeRequired)
            {
                Invoke(new Action(() => form.Show()));
            }
            else
            {
                form.Show();
            }
        }
    }
}
