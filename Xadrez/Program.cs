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

                tabuleiro.AdicionarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
                tabuleiro.AdicionarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(1, 3));
                tabuleiro.AdicionarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 2));
                tabuleiro.AdicionarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(7, 3));

                Tela.ImprimirTabuleiro(tabuleiro);


            } catch (TabuleiroException ex) { 
                Console.WriteLine(ex.Message);
            }
        }
    }

}