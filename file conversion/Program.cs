using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace file_conversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] bytesToSend1 = { };
            byte[] tempFile = { };
            byte[] configBuffer = { };
            List<byte[]> arraystuff = new List<byte[]>{ bytesToSend1, tempFile, configBuffer };             //string singleFile = @"\\DISTRSERVER\public-drive\Justin_files\MC92N0\\WM\UPDATE\CleanReg.bin";
            string[] address = {  };
            address = File.ReadAllLines(@"C:\Users\Justin\Desktop\string of files\92WM65.txt");

            string[] configs = {  };
            configs = File.ReadAllLines(@"C:\Users\Justin\Desktop\string of files\92WM65configs.txt");
            foreach (string config in configs) 
            {
                string fileName = Path.GetFileName(config);
                Console.WriteLine($"Process started for file {fileName}");

                bytesToSend1 = new byte[0];
                for (int x = 0; x < address.Length; x++)
                {
                    //get array size
                    Console.WriteLine($"Collecting file {address[x]}");
                    tempFile = new byte[File.ReadAllBytes(address[x]).ToArray().Length];

                    //declare array info
                    Console.WriteLine($"Collected file {address[x]}. File size:{tempFile.Length} bytes");
                    tempFile = File.ReadAllBytes(address[x]).ToArray();

                    //config info merged in
                    if (x == 1)
                    {
                        configBuffer = new byte[File.ReadAllBytes(config).ToArray().Length];
                        configBuffer = File.ReadAllBytes(config).ToArray();
                        bytesToSend1 = bytesToSend1.Concat(configBuffer).ToArray();
                    }

                    bytesToSend1 = bytesToSend1.Concat(tempFile).ToArray();
                    Console.WriteLine($"attached {fileName} to big array. total size:{bytesToSend1.Length} bytes\n");
                }
                Console.WriteLine("currently writing file");
                File.WriteAllBytes($@"\\DISTRSERVER\public-drive\Justin_files\Master files\MC92N0\WM6.5\full hex images\{fileName}", bytesToSend1);
                Console.WriteLine(fileName + " written successfully\n");
            }
            Console.ReadLine();
        }
    }
}
