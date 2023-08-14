using System;


namespace TabuleiroXadrez {
    internal class Tabuleiro {

        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas) {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }


        public Peca Peca(int linha, int coluna) {
            return Pecas[linha, coluna];
        }

        public void AdicionarPeca(Peca peca, Posicao posicao) {
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }
    }
}