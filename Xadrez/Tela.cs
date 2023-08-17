using System.Collections.Generic;
using TabuleiroXadrez;
using LogicaXadrez;
using System.Runtime.Intrinsics.X86;

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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
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
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0)) { 
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    } else {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Tela.ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
            Console.WriteLine("     a  b  c  d  e  f  g  h ");
            Console.WriteLine();
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis) {

            Console.WriteLine();
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.White;
            for (int i = 0; i < tabuleiro.Linhas; i++) {
                Console.Write("  " + (8 - i) + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++) {
                    if (posicoesPossiveis[i,j]) {
                        Console.BackgroundColor = fundoAlterado;
                    } else {
                        if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0)) {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        } else {
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                    }
                    Tela.ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("     a  b  c  d  e  f  g  h ");
            Console.WriteLine();
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez() {
            
            string s = Console.ReadLine();

            if (s == "legenda") {
                ImprimirLegenda();
            }

            char coluna = ' ';
            if (s[0] == 'a' || s[0] == 'b' || s[0] == 'c' || s[0] == 'd' ||
            s[0] == 'e' || s[0] == 'f' || s[0] == 'g' || s[0] == 'h') {
                coluna = s[0];
            }  else {
                throw new TabuleiroException("Erro de digitação, essa posição não existe.");
            }

            int linha = 0;
            if (s[1] == '1' || s[1] == '2' || s[1] == '3' || s[1] == '4' ||
            s[1] == '5' || s[1] == '6' || s[1] == '7' || s[1] == '8') {
                linha = int.Parse(s[1] + "");
            } else {
                throw new TabuleiroException("Erro de digitação, essa posição não existe.");
            }
            
            return new PosicaoXadrez(coluna, linha);
        }
        public static void ImprimirPeca(Peca peca) {

            if (peca == null) {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("   ");
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Console.Write(" ");
                if (peca.Cor == Cor.Branca) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(peca);
                    Console.ForegroundColor = ConsoleColor.White;
                } else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static void ImprimirLegenda() {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Legenda:");
            Console.WriteLine();
            Console.WriteLine("    P - Peão");
            Console.WriteLine("    T - Torre");
            Console.WriteLine("    C - Cavalo");
            Console.WriteLine("    B - Bispo");
            Console.WriteLine("    D - Dama");
            Console.WriteLine("    R - Rei");
            throw new TabuleiroException("");
        }
    }
}
