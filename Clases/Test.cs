using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFidelidadPaws.Clases
{
    public class Test
    {
        public static void Main(String[] args)
        {
            PawsClub paws = new PawsClub();
            Cliente agustin = new Cliente("Agustin", "RiverPlate2018", 200);
            paws.addCliente(agustin);
            Cliente clienteRecuperado = paws.getCliente("Agustin");


        }
    }
}