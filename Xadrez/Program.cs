using System;
using System.Globalization;
using Xadrez;
using TabuleiroXadrez;
using LogicaXadrez;


namespace XadrezConsole {
    internal class Program {
        static void Main(string[] args) {

            PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

            while (!partidaDeXadrez.Terminada) {

                try {

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro);
                    Console.WriteLine();
                    Console.WriteLine("Turno: " + partidaDeXadrez.Turno);
                    Console.WriteLine("Aguardando jogada: " + partidaDeXadrez.JogadorAtual);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    partidaDeXadrez.PosicaoDeOrigemValida(origem);

                    bool[,] posicoesPossiveis = partidaDeXadrez.Tabuleiro.Peca(origem).MovimentosPossiveis();
                    Console.Clear();

                    Tela.ImprimirTabuleiro(partidaDeXadrez.Tabuleiro, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partidaDeXadrez.PosicaoDeDestinoValida(origem, destino);

                    partidaDeXadrez.RealizaJogada(origem, destino);

                } catch (TabuleiroException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }   
    }
}