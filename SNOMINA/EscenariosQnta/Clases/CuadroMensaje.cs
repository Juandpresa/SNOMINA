using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;

namespace EscenariosQnta.Clases
{
    public class CuadroMensaje
    {
        #region Variables
        
        string UrlIcon = string.Empty;
        string EstiloBoton = string.Empty;
        string HtmlBoton = string.Empty;
        string HtmlScript = string.Empty;
        string HtmlCore = string.Empty;

        string BotonOKHTML = string.Empty;
        string BotonCancelHTML = string.Empty;

        string BotonYesHTML = string.Empty;
        string BotonNoHTML = string.Empty;

        string BotonYesCHTML = string.Empty;
        string BotonNoCHTML = string.Empty;
        string BotonCancelCHTML = string.Empty;

        StringBuilder builder = new StringBuilder();        

        object sender;
        #endregion

        public string MensajeTexto
        {
            get;
            set;
        }

        public string MensajeTitulo
        {
            get;
            set;
        }

        public CuadroMensajeIcono MensajeIcono
        {
            get;
            set;
        }

        public CuadroMensajeBoton MensajeBoton
        {
            get;
            set;
        }

        public CuadroMensajeEstilo MensajeEstilo
        {
            get;
            set;
        }

        public List<string> SuccessEvent = new List<string>();

        public List<string> FailedEvent = new List<string>();

        public CuadroMensaje()
        {           
            this.MensajeIcono = CuadroMensajeIcono.Informativo;
            this.MensajeBoton = CuadroMensajeBoton.Ok;
            this.MensajeEstilo = CuadroMensajeEstilo.EstiloB;
        }

        public CuadroMensaje(string texto)
        {
            this.MensajeTexto = texto;            
            this.MensajeIcono = CuadroMensajeIcono.Informativo;
            this.MensajeBoton = CuadroMensajeBoton.Ok;
            this.MensajeEstilo = CuadroMensajeEstilo.EstiloA;
        }

        public CuadroMensaje(string texto, string titulo)
        {
            this.MensajeTexto = texto;
            this.MensajeTitulo = titulo;
            this.MensajeIcono = CuadroMensajeIcono.Informativo;
            this.MensajeBoton = CuadroMensajeBoton.Ok;
            this.MensajeEstilo = CuadroMensajeEstilo.EstiloA;
        }
        
        public CuadroMensaje(string texto, string titulo, CuadroMensajeIcono icono, CuadroMensajeBoton boton, CuadroMensajeEstilo estilo)
        {
            this.MensajeTexto = texto;
            this.MensajeTitulo = titulo;
            this.MensajeIcono = icono;
            this.MensajeBoton = boton;
            this.MensajeEstilo = estilo;
        }

        public string Mostrar(object sender)
        {
            switch (this.MensajeIcono)
            {
                case CuadroMensajeIcono.Advertencia:
                    UrlIcon = "<img src ='../image/Advertencia.gif' />";
                    break;
                case CuadroMensajeIcono.Error:
                    UrlIcon = "<img src ='../image/Error.gif' />";
                    break;
                case CuadroMensajeIcono.Exitoso:
                    UrlIcon = "<img src ='../image/Exitoso.gif' />";
                    break;
                case CuadroMensajeIcono.Informativo:
                    UrlIcon = "<img src ='../image/Informativo.gif' />";
                    break;
                default:
                    UrlIcon = string.Empty;
                    break;
            }

            switch (this.MensajeEstilo)
            {
                case CuadroMensajeEstilo.EstiloA:
                    EstiloBoton = "button_classA";
                    break;
                case CuadroMensajeEstilo.EstiloB:
                    EstiloBoton = "button_classB";
                    break;
            }


            switch (this.MensajeBoton)
            {
                case CuadroMensajeBoton.Ok:
                    HtmlBoton = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    HtmlBoton = String.Format(HtmlBoton, "OK", EstiloBoton, "Yes();");
                    builder.Append(HtmlBoton);
                    break;
                
                case CuadroMensajeBoton.OKCancel:                    
                    BotonOKHTML = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    BotonOKHTML = String.Format(BotonOKHTML, "OK", EstiloBoton, "Yes();");
                    builder.Append(BotonOKHTML);
                    builder.Append("&nbsp;&nbsp;&nbsp;");
                    BotonCancelHTML = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    BotonCancelHTML = String.Format(BotonCancelHTML, "Cancel", EstiloBoton, "No();");
                    builder.Append(BotonCancelHTML);
                    break;
                
                case CuadroMensajeBoton.YesOrNo:                    
                    BotonYesHTML = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    BotonYesHTML = String.Format(BotonYesHTML, "Yes", EstiloBoton, "Yes();");
                    builder.Append(BotonYesHTML);
                    builder.Append("&nbsp;&nbsp;&nbsp;");
                    BotonNoHTML = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    BotonNoHTML = String.Format(BotonNoHTML, "No", EstiloBoton, "No();");
                    builder.Append(BotonNoHTML);
                    break;
                
                case CuadroMensajeBoton.YesNoCancel: 
                    BotonYesCHTML = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    BotonYesCHTML = String.Format(BotonYesCHTML, "Yes", EstiloBoton, "Yes();");
                    builder.Append(BotonYesCHTML);
                    builder.Append("&nbsp;&nbsp;&nbsp;");
                    BotonNoCHTML = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    BotonNoCHTML = String.Format(BotonNoCHTML, "No", EstiloBoton, "No();");
                    builder.Append(BotonNoCHTML);
                    builder.Append("&nbsp;&nbsp;&nbsp;");
                    BotonCancelCHTML = CuadroMensajeBasico.CuadroMensajeBotonHtml;
                    BotonCancelCHTML = String.Format(BotonCancelCHTML, "Cancel", EstiloBoton, "No();");
                    builder.Append(BotonCancelCHTML);
                    break;
            }

            string RealizadoNombre = string.Empty;
            string FallidaNombre = "1";

            StringBuilder RealizadoBuilder = new StringBuilder();
            StringBuilder FallidaBuilder = new StringBuilder();

            if (SuccessEvent != null && SuccessEvent.Count != 0)
            {
                int eventCounts = SuccessEvent.Count;
                for (int i = 0; i < eventCounts; i++)
                {
                    RealizadoBuilder.Append("PageMethods.");
                    RealizadoBuilder.Append(SuccessEvent[i].ToString());
                    RealizadoBuilder.Append("(null,null,Success, Failed);");
                }
                RealizadoNombre = RealizadoBuilder.ToString();
            }

            if (FailedEvent != null && FailedEvent.Count != 0)
            {
                int eventCounts = FailedEvent.Count;
                for (int i = 0; i < eventCounts; i++)
                {
                    FallidaBuilder.Append("PageMethods.");
                    FallidaBuilder.Append(FailedEvent[i].ToString());
                    FallidaBuilder.Append("(null,null,Success, Failed);");
                }
                FallidaNombre = FallidaBuilder.ToString();
            }

            HtmlScript = CuadroMensajeBasico.CuadroMensajeScript;
            HtmlScript = String.Format(HtmlScript, RealizadoNombre, FallidaNombre);
            HtmlCore = CuadroMensajeBasico.CuadroMensajeHTML;
            HtmlCore = String.Format(HtmlCore, this.MensajeTitulo, UrlIcon, this.MensajeTexto, builder.ToString());
            (sender as Page).ClientScript.RegisterClientScriptBlock(sender.GetType(), "_arg", HtmlScript, true);
            
            return HtmlCore;

        }

        public enum CuadroMensajeIcono
        {
            Ninguna,
            Advertencia,
            Exitoso,
            Informativo,
            Error
        }

        public enum CuadroMensajeBoton
        {
            Ok,
            OKCancel,
            YesOrNo,
            YesNoCancel
        };

        public enum CuadroMensajeEstilo
        {
            EstiloA,
            EstiloB
        }
    }
}