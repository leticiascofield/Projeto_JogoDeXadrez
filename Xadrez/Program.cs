using System;
using System.Globalization;
using Xadrez;
using TabuleiroXadrez;
using LogicaXadrez;


namespace XadrezConsole {
    internal class Program {
        static void Main(string[] args) {

            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            tabuleiro.AdicionarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
            tabuleiro.AdicionarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
            tabuleiro.AdicionarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(2, 4));

            Tela.ImprimirTabuleiro(tabuleiro);

        }
    }

}