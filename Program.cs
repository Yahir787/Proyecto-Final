using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Proyecto_U5_y_U6
{
    class Comentario
    {
        //Propiedades
        public int id  { get; set;}
        public string autor { get; set; }
        public DateTime fecha { get; set; }
        public string comentario { get; set; }
        public string direccionIP { get; set; }
        public int es_Inapropiado { get; set; }
        public int likes { get; set; }

        //Apropiado o no
        public void ApropiadoSelec()
        {
            if(this.likes< this.es_Inapropiado)
            {
                Console.WriteLine("------------------------Comentario Censurado------------------------");
            }
            else
            {
                Console.WriteLine(this);
            }
        }

        //Fromato para la impresion en la consola
        public override string ToString()
        {
            return string.Format($"{id}  -  {autor}  -  {fecha} || {comentario} || Inapropiado: {es_Inapropiado}  -  Likes: {likes}");
        }
    }

    class ComentarioDB
    {
        //Creacion del archivo
        public static void SaveToFile(List<Comentario> comentarios, string path)
        {
            StreamWriter textOut = null;
            try
            {
                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comentario in comentarios)
                {
                    textOut.Write(comentario.id + "-");
                    textOut.Write(comentario.autor + "-");
                    textOut.Write(comentario.fecha + "-");
                    textOut.Write(comentario.comentario + "-");
                    textOut.Write(comentario.direccionIP + "-");
                    textOut.Write(comentario.es_Inapropiado + "-");
                    textOut.WriteLine(comentario.likes);
                }
            }
            catch(OutOfMemoryException e)
            {
                Console.WriteLine("No hay espacio suficiente en la memoria");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
            }


        }
        //Lectura del archivo
        public static List<Comentario> ReadFromFile(string path)
        {

            List<Comentario> comentarios = new List<Comentario>();

            StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));

            while (textIn.Peek() != -1)
            {
                string row = textIn.ReadLine();
                string[] col = row.Split('-');
                Comentario c = new Comentario();
                c.id = int.Parse(col[0]);
                c.autor = col[1];
                c.fecha = DateTime.Parse(col[2]);
                c.comentario = col[3];
                c.direccionIP = col[4];
                c.es_Inapropiado = int.Parse(col[5]);
                c.likes = int.Parse(col[6]);
                comentarios.Add(c);

            }
            textIn.Close();

            return comentarios;

        }
        //Implementacion de elemento al archivo
        public static void AddToFile( Comentario comentario, string path)
        {

            StreamWriter textOut = null;

            try
            {
                textOut = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write));
                textOut.Write(comentario.id + " - ");
                textOut.Write(comentario.autor + " - ");
                textOut.Write(comentario.fecha + " - ");
                textOut.Write(comentario.comentario + " - ");
                textOut.Write(comentario.direccionIP + " - ");
                textOut.Write(comentario.es_Inapropiado + " - ");
                textOut.WriteLine(comentario.likes);


            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine("No hay espacio suficiente en la memoria");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
            }
            

        }

    }

    

    class Program
    {
        static void Main(string[] args)
        {
            //Variables utilizadas en los distintos procesos
            int id=0;
            int opc = 0;
            string com, nom;
            DateTime Hoy = DateTime.Today;

            /*
           List<Comentario> comentarios = new List<Comentario>();

           comentarios.Add(new Comentario() { id = 0, autor = "Roberto", fecha = new DateTime(2021, 6, 2, 7, 0, 0, 0), comentario = "La pregunta no fue para ti", direccionIP = "192.168.0.11", es_Inapropiado = 27, likes = 6 });
           comentarios.Add(new Comentario() { id = 1, autor = "Pedro", fecha=new DateTime(2021,6,6,8,0,0,0), comentario="Hola, buenas tardes", direccionIP= "192.168.0.11",es_Inapropiado=1, likes=15 });
           comentarios.Add(new Comentario() { id = 2, autor = "Juan", fecha = new DateTime(2021, 6, 1, 11, 0, 0, 0), comentario = "Holaaaa", direccionIP = "192.168.0.11", es_Inapropiado = 0, likes = 21 });
           comentarios.Add(new Comentario() { id = 3, autor = "Alejandra", fecha = new DateTime(2021, 6, 2, 10, 0, 0, 0), comentario = "Hey Hey calmados", direccionIP = "192.168.0.11", es_Inapropiado = 3, likes = 7 });
           comentarios.Add(new Comentario() { id = 4, autor = "Maria", fecha = new DateTime(2021, 6, 1, 16, 0, 0, 0), comentario = "En el trabajo unu", direccionIP = "192.168.0.11", es_Inapropiado = 14, likes = 23 });
           comentarios.Add(new Comentario() { id = 5, autor = "Laura", fecha = new DateTime(2021, 6, 1, 13, 0, 0, 0), comentario = "Que hacen?", direccionIP = "192.168.0.11", es_Inapropiado = 5, likes = 17 });
           
            ComentarioDB.SaveToFile(comentarios, @"C:\Users\Yahir\comentarios.txt");
            */
                
            List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\Yahir\comentarios.txt");

            //filtro de la lista de comentarios
            foreach (var p in comentarios)
            {
            p.ApropiadoSelec();

            }
            
            try
            {           //Menu 
                do
                {
                    Console.WriteLine(" 1. Agregar un comentario \n 2. Borrar comentario \n 3. Ordenar por fecha \n 4. Ordenar por likes \n 5. Salir");
                    opc = int.Parse(Console.ReadLine());
                    switch (opc)
                    {
                        case 1:
                            {
                                //Agregar comentario
                                /*
                                List<Comentario> c = ComentarioDB.ReadFromFile(@"C:\Users\Yahir\comentarios.txt");
                                
                                id = 0;
                                foreach (var come in c)
                                {

                                    id = come.id++;

                                }
                                id++;*/
                                Console.WriteLine("Escriba el id que le gustaria en su comentario: ");
                                id = int.Parse(Console.ReadLine());

                                Console.WriteLine("Escriba su nombre: ");
                                nom = Console.ReadLine();

                                Console.WriteLine("Escriba su comentario: ");
                                com = Console.ReadLine();
                                Comentario x = new Comentario() { id = id, autor = nom, fecha = Hoy, comentario = com, direccionIP = "192.168.0.11", es_Inapropiado = 0, likes = 0 };
                                ComentarioDB.AddToFile(x, @"C:\Users\Yahir\comentarios.txt");


                            }
                            break;

                        case 2:
                            {
                                //Borrar comentario
                                int y = 0;
                                int z = 0;
                                List<Comentario> c = ComentarioDB.ReadFromFile(@"C:\Users\Yahir\comentarios.txt");
                                Console.WriteLine("Cual es la id del comentario?");
                                y = int.Parse(Console.ReadLine());
                                for (int i = 0; i < c.Count; i++)
                                {
                                    if (c[i].id == y)
                                    {

                                        z = i;

                                    }

                                }
                                c.RemoveAt(z);
                                ComentarioDB.SaveToFile(c, @"C:\Users\Yahir\comentarios.txt");

                            }
                            break;

                        case 3:
                            {
                                //Ordenado por fecha
                                List<Comentario> c = ComentarioDB.ReadFromFile(@"C:\Users\Yahir\comentarios.txt");

                                var n = c.OrderByDescending(x => x.fecha).ToList();

                                foreach (var come in n)
                                {
                                    come.ApropiadoSelec();
                                }

                            }
                            break;

                        case 4:
                            {
                                //Ordenado por likes
                                List<Comentario> c = ComentarioDB.ReadFromFile(@"C:\Users\Yahir\comentarios.txt");

                                var n = c.OrderByDescending(x => x.likes).ToList();
                                foreach (var come in n)
                                {
                                    come.ApropiadoSelec();
                                }



                            }
                            break;

                        case 5:
                            {
                                //Bye
                                Console.WriteLine("Tenga un buen dia B)");
                            }
                            break;

                        default:
                            {
                                Console.WriteLine(opc + " no es una opcion valida.");
                            }
                            break;

                    }

                } while (opc != 5);
            }
            catch (FormatException e)
            {
                Console.WriteLine("El valor tiene que ser en numero");
                Console.WriteLine(e);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("El valor es demasiado grande o pequeño para ser almacenado");
                Console.WriteLine(e);

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

                




            Console.ReadKey();
        }
    }
}
