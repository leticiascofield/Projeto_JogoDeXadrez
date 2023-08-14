using System;
using System.Globalization;
using Xadrez;
using TabuleiroXadrez;
using LogicaXadrez;


namespace XadrezConsole {
    internal class Program {
        static void Main(string[] args) {
            try {

                Tabuleiro tabuleiro = new Tabuleiro(8, 8);
                PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);


                Console.WriteLine(posicaoXadrez);
                Console.WriteLine(posicaoXadrez.ToPosicao());
                Tela.ImprimirTabuleiro(tabuleiro);


            } catch (TabuleiroException ex) { 
                Console.WriteLine(ex.Message);
            }
        }
    }

}