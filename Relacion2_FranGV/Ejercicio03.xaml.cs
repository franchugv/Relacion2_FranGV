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

        // Instanciar Layout
        VerticallayoutPrincipal = new VerticalStackLayout
        {
            Padding = new Thickness(20, 40, 20, 20),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,

            Children =
            {                
                EntryDatos
            }
        };




        // Generación de botones

        HorizontalStackLayout Fila1 = new HorizontalStackLayout();
        HorizontalStackLayout Fila2 = new HorizontalStackLayout();
        HorizontalStackLayout Fila3 = new HorizontalStackLayout();
        HorizontalStackLayout Fila4 = new HorizontalStackLayout();

        string[] ListaBotonesNoOperadores = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "%", "Borrar" };
 


        const byte numColum = 4;
        string[] BotonesTotales = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "%", "Borrar", "x", "-", "+", "÷", "=" };
        string   Fila0List = "Borrar";
        string[] Fila1List = { "7", "8", "9", "x" };
        string[] Fila2List = { "4", "5", "6", "+" };
        string[] Fila3List = { "1", "2", "3", "÷" };
        string[] Fila4List = { "0", "%", "-", "=" };


        List<Button> botones = new List<Button>();

        // FILA 1
        for (int indice = 0; indice < numColum; indice++)
		{
            botones.Add(CrearBoton(Fila1List[indice], ListaBotonesNoOperadores));
            
            Fila1.Children.Add(botones[indice]);
        }
        VerticallayoutPrincipal.Children.Add(Fila1);

        // FILA 2
        for(int indice = 0; indice < numColum; indice++)
        {
            botones.Add(CrearBoton(Fila2List[indice], ListaBotonesNoOperadores));

            Fila2.Children.Add(botones[indice]);
        }
        VerticallayoutPrincipal.Children.Add(Fila2);

        // FILA 3

        for (int indice = 0; indice < numColum; indice++)
        {
            botones.Add(CrearBoton(Fila3List[indice], ListaBotonesNoOperadores));

            Fila3.Children.Add(botones[indice]);


        }
        VerticallayoutPrincipal.Children.Add(Fila3);

        // FILA 4

        for (int indice = 0; indice < numColum; indice++)
        {
            botones.Add(CrearBoton(Fila4List[indice], ListaBotonesNoOperadores));

            Fila4.Children.Add(botones[indice]);
        }
        VerticallayoutPrincipal.Children.Add(Fila4);



        // Cargar el Layout
        Content = VerticallayoutPrincipal;



    }

    // Creación de controles
    private Button CrearBoton(string texto, string[] Lista)
    {
        Button boton = new Button()
        {
            Text = texto,
            WidthRequest = 50,
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