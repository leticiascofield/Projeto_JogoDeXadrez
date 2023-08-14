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

                tabuleiro.AdicionarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
                tabuleiro.AdicionarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 9));
                tabuleiro.AdicionarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(2, 4));
                tabuleiro.AdicionarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(0, 0));

                Tela.ImprimirTabuleiro(tabuleiro);
            } catch (TabuleiroException ex) { 
                Console.WriteLine(ex.Message);
            }

        }
    }

}