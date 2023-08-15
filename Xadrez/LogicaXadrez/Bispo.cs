using TabuleiroXadrez;

namespace LogicaXadrez {
    internal class Bispo : Peca {

        public Bispo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            //acima e esquerda
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.Linha = pos.Linha - 1;
                pos.Coluna = pos.Coluna - 1;
            }

            //acima e direita
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.Linha = pos.Linha - 1;
                pos.Coluna = pos.Coluna + 1;
            }

            //abaixo e esquerda
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.Linha = pos.Linha + 1;
                pos.Coluna = pos.Coluna - 1;
            }

            //abaixo e direita
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor) {
                    break;
                }
                pos.Linha = pos.Linha + 1;
                pos.Coluna = pos.Coluna + 1;
            }

            return mat;
        }




        private bool MovimentoPossivel(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override string ToString() {
            return "B";
        }
    }
}
