using Microsoft.Maui.Controls.Compatibility.Platform;

namespace Relacion2_FranGV;

public partial class Ejercicio03 : ContentPage
{


    // Datos calculadora
    List<float> _numeros = new List<float>();
    List<string> _operadores = new List<string>();
    Entry EntryDatos = new Entry() { IsReadOnly = true };

    public Ejercicio03()
	{
		// InitializeComponent(); // Sobra

        // Inicializar Layout
        VerticalStackLayout VerticallayoutPrincipal;
        // Creación filas
        HorizontalStackLayout FilaHSL = new HorizontalStackLayout();

        // Instanciar Layout
        VerticallayoutPrincipal = new VerticalStackLayout
        {
            Padding = new Thickness(20, 40, 20, 20),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,

            Children =
            {                
                EntryDatos,
                FilaHSL
            }
        };




        // Generación de botones



        string[] ListaBotonesNoOperadores = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "%", "Borrar" };
 

        // RECURSOS FILAS
        const byte numColum = 4;
        const byte numFilas = 4;
        string   FilaBorrar = "Borrar";
        string[] ListaBotonesTexto = { 
            "7", "8", "9", "x",
            "4", "5", "6", "+", 
            "1", "2", "3", "÷", 
            "0", "%", "-", "=" };




        // FILA 1
        FilaHSL.Children.Add(CrearBoton(FilaBorrar, ListaBotonesNoOperadores, 200));

        // Contador para recorrer la lista de botones
        int contador = 0;

        for(int indiceFila = 0; indiceFila < numFilas; indiceFila++)
        {
            // Creación de la fila
            FilaHSL = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.Center };

            for(int indiceColumna = 0; indiceColumna < numColum; indiceColumna++)
            {
                Button botonFila = CrearBoton(ListaBotonesTexto[contador], ListaBotonesNoOperadores, 50);

                // Añadir botón a la fila
                FilaHSL.Add(botonFila);

                contador++;
            }
            // Añadir columna al layout
            VerticallayoutPrincipal.Children.Add(FilaHSL);
        }

        // Cargar el Layout
        Content = VerticallayoutPrincipal;



    }

    // Creación de controles
    private Button CrearBoton(string textoBoton, string[] ListaNoOperadores, float ancho)
    {
        Button boton = new Button()
        {
            Text = textoBoton,
            WidthRequest = ancho,
            HeightRequest = 50,
        };

        if (ListaNoOperadores.Contains(textoBoton))
        {
            boton.Clicked += (s, e) => ControladorBotones(s, e);
        }
        else
        {
            boton.Clicked += (s, e) => ControladorBotonesOperadores(s, e);
        }


        return boton;
    }

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
            _numeros.Add(Convert.ToSingle(EntryDatos.Text));


            // Cada vez que pulsemos
            // un operador limpiaremos el Entry
            if (boton.Text != "=")
            {
                _operadores.Add(boton.Text);
                EntryDatos.Text = "";
            }
            else
            {
                EntryDatos.Text = Calculos.CalculosCalculadora(_numeros, _operadores).ToString();

                // Mostrar en pantalla el resultado

                // Limpiar Datos
                _numeros.Clear();
                _operadores.Clear();
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
        _numeros.Clear();
        _operadores.Clear();
    }
    #endregion

    // Salida
    private void MostrarError(string error)
    {
        DisplayAlert("Error", $"Error: {error}", "Ok");
    }

}