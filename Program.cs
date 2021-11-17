using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DsProject1._1
{
    class Program
      
    {
        static void Main(string[] args)
        {

            //deneme
            //****************************************************************************************************************
            //a kısmı kontrolü -OK
            Console.WriteLine("00000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Console.WriteLine("000000000000000000000000        ---A KISMI KONTROL---        0000000000000000000000");
            Console.WriteLine("00000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            MatrixClass d = new MatrixClass(20,100,100); //d nesnesi n width ve height parametreleri verilerek oluşturuldu
            double[,]matris=d.randomNumber();   //d nesnesine ait matris oluşturuldu


            for(int i = 0; i < matris.GetLength(0); i++)
            {
                
                
                Console.Write(matris[i,0]);
                Console.Write(" ");
                Console.WriteLine(matris[i, 1]);
               
               
            }


            //b kısmı kontrolü --OK
            Console.WriteLine("00000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Console.WriteLine("000000000000000000000000        ---B KISMI KONTROL---        0000000000000000000000");
            Console.WriteLine("00000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            double[,] m= d.distanceMatrix();
            for(int i = 0; i < d.matrix.GetLength(0); i++)
            {
                for(int j = 0; j < d.matrix.GetLength(0); j++)
                {
                    Console.Write(m[i,j]);
                    Console.Write("  ");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }


            //c kısmı kontrolü
            //
            Console.WriteLine("00000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Console.WriteLine("000000000000000000000000        ---C KISMI KONTROL---        0000000000000000000000");
            Console.WriteLine("00000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            ArrayList a = new ArrayList();
            double u;
            (a,u)=d.nNeighbor();

            Console.WriteLine("uzaklık: " + u);
            for(int i = 0; i < matris.GetLength(0); i++)
            {
                Console.WriteLine(a[i]);
            }








            //denemebitiş
            //****************************************************************************************************************

            Console.ReadLine();
        }
    }

    //MATRİS SINIFI:
    class MatrixClass
    {
        public int n;
        public int width;
        public int height;
        public double[,] matrix;


        public MatrixClass(int n1,int width1,int height1)
        {
            width = width1;
            height = height1;
            n = n1;
            matrix = new double[n, 2];
           

        }

        public double[,] randomNumber()
        {
           
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {

                matrix[i,0] = random.NextDouble()*width;
               // Console.WriteLine(matrix[i, 0]);
                matrix[i,1] = random.NextDouble() * height;
                //Console.WriteLine(matrix[i, 1]);


            }

            return matrix;

        }



        public double[,] distanceMatrix()
        {
            double[,] dM = new double[matrix.GetLength(0), matrix.GetLength(0)]; //metodun döndüreceği uzaklık matrisi



            // her bir elemanı diğer tüm elemanlarla karşılaştırmak için iç içe iki for döngüsü:
            for (int i = 0;i< matrix.GetLength(0); i++){
                for(int j = 0; j < matrix.GetLength(0); j++)
                {
                    dM[i, j] = uzaklıkHesapla(matrix[i, 0],matrix[j,0], matrix[i, 1],matrix[j,1]);

                }
            }




            //matematiksel hesplama için lokal metod:
            double uzaklıkHesapla(double x1,double x2,double y1,double y2)
            {
                double distance = Math.Sqrt(Math.Pow((x2-x1),2) + Math.Pow((y2 - y1), 2));

                return distance;
            }
            {

            }

            return dM;

        }






        //nearest neighbor metodu




        public (ArrayList,double) nNeighbor()
        {
            double toplamUzaklık = 0;

            // gezilen noktaları basitçe eklemek için arraylist kullanıyorum
            ArrayList gezilenNoktalar = new ArrayList();
            //en yakın nokta sıralaması:
            ArrayList nearestPointList = new ArrayList();

            //başlangıç için ilk random noktayı seçiyorum:
            Random rnd = new Random();


            int count = 0;// aşağıdaki for döngüsü içinde min sayıyı bulabilmek için

            int nn = rnd.Next(0, matrix.GetLength(0));

            double tempMinDist = 0;

            int nnIndex=0;
         
           
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
               
                gezilenNoktalar.Add(nn);  

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if ((i == j)|gezilenNoktalar.Contains(j))
                    {
                        //i==j durumu noktanın kendisine uzaklığı vereceğinden anlamsızdır
                        continue;
                    }
                    count++;
                    if (count == 1)//min sayıyı bulmak için pivot değer ataması
                    {
                        tempMinDist = uzaklıkHesapla(matrix[nn, 0], matrix[j, 0], matrix[nn, 1], matrix[j, 1]);
                        nnIndex = j;
                    }

                    if(uzaklıkHesapla(matrix[nn, 0], matrix[j, 0], matrix[nn, 1], matrix[j, 1]) < tempMinDist)  //for döngüsü boyunca döndürüp gerçek min değer bulunuyor
                    {
                        tempMinDist = uzaklıkHesapla(matrix[nn, 0], matrix[j, 0], matrix[nn, 1], matrix[j, 1]);
                        nnIndex = j;

                    }


                }
                nn = nnIndex;
               
                toplamUzaklık += tempMinDist;


                count = 0;
            }


            //matematiksel hesplama için lokal metod:
            double uzaklıkHesapla(double x1, double x2, double y1, double y2)
            {
                double distance = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

                return distance;
            }

            return (gezilenNoktalar,toplamUzaklık);
        }

    }
}
