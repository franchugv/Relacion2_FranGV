namespace Relacion2_FranGV;

public partial class Ejercicio01 : ContentPage
{

    #region RECURSOS CLASE

    public Ejercicio01()
	{
		InitializeComponent();
	}


    // Datos calculadora
    List<float> Numeros = new List<float>();
    List<string> Operadores = new List<string>();
#endregion

    #region EVENTOS
    // Eventos
    private void ControladorBotones(object sender, EventArgs e)
    {
        // Recursos
        bool esValido = true;
        string mensajeError = "";
        Button boton = (Button)sender;

        try
        {
            switch (boton.Text)
            {
                case "Borrar":
                    LimpiarDatos();
                    break;
                case "%":
                    FuncionBotonPorcentaje();
                    break;
                default:
                    // Asignar contenido del botón al Entry
                    EntryDatos.Text += boton.Text;
                    break;
            }

        }
        catch (Exception error)
        {
            esValido = false;
            mensajeError = error.Message;
        }
        finally
        {
            if (!esValido)
            {
                MostrarError(mensajeError);
                LimpiarDatos();
            }
        }

    }

    private void ControladorBotonesOperadores(object sender, EventArgs e)
    {
        // Recursos
        bool esValido = true;
        string mensajeError = "";
        Button boton = (Button)sender;
        string aux;

        try
        {
            // Añadir valores
            // En caso de pulsar el igual, también se guardará el resultado
            Numeros.Add(Convert.ToSingle(EntryDatos.Text));


            // Cada vez que pulsemos
            // un operador limpiaremos el Entry
            if (boton.Text != "=")
            {
                Operadores.Add(boton.Text);
                EntryDatos.Text = "";
            }
            else
            {
                Calculos();
            }
        }
        catch (Exception error)
        {
            esValido = false;
            mensajeError = error.Message;
        }
        finally
        {
            if (!esValido)
            {
                MostrarError(mensajeError);
                LimpiarDatos();
            }
        }
    }

    #endregion

    #region FUNCIONES BOTONES
    // Funciones botones
    private void FuncionBotonPorcentaje()
    {
        // Recursos
        float numero = 0;

        // Proceso
        numero = Convert.ToSingle(EntryDatos.Text);
        numero = (numero / 100);


        EntryDatos.Text = numero.ToString();
    }

    private void LimpiarDatos()
    {
        // Limpiar Datos
        EntryDatos.Text = "";
        Numeros.Clear();
        Operadores.Clear();
    }
    #endregion

    #region INTERFAZ
    // Salida
    private void MostrarError(string error)
    {
        DisplayAlert("Error", $"Error: {error}", "Ok");
    }

    private void Calculos()
    {
        // Lo inicializamos al primer valor
        float resultado = Numeros[0];

        // Los calculos están divididos por los operadores
        for (int indice = 0; indice < Operadores.Count; indice++)
        {

            switch (Operadores[indice])
            {
                // Inicializando el resultado con el index 0 y calculandolo con el siguiente podremos recorrer todos los datos
                case "+":
                    resultado += Numeros[indice + 1];
                    break;
                case "÷":
                    if (Numeros[indice + 1] == 0) throw new Exception("No se puede dividir Entre 0");

                    resultado /= Numeros[indice + 1];
                    break;
                case "-":
                    resultado -= Numeros[indice + 1];
                    break;
                case "x":
                    resultado *= Numeros[indice + 1];
                    break;

            }
        }
        // Mostrar en pantalla el resultado
        EntryDatos.Text = resultado.ToString();

        // Limpiar Datos
        Numeros.Clear();
        Operadores.Clear();
    }

    #endregion
}