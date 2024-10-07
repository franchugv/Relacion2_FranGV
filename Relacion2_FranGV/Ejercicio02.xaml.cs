namespace Relacion2_FranGV;

public partial class Ejercicio02 : ContentPage
{
	public Ejercicio02()
	{
		InitializeComponent();
	}

    // Variables globales
    List<float> Valores = new List<float>();
    List<string> Operadores = new List<string>();


    // Eventos

    private void ControladorBotones(object sender, EventArgs e)
    {
        // Recursos
        bool esValido = true;
        string mensajeError = "";
        Button boton = (Button)sender;

        try
        {
            // Asignar contenido del botón al Entry
            EntryDatos.Text += boton.Text;
        }
        catch(Exception error)
        {
            esValido = false;
            mensajeError = error.Message;
        }
        finally
        {
            if (!esValido) MostrarError(mensajeError);
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
            Valores.Add(Convert.ToSingle(EntryDatos.Text));



           if(boton.Text != "=")
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
            if (!esValido) MostrarError(mensajeError);
        }
    }

    private void Calculos()
    {
        float resultado = Valores[0];


        for (int indice = 0; indice < Operadores.Count; indice++)
        {

            switch (Operadores[indice])
            {
                case "+":
                    resultado += Valores[indice + 1]; // Sumar el siguiente valor
                    break;
                case "÷":
                    resultado /= Valores[indice + 1]; // Dividir por el siguiente valor
                    break;
                case "-":
                    resultado -= Valores[indice + 1]; // Restar el siguiente valor
                    break;
                // Opcionalmente puedes agregar otros operadores, como multiplicación
                case "X":
                    resultado *= Valores[indice + 1];
                    break;
                case "%":

                    break;
            }
        }

        EntryDatos.Text = resultado.ToString();

        // Limpiar Datos
        Valores.Clear();
        Operadores.Clear();
    }

    private void Limpiar(object sender, EventArgs e)
    {
        EntryDatos.Text = "";
    }

    private void MostrarError(string error)
    {
        DisplayAlert("Error", $"Error: {error}", "Ok");
    }


}