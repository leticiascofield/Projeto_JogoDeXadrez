using System;


namespace TabuleiroXadrez {
    abstract class Peca {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Cor cor, Tabuleiro tabuleiro) {
            Posicao = null;
            Cor = cor;
            QteMovimentos = 0;
            Tabuleiro = tabuleiro;
        }

        public void IncrementarQteMovimentos() {
            QteMovimentos++;
        }

        public void DecrementarQteMovimentos() {
            QteMovimentos--;
        }

        public bool ExisteMovimentosPossivels() {
            bool[,] mat = MovimentosPossiveis();

            for(int i = 0; i < Tabuleiro.Linhas; i++) {
                for(int  j = 0; j < Tabuleiro.Colunas; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPermitido(Posicao posicao) {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}