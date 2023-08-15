using System.Collections.Generic;
using TabuleiroXadrez;
using LogicaXadrez;

namespace Xadrez {
    internal class Tela {

        public static void ImprimirPartida(PartidaDeXadrez partidaDeXadrez) {
            ImprimirTabuleiro(partidaDeXadrez.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturas(partidaDeXadrez);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partidaDeXadrez.Turno);
            if (!partidaDeXadrez.Terminada) {
                Console.WriteLine("Aguardando jogada: " + partidaDeXadrez.JogadorAtual);
                if (partidaDeXadrez.Xeque) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("XEQUE!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("XEQUEMATE!");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vencedor: " + partidaDeXadrez.JogadorAtual);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        public static void ImprimirPecasCapturas(PartidaDeXadrez partidaDeXadrez) {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partidaDeXadrez.PecasCapturadasPorCor(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partidaDeXadrez.PecasCapturadasPorCor(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto) {
            Console.Write("[");
            foreach (Peca p in conjunto) {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro) {

            Console.WriteLine();
            for (int i = 0; i < tabuleiro.Linhas;  i++) {
                Console.Write("  " + (8 - i) + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++) {
                   Tela.ImprimirPeca(tabuleiro.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("    a b c d e f g h");
            Console.WriteLine();
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis) {

            Console.WriteLine();
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tabuleiro.Linhas; i++) {
                Console.Write("  " + (8 - i) + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++) {
                    if (posicoesPossiveis[i,j]) {
                        Console.BackgroundColor = fundoAlterado;
                    } else {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    Tela.ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("    a b c d e f g h");
            Console.WriteLine();
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void ImprimirPeca(Peca peca) {

            if (peca == null) {
                Console.Write("- ");
            } else {
                if (peca.Cor == Cor.Branca) {
                    Console.Write(peca);
                } else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
