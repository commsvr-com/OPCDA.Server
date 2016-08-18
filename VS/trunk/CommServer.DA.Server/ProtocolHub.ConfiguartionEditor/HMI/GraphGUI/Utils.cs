//Utils.cs - zawiera proste funkcje matematyczne
//Autor: Paweł Grącki
using System;
using System.Collections.Generic;
namespace SchemaGenerator
{
    /// <summary>
    /// Klasa zawiera proste funkcje matematyczne
    /// </summary>
    static class Utils
    {
        /// <summary>
        /// Funkcja zwraca największy z parametrów
        /// </summary>
        /// <param name="a1">liczba nr 1</param>
        /// <param name="a2">liczba nr 2</param>
        /// <param name="a3">liczba nr 3</param>
        /// <param name="a4">liczba nr 4</param>
        public static int Max(int a1, int a2, int a3, int a4, int a5)
        {
            int b1, b2, c;
            if (a1 > a2)
            {
                b1 = a1;
            }
            else
            {
                b1 = a2;
            }
            if (a3 > a4)
            {
                b2 = a3;
            }
            else
            {
                b2 = a4;
            }
            if (b1 > b2)
            {
                c = b1;
            }
            else
            {
                c = b2;
            }
            if (c < a5)
            {
                c = a5;
            }
            return c;
        }
    }
}
