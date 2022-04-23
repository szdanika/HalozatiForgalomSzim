using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalozatiForgalomSzim.NetworkTools
{
    internal class Hub : NetworkTool
    { //Mindenkinek elkuldi akinek csak tudja illetve neki meg nem kuldott uzenetett
        public event Recived recived;
        public Hub(int addres, Connections<NetworkTool> con) : base(addres, con)
        {
        }

        public override void Recive(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            if(reciver == this)
                recived?.Invoke(this.ToString());
            else
                this.Send(sender,reciver,bytes);
        }

        public override void Send(NetworkTool sender, NetworkTool reciver, int bytes)
        {
            //Console.WriteLine("Beleptem ide : " + this.ToString());
            if(sender == this)
            {

                if (!connections.ItsConnected(sender, reciver))
                    throw new Exceptions.ItsNotConnectedException(sender, reciver);
            }
                

            foreach(var item in connections.Neighbors(this))
            {
                //Console.WriteLine("innen kuldom :" + this.ToString() + " ide : " + item.To.ToString());
                if(!sentMassage.Contains(item.To))
                {
                    ListAdd(item.To);
                    item.To.ListAdd(this);
                    item.To.Recive(sender, reciver, bytes);
                }
            }
            

        }
    }
}
