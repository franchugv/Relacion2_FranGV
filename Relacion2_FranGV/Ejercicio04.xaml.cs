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

        // RECURSOS FILAS
        const byte numColumBotonesyGrid = 4;
        const byte numFilasBotones = 4;
        const byte numFilasGrid = 7;
        string[] ListaBotonesComunes = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "%", "Borrar" };


        string FilaBorrar = "Borrar";
        string[] FilaList = {
            "7", "8", "9", "x",
            "4", "5", "6", "+",
            "1", "2", "3", "÷",
            "0", "%", "-", "=" };

        // Inicializar Grid

        Grid GridPrincipal = new Grid();

        // Definir columnas
        for (int indiceColumn = 0; indiceColumn < numColumBotonesyGrid; indiceColumn++)
        {
            // Añadir cuatro colomnas
            GridPrincipal.ColumnDefinitions.Add(new ColumnDefinition());
        }

        // Definir filas
        for(int indiceFilas = 0; indiceFilas < numFilasGrid; indiceFilas++)
        {
            // Añadir siete filas
            GridPrincipal.RowDefinitions.Add(new RowDefinition());
        }


        // Fila 0
        // Asignar Entry
        Grid.SetColumnSpan(EntryDatos, 4);
        Grid.SetRow(EntryDatos, 0);
        GridPrincipal.Children.Add(EntryDatos);


        // FILA 1
        Button botonBorrar = CrearBoton(FilaBorrar, ListaBotonesComunes, 200, 50);

        // Crear el botón
        CrearBoton(FilaBorrar, ListaBotonesComunes, 1000, 50);

        // Añadir el botón al GridPrincipal
        // Column Span significa
        // que ocupará toda la columna
        Grid.SetColumnSpan(botonBorrar, 4);
        Grid.SetRow(botonBorrar, 1);
        GridPrincipal.Children.Add(botonBorrar);



        int contador = 0;

        // fila = 1, queremos que empiece una fila por debajo, ç
        // y numFilas también lo tenemos que desplazar una posición
        // para que el bucle no acabe antes de la cuenta
        for (int filas = 1; filas < numFilasBotones+1; filas++)
        {
           for(int columnas = 0; columnas < numColumBotonesyGrid; columnas++)
            {
                Button botones = CrearBoton(FilaList[contador], ListaBotonesComunes, 100, 50);
                // Contador para recorrer el array con el texto de botones
                contador ++;


                // Botón, columna, fila + 1,
                // queremos que empiece una fila por debajo,
                // ya que encima está el Entry y el botón borrar
                GridPrincipal.Add(botones, columnas, filas+1);
            }
        }


        Content = GridPrincipal;




    }


    // Creación de controles
    private Button CrearBoton(string textoBoton, string[] ListaBotones, float anchura, float altura)
    {
        Button boton = new Button()
        {
            Text = textoBoton,
            WidthRequest = anchura,
            HeightRequest = altura,
        };


        if (ListaBotones.Contains(textoBoton))
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