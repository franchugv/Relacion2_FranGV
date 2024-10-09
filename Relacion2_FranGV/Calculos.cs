using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relacion2_FranGV
{
    public static class Calculos
    {
        public static float CalculosCalculadora(List<float> listaNumeros, List<string> listaOperadores)
        {
            // Lo inicializamos al primer valor
            float resultado = listaNumeros[0];

            // Los calculos están divididos por los operadores
            for (int indice = 0; indice < listaOperadores.Count; indice++)
            {

                switch (listaOperadores[indice])
                {
                    // Inicializando el resultado con el index 0 y calculandolo con el siguiente podremos recorrer todos los datos
                    case "+":
                        resultado += listaNumeros[indice + 1];
                        break;
                    case "÷":
                        if (listaNumeros[indice + 1] == 0) throw new Exception("No se puede dividir Entre 0");

                        resultado /= listaNumeros[indice + 1];
                        break;
                    case "-":
                        resultado -= listaNumeros[indice + 1];
                        break;
                    case "x":
                        resultado *= listaNumeros[indice + 1];
                        break;

                }
            }

            return resultado;

        }

    }
}
