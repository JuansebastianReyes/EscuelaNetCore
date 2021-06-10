using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela.App
{
    class Program
    {
        static void Main(string[] args)
        {   
            //AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(2000, 100, 1);

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var listaAsg = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluaXAsig();
            var listaPromXAsig = reporteador.GetPromeAlumnPorAsignatura();

            Printer.WriteTitle("Captura de Evaluacion por consola");
            var newEval = new Evaluacion();
            string nombre, notaStr;

            WriteLine("Ingrese el nombre de la Evaluación");
            Printer.PresioneEnter();
            nombre = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El Valor del nombre no puede ser vacio");
                WriteLine("Saliendo del Programa");
            }else{
                newEval.Nombre = nombre.ToLower();
                WriteLine("El nombre de la evaluación a sido ingresado correctamente ");
            }

            WriteLine("Ingrese la nota de la evaluación");
            Printer.PresioneEnter();
            notaStr = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(notaStr))
            {

                Printer.WriteTitle("El Valor de la nota no puede ser vacio");
                WriteLine("Saliendo del Programa");

            }else{

                try
                {

                    newEval.Nota = float.Parse(notaStr);
                    if(newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    }else{
                        WriteLine("El nombre de la evaluación a sido ingresado correctamente ");
                    }
                      
                }
                catch(ArgumentOutOfRangeException e){
                    Printer.WriteTitle(e.Message);
                    WriteLine("Saliendo del Programa");
                }
                catch (Exception)
                {
                    Printer.WriteTitle("El Valor de la nota no un número valido");
                    WriteLine("Saliendo del Programa");
                }finally{
                    Printer.WriteTitle("FINALLY");
                    Printer.Beep(2500, 500, 3);
                }

            }
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.Beep(3000,1000,3);
             Printer.WriteTitle("SALIO");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            
            Printer.WriteTitle("Cursos de la Escuela");
            
            
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
