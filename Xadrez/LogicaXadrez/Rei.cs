using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Rei : Peca {

        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new Posicao(0,0);

            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    if (!(i == 0 && j == 0)) {
                        posicao.DefinirValores(Posicao.Linha + i, Posicao.Coluna + j);
                        if (Tabuleiro.PosicaoValida(posicao) && MovimentoPossivel(posicao)) {
                            mat[posicao.Linha, posicao.Coluna] = true;
                        }
                    }
                }
            }

            return mat;
        }

       


        private bool MovimentoPossivel(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override string ToString() {
            return "R";
        }
    }
}
