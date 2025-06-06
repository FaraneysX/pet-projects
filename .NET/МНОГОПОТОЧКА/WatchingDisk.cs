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
    public partial class WatchingDisk : Form
    {
        public Disk Disk { get; set; }

        public WatchingDisk()
        {
            InitializeComponent();
        }

        public void SetInformation(Friend friend, Image image)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    disk_name.Text = Disk.Name;
                    name.Text = friend.Name;
                    disk_picture.Image = image;
                }));
            }
            else
            {
                disk_name.Text = Disk.Name;
                name.Text = friend.Name;
                disk_picture.Image = image;
            }
        }

        public void WatchingDiskProgress(Disk disk)
        {
            if (disk == Disk)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => WatchingDiskProgress(disk)));
                }
                else
                {
                    progressBar1.PerformStep();
                }
            }
        }

        public void Subscribe(Friend friend, Image image)
        {
            SetInformation(friend.Recipient, image);

            friend.WatchingDisk += WatchingDiskProgress;

            friend.WatchedDisk += disk =>
            {
                if (disk == Disk)
                {
                    Thread.Sleep(200);
                    InvokeClose();
                }
            };
        }

        private void InvokeClose()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Close()));
            }
            else
            {
                Close();
            }
        }
    }
}
