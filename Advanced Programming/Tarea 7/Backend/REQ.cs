using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public static class REQ
    {

        //Esto es un método para parsear el JSON leido como string
        public static string[] JSONPARSER (string httpREQ)
        {
            try 
            {
                string[] stringSeparators = new string[] { "\"url\":\"" };
                string[] images;
                images = httpREQ.Split(stringSeparators, StringSplitOptions.None);
                for (int i = 1; i < images.Length; i++ )
                {
                    string link = "";
                    for (int j = 0; j < images[i].Length; j++)
                    {
                        if (images[i][j] != '\"')
                        {
                            link += images[i][j];
                        }
                        else 
                        {
                            break;
                        }
                    }
                    images[i] = link; 
                }
                string[] retorner = new string[images.Length - 1];

                for (int i = 1; i < images.Length; i++ )
                {
                    retorner[i - 1] = images[i].Trim();
                }

                return retorner;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        //REQUEST HTTP a una API REST

        public static string Request(string SEARCH) 
        {
            string sURL;
            sURL = "https://ajax.googleapis.com/ajax/services/search/images?v=1.0&q="+SEARCH;

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);


            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    return sLine;
            }
            return "ERROR";
        }
    }
}
