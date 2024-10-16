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
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            },
            RowDefinitions =
            {                
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
            }
        };
        
        // Asignar Entry
        Grid.SetColumnSpan(EntryDatos, 4);
        Grid.SetRow(EntryDatos, 0);
        GridPrincipal.Children.Add(EntryDatos);


        // Generación de botones
        string[] ListaBotonesComunes = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "%", "Borrar" };


        // RECURSOS FILAS
        const byte numColum = 4;
        const byte numFilas = 4;

        string Fila0 = "Borrar";
        string[] FilaList = { 
            "7", "8", "9", "x",
            "4", "5", "6", "+", 
            "1", "2", "3", "÷", 
            "0", "%", "-", "=" };


        // FILA 0
        Button botonBorrar = CrearBoton(Fila0, ListaBotonesComunes, 200, 50);

        // Crear el botón
        CrearBoton(Fila0, ListaBotonesComunes, 1000, 50);

        // Añadir el botón al GridPrincipal
        Grid.SetColumnSpan(botonBorrar, 4);
        Grid.SetRow(botonBorrar, 1);
        GridPrincipal.Children.Add(botonBorrar);



        int contador = 0;

        // fila = 1, queremos que empiece una fila por debajo, ç
        // y numFilas también lo tenemos que desplazar una posición
        // para que el bucle no acabe antes de la cuenta
        for (int filas = 1; filas < numFilas+1; filas++)
        {
           for(int columnas = 0; columnas < numColum; columnas++)
            {
                Button botones = CrearBoton(FilaList[contador], ListaBotonesComunes, 100, 50);
                // Contador para recorrer el array con el texto de botones
                contador += 1;


                // Botón, columna, fila + 1, queremos que empiece una fila por debajo,
                // ya que encima está el Entry y el botón borrar
                GridPrincipal.Add(botones, columnas, filas+1);
            }
        }

        #region 
        //// FILA 1
        //for (int indice = 0; indice < numColum; indice++)
        //{
        //    botonesFila1.Add(CrearBoton(Fila1List[indice], ListaBotonesComunes, 50));

        //    // Botón, columna, fila
        //    GridPrincipal.Add(botonesFila1[indice], indice, 2);
        //}

        //List<Button> botonesFila2 = new List<Button>();

        //// FILA 2
        //for (int indice = 0; indice < numColum; indice++)
        //{
        //    botonesFila2.Add(CrearBoton(Fila2List[indice], ListaBotonesComunes, 50));

        //    GridPrincipal.Add(botonesFila2[indice], indice, 3);
        //}

        //// FILA 3
        //List<Button> botonesFila3 = new List<Button>();

        //for (int indice = 0; indice < numColum; indice++)
        //{
        //    botonesFila3.Add(CrearBoton(Fila3List[indice], ListaBotonesComunes, 50));

        //    GridPrincipal.Add(botonesFila3[indice], indice, 4);


        //}

        //// FILA 4
        //List<Button> botonesFila4 = new List<Button>();

        //for (int indice = 0; indice < numColum; indice++)
        //{
        //    botonesFila4.Add(CrearBoton(Fila4List[indice], ListaBotonesComunes, 50));

        //    GridPrincipal.Add(botonesFila4[indice], indice, 5);
        //}
        #endregion


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