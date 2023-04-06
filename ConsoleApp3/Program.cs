using System;
using System.Data;
using System.Data.SqlClient;
namespace exercisepabd_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = DESKTOP-LAFVQ8T\\MAHFUDZSIDDIQ; ";
                                conn = new SqlConnection(string.Format(strKoneksi, db));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.Write("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA TERMINAL\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA BUS DITERMINAL\n");
                                                    Console.WriteLine("Masukkan  ID bus :");
                                                    string ID = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Bus :");
                                                    string Nmabus = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Type Bus :");
                                                    string tpe = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jenis Kelamin (L/P) :");
                                                    string jk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string notlpn = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(ID, Nmabus, tpe, jk, notlpn, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " +
                                                            "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }

        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From Bus", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string ID, string Nmabus, string tpe, string jk, string notlpn,
            SqlConnection con)
        {

            string str = "";
            str = "insert into  Bus (ID,NamaBus,TypeBus,sex,PhoneMhs)"
                    + " values(@ID,@nma,@Type,@JK,@Phn)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nim", ID));
            cmd.Parameters.Add(new SqlParameter("nma", Nmabus));
            cmd.Parameters.Add(new SqlParameter("alamat", tpe));
            cmd.Parameters.Add(new SqlParameter("JK", jk));
            cmd.Parameters.Add(new SqlParameter("Phn", notlpn));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}
