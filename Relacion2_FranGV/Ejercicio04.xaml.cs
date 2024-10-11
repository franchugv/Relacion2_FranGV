namespace Relacion2_FranGV;

public partial class Ejercicio04 : ContentPage
{
    // Datos calculadora
    List<float> _numeros = new List<float>();
    List<string> _operadores = new List<string>();
    Entry EntryDatos = new Entry() { IsReadOnly = true };


    public Ejercicio04()
	{
		InitializeComponent();



        Grid GridPrincipal = new Grid()
        {
            RowSpacing = 2,
            ColumnSpacing = 2,
            VerticalOptions = LayoutOptions.Center,
            RowDefinitions =
            {                
                new RowDefinition{Height = new GridLength(100)},
                new RowDefinition{Height = new GridLength(100)},
                new RowDefinition{Height = new GridLength(100)},
                new RowDefinition{Height = new GridLength(100)},
                new RowDefinition{Height = new GridLength(100)},
                new RowDefinition{Height = new GridLength(100)},
                new RowDefinition{Height = new GridLength(100)},
            },
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            }
        };
        
        // Asignar Entry
        Grid.SetColumnSpan(EntryDatos, 4);
        Grid.SetRow(EntryDatos, 0);
        GridPrincipal.Children.Add(EntryDatos);


        // Generación de botones
        string[] ListaBotonesNoOperadores = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "%", "Borrar" };


        // RECURSOS FILAS
        const byte numColum = 4;
        string[] BotonesTotales = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "%", "Borrar", "x", "-", "+", "÷", "=" };
        string Fila0List = "Borrar";
        string[] Fila1List = { "7", "8", "9", "x" };
        string[] Fila2List = { "4", "5", "6", "+" };
        string[] Fila3List = { "1", "2", "3", "÷" };
        string[] Fila4List = { "0", "%", "-", "=" };


        // FILA 0

        Button botonBorrar = CrearBoton(Fila0List, ListaBotonesNoOperadores, 200);

        // Crear el botón
        CrearBoton(Fila0List, ListaBotonesNoOperadores, 600);

        // Añadir el botón al GridPrincipal
        Grid.SetColumnSpan(botonBorrar, 4);
        Grid.SetRow(botonBorrar, 1);
        GridPrincipal.Children.Add(botonBorrar);


        List<Button> botonesFila1 = new List<Button>();


        // FILA 1
        for (int indice = 0; indice < numColum; indice++)
        {
            botonesFila1.Add(CrearBoton(Fila1List[indice], ListaBotonesNoOperadores, 50));

            GridPrincipal.Add(botonesFila1[indice], indice, 2);
        }

        List<Button> botonesFila2 = new List<Button>();

        // FILA 2
        for (int indice = 0; indice < numColum; indice++)
        {
            botonesFila2.Add(CrearBoton(Fila2List[indice], ListaBotonesNoOperadores, 50));

            GridPrincipal.Add(botonesFila2[indice], indice, 3);
        }

        // FILA 3
        List<Button> botonesFila3 = new List<Button>();

        for (int indice = 0; indice < numColum; indice++)
        {
            botonesFila3.Add(CrearBoton(Fila3List[indice], ListaBotonesNoOperadores, 50));

            GridPrincipal.Add(botonesFila3[indice], indice, 4);


        }

        // FILA 4
        List<Button> botonesFila4 = new List<Button>();

        for (int indice = 0; indice < numColum; indice++)
        {
            botonesFila4.Add(CrearBoton(Fila4List[indice], ListaBotonesNoOperadores, 50));

            GridPrincipal.Add(botonesFila4[indice], indice, 5);
        }



        Content = GridPrincipal;




    }


    // Creación de controles
    private Button CrearBoton(string texto, string[] Lista, float tamanio)
    {
        Button boton = new Button()
        {
            Text = texto,
            WidthRequest = tamanio,
            HeightRequest = 50,
        };


        if (Lista.Contains(texto))
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