using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Cavalo : Peca {

        public Cavalo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            // L acima e direita
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;               
            }

            // L acima e esquerda
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // L direita e acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // L direita e abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // L abaixo e direita
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // L abaixo e esquerda
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // L esquerda e acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // L esquerda e abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }




        private bool MovimentoPossivel(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override string ToString() {
            return "C";
        }
    }
}
