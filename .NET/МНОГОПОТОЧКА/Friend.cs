using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    public class Friend
    {
        public string Name { get; set; }
        public Queue<Disk> Discs { get; set; }
        public Friend Recipient { get; set; }

        public Friend() { }
        public Friend(string name) => Name = name;
        public Friend(string name, Queue<Disk> discs)
        {
            Name = name;
            Discs = discs;
        }
        public Friend(string name, Queue<Disk> discs, Friend recipient)
        {
            Name = name;
            Discs = discs;
            Recipient = recipient;
        }

        public event Action<Disk> SendDisk;
        public event Action<Disk> WatchingDisk;
        public event Action<Disk> WatchedDisk;

        public void SendingDisk(Disk disk)
        {
            // Создаем новый поток для просмотра фильма на диске.
            Thread thread = new Thread(() =>
            {
                // Добавляем диск получателю.
                Recipient.Discs.Enqueue(disk);

                // Указываем на отправку диска.
                SendDisk?.Invoke(disk);

                // Смотрит, пока не закончится фильм на диске.
                for (int i = 0; i < 10; ++i)
                {
                    WatchingDisk?.Invoke(disk);

                    Thread.Sleep(200);
                }

                // Указываем, что завершился просмотр фильма на диске.
                WatchedDisk?.Invoke(disk);
            });

            thread.Start();
        }
    }
}
